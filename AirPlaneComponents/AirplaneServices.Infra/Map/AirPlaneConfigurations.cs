using AirplaneServices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AirplaneServices.Infra.Map
{
    public class AirPlaneConfigurations : IEntityTypeConfiguration<AirPlane>
    {
        public void Configure(EntityTypeBuilder<AirPlane> builder)
        {
            builder.Property(x => x.Id).IsRequired(true);

            builder.Property(x => x.Code).IsRequired(true).HasColumnType("varchar(50)");

            builder.Property(x => x.ModelId).IsRequired(true);

            builder.HasOne(x => x.Model);

            builder.Property(x => x.NumberOfPassengers).IsRequired(true);

            builder.Property(x => x.CreationDate).IsRequired(true);

            builder.ToTable("AirPlane").HasKey(x => x.Id);


            builder.HasData(new AirPlane()
            {
                Id = Guid.Parse("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                Code = "1",
                ModelId = Guid.Parse("7f430a38-a6b2-4a8f-96d5-801725dfdfc1"),
                NumberOfPassengers = 111,
                CreationDate = DateTime.Now
            },
            new AirPlane()
            {
                Id = Guid.Parse("a714554f-f363-42f1-b41a-81ee85186622"),
                Code = "3B",
                ModelId = Guid.Parse("7f430a38-a6b2-4a8f-96d5-801725dfdfc3"),
                NumberOfPassengers = 167,
                CreationDate = DateTime.Now
            },
            new AirPlane()
            {
                Id = Guid.Parse("a714554f-f363-42f1-b41a-81ee85186661"),
                Code = "4",
                ModelId = Guid.Parse("7f430a38-a6b2-4a8f-96d5-801725dfdfc4"),
                NumberOfPassengers = 117,
                CreationDate = DateTime.Now
            });
        }
    }

}
