using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configurations.Seed
{
   
    class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder.HasData(
                new Employee { Id = new Guid("290603c0-d0dd-4559-ba34-7579ca3da866"),
                    Name = "Ahmed rabea", Age=36 , Position="Manager" ,CompanyId = new Guid("7ab9dba8-32ba-4f79-90fd-fccb7465a0eb")
                },
                  new Employee
                  {
                      Id = new Guid("0bf027a2-9a38-41fd-b389-b73ab290d29b"),
                      Name = "Mohamed rabea",
                      Age = 36,
                      Position = "Manager",
                      CompanyId = new Guid("7ab9dba8-32ba-4f79-90fd-fccb7465a0eb")
                  }
            );
        }
    }
}
