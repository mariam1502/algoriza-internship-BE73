using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BaseEntityMap
    {
        public BaseEntityMap(EntityTypeBuilder<Patient> entityBuilder) 
        {
            entityBuilder.HasKey(b => b.Id);
            entityBuilder.Property(b => b.Email).IsRequired().HasMaxLength(30);
            entityBuilder.Property(b => b.PasswordHash).IsRequired().HasMaxLength(15);
            entityBuilder.Property(b => b.FirstName).IsRequired().HasMaxLength(30);
            entityBuilder.Property(b => b.LastName).IsRequired().HasMaxLength(30);
            entityBuilder.Property(b => b.DateOfBirth).IsRequired();
        }
    }
}
