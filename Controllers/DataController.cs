using WebApi.Authorization;
using WebApi.Models;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Entities;
using WebApi.Data;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    private readonly DataContext _salesContext;

    public DataController(DataContext salesContext)
    {
        _salesContext = salesContext;
    }

    [HttpGet("defex")]
    public ActionResult GetDefEx()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        var query = numbers.Where(n => n % 2 == 0);
        // Deferred Execution
        Console.WriteLine("Deferred Execution:");
        foreach (var num in query)
        {
            Console.WriteLine(num);
        }
        // Immediate Execution
        Console.WriteLine("Immediate Execution:");
        var immediateQuery = numbers.Where(n => n % 2 == 0).ToList();
        foreach (var num in immediateQuery)
        {
            Console.WriteLine(num);
        }

        return Ok("done - see console");
    }

    [HttpGet("dbcontext")]
    public ActionResult GetDbContext()
    {
        var products = _salesContext.Products
                              .Where(c => c.ProductCategory == ProductCategory.Clothing)
                              .Where(c => c.Price > 100)
                              .OrderBy(c => c.Price)
                              .ToList();
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Name}, {product.ProductCategory}, {product.Price}");
        }


        return Ok("done - see console");
    }


    [HttpGet("setops")]
    public ActionResult GetSetOps()
    {
        var set1 = new List<int> { 1, 2, 3, 4, 5 };
        var set2 = new List<int> { 4, 5, 6, 7, 8 };
        Console.WriteLine("Union:");
        foreach (var num in set1.Union(set2))
        {
            Console.WriteLine(num);
        }
        Console.WriteLine("Intersect:");
        foreach (var num in set1.Intersect(set2))
        {
            Console.WriteLine(num);
        }

        Console.WriteLine("Difference (in set1 but not in set2):");
        foreach (var num in set1.Except(set2))
        {
            Console.WriteLine(num);
        }
        return Ok("done - see console");
    }


    [HttpGet("joinops")]
    public ActionResult GetJoinOps()
    {
        var employees = new List<DEmployee>
        {
            new DEmployee { Id = 1, Name = "John", DepartmentId = 1 },
            new DEmployee { Id = 2, Name = "Jane", DepartmentId = 2 },
            new DEmployee { Id = 3, Name = "Bob", DepartmentId = 1 },
        };

        var departments = new List<DDepartment>
        {
            new DDepartment { Id = 1, Name = "Engineering" },
            new DDepartment { Id = 2, Name = "Marketing" },
        };

        var query = employees.Join(departments,
                                   emp => emp.DepartmentId,
                                   dept => dept.Id,
                                   (emp, dept) => new { EmployeeName = emp.Name, DepartmentName = dept.Name });

        foreach (var result in query)
        {
            Console.WriteLine($"{result.EmployeeName} - {result.DepartmentName}");
        }

        return Ok("done - see console");
    }


    [HttpGet("groupops")]
    public ActionResult GetGroupOps()
    {
        var people = new List<Person>
        {
            new Person { Name = "Alice", Age = 30 },
            new Person { Name = "Peter", Age = 25 },
            new Person { Name = "Laura", Age = 50 },
            new Person { Name = "Bob", Age = 25 },
            new Person { Name = "Michael", Age = 30 },
            new Person { Name = "Tommy", Age = 50 },
        };

        var groupedByAge = people.GroupBy(p => p.Age);

        foreach (var group in groupedByAge)
        {
            Console.WriteLine($"Age Group: {group.Key}");

            foreach (var person in group)
            {
                Console.WriteLine($"  {person.Name}");
            }
        }

        return Ok("done - see console");
    }



    [HttpGet("collections")]
    public ActionResult GetCollections()
    {
        var products = new List<DProduct>
        {
            new DProduct { Name = "Laptop", Stock = 10 },
            new DProduct { Name = "Smartphone", Stock = 0 },
            new DProduct { Name = "Tablet", Stock = 5 }
        };

        // LINQ Query Syntax
        var inStockProductsQuery = from p in products
                                   where p.Stock > 0
                                   select p;

        // LINQ Method Syntax
        var inStockProductsMethod = products.Where(p => p.Stock > 0);

        foreach (var product in inStockProductsMethod) // <- you can use Query or Method Syntax here
        {
            Console.WriteLine(product.Name);
        }
        int totalStock = products.Sum(p => p.Stock);
        Console.WriteLine($"Total Stock: {totalStock}");
        var productsByStockStatus = from p in products
                                    group p by p.Stock > 0 into g
                                    select new
                                    {
                                        InStock = g.Key,
                                        Products = g
                                    };

        foreach (var group in productsByStockStatus)
        {
            Console.WriteLine(group.InStock ? "In Stock:" : "Out of Stock:");
            foreach (var product in group.Products)
            {
                Console.WriteLine($"- {product.Name}");
            }
        }

        return Ok("done - see console");
    }

    [HttpGet("joining")]
    public ActionResult GetJoins()
    {
        var products = new List<DProduct>
        {
            new DProduct { Name = "Laptop", Stock = 10 },
            new DProduct { Name = "Smartphone", Stock = 0 },
            new DProduct { Name = "Tablet", Stock = 5 }
        };
        var orders = new List<Order>
        {
            new Order { OrderId = 1, ProductName = "Laptop" },
            new Order { OrderId = 2, ProductName = "Tablet" }
        };

        // Join Products with Orders
        var productOrders = from p in products
                            join o in orders on p.Name equals o.ProductName
                            select new { p.Name, o.OrderId };

        foreach (var po in productOrders)
        {
            Console.WriteLine($"Product: {po.Name}, Order ID: {po.OrderId}");
        }

        return Ok("done - see console");
    }


    [HttpGet("db")]
    public ActionResult GetDb()
    {
        var products = _salesContext.Products
                                 .Where(p => p.Price < 10)
                                 .ToList();
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Name}, {product.ProductCategory}, {product.Price}");
        }

        return Ok("done - see console");
    }


}