using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DoctorMap
    {
        public DoctorMap(EntityTypeBuilder<Doctor> entityBuilder)
        {
            entityBuilder.HasKey(d => d.Id);
            entityBuilder.Property(d => d.Email).IsRequired().HasMaxLength(30);
            entityBuilder.Property(d => d.PasswordHash).IsRequired().HasMaxLength(15);
            entityBuilder.Property(d => d.FirstName).IsRequired().HasMaxLength(30);
            entityBuilder.Property(d => d.LastName).IsRequired().HasMaxLength(30);

            entityBuilder.Property(d => d.DateOfBirth).IsRequired();

        }

    }
}
