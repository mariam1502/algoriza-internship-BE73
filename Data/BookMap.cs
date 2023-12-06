using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BookMap
    {
        public BookMap(EntityTypeBuilder<Book> entityBuilder)
        {
            entityBuilder.Property(b => b.CouponId).IsRequired(false);

            entityBuilder.HasOne(e => e.Time)
                .WithMany(t => t.Books)
                .HasForeignKey(b => b.TimeId)
                .OnDelete(DeleteBehavior.NoAction);

            entityBuilder.HasOne(e => e.Coupon)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CouponId)
                .OnDelete(DeleteBehavior.NoAction);

            entityBuilder.HasOne(e => e.Patient)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
