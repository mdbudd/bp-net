﻿using Microsoft.EntityFrameworkCore;

namespace WebApi.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }

        public DbSet<EntityType>? EntityTypes { get; set; }
        public DbSet<Approval>? Approvals { get; set; }
    }
}