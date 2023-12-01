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
            entityBuilder.Property(b => b.specialization).IsRequired().HasMaxLength(20);

        }

    }
}
