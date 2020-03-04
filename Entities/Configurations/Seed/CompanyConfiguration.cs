using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configurations.Seed
{
    class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {

            builder.HasData(
                new Company { Id=new Guid("7ab9dba8-32ba-4f79-90fd-fccb7465a0eb") ,  Name="ITShare" , Country="Egypt" , Address="Nasr City"} ,
                new Company { Id = new Guid("0bf027a2-9a38-41fd-b389-b73ab290d29b"), Name = "ABC", Country = "USA", Address = "AY Haga" }
            );
        }
    }
}
