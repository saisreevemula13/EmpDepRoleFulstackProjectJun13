using EmpDepRoleFulstackProjectJun13.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EmpDepRoleFulstackProjectJun13.Data
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
