﻿using Abp.EntityFrameworkCore;
using Acme.SimpleTaskApp.Persons;
using Acme.SimpleTaskApp.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Acme.SimpleTaskApp.EntityFrameworkCore
{
    public class SimpleTaskAppDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...
        public DbSet<Task> Tasks { get; set; }

        public DbSet<Person> Persons { get; set; }

        public SimpleTaskAppDbContext(DbContextOptions<SimpleTaskAppDbContext> options)
            : base(options)
        {

        }
    }
}
