using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PatientMap
    {
        public PatientMap(EntityTypeBuilder<Patient> entityBuilder)
        {

            entityBuilder.HasKey(p => p.Id);
            entityBuilder.Property(p => p.Email).IsRequired().HasMaxLength(30);
            entityBuilder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(15);
            entityBuilder.Property(p => p.FirstName).IsRequired().HasMaxLength(30);
            entityBuilder.Property(p => p.LastName).IsRequired().HasMaxLength(30);

            entityBuilder.Property(p => p.DateOfBirth).IsRequired();
        }
    }
}
