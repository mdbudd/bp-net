using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Text.Json.Serialization;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Data;
using WebApi.Models;
using Microsoft.OpenApi.Models;
using GraphQL;
using GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;

    // use sql server db in production and sqlite db in development
    if (env.IsProduction())
        services.AddDbContext<DataContext>();
    else
        services.AddDbContext<DataContext>(); // using sql server for both as docker is running it.
                                              // services.AddDbContext<DataContext, SqliteDataContext>();

    // services.AddDbContext<ProjectContext>(options =>
    //     options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectDb")));
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(opt =>
    {
        opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebApp", Version = "v1" });
        opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });

        opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
        });
    });

    services.Configure<BookStoreDatabaseSettings>(
        builder.Configuration.GetSection("BookStoreDatabase"));
    services.AddSingleton<BooksService>();
    // configure automapper with all automapper profiles from this assembly
    services.AddAutoMapper(typeof(Program));

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();

    services.AddSingleton<IEmployeeService, EmployeeService>();
    services.AddSingleton<EmployeeDetailsType>();
    services.AddSingleton<EmployeeQuery>();
    services.AddSingleton<ISchema, EmployeeDetailsSchema>();
    services.AddGraphQL(b => b
        .AddAutoSchema<EmployeeQuery>()  // schema
        .AddSystemTextJson());   // serializer
}

var app = builder.Build();


// configure HTTP request pipeline
{

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
    app.UseGraphQL<ISchema>("/graphql");            // url to host GraphQL endpoint
    app.UseGraphQLPlayground(
        "/",                               // url to host Playground at
        new GraphQL.Server.Ui.Playground.PlaygroundOptions
        {
            GraphQLEndPoint = "/graphql",         // url of GraphQL endpoint
            SubscriptionsEndPoint = "/graphql",   // url of GraphQL endpoint
        });
}

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{

    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

    dataContext.Database.Migrate();
    if (!dataContext.Users.Any())
    {
        var testUsers = new List<User>
        {
            new User { FirstName = "Super", LastName = "User", Username = "super", PasswordHash = BCryptNet.HashPassword("super"), Role = Role.Super },
            new User { FirstName = "Admin", LastName = "User", Username = "admin", PasswordHash = BCryptNet.HashPassword("admin"), Role = Role.Admin },
            new User { FirstName = "Normal", LastName = "User", Username = "user", PasswordHash = BCryptNet.HashPassword("user"), Role = Role.User }
        };
        dataContext.Users.AddRange(testUsers);
        dataContext.SaveChanges();
    }
    if (!dataContext.Products.Any())
    {
        dataContext.Seed();
    }
}

app.Run();