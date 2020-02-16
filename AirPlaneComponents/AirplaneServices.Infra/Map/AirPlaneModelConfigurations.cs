using AirplaneServices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AirplaneServices.Infra.Map
{
    public class AirPlaneModelConfigurations : IEntityTypeConfiguration<AirPlaneModel>
    {
        public void Configure(EntityTypeBuilder<AirPlaneModel> builder)
        {
            builder.Property(x => x.Id).IsRequired(true);

            builder.Property(x => x.Name).IsRequired(true).HasColumnType("varchar(50)");

            builder.ToTable("AirPlaneModel").HasKey(x => x.Id);

            builder.HasData(new AirPlaneModel()
            {
                Id = Guid.Parse("7f430a38-a6b2-4a8f-96d5-801725dfdfc1"),
                Name = "Airbus A300B1"
            }, new AirPlaneModel()
            {
                Id = Guid.Parse("7f430a38-a6b2-4a8f-96d5-801725dfdfc2"),
                Name = "Airbus A319"
            }, new AirPlaneModel()
            {
                Id = Guid.Parse("7f430a38-a6b2-4a8f-96d5-801725dfdfc3"),
                Name = "Boeing 737-100"
            }, new AirPlaneModel()
            {
                Id = Guid.Parse("7f430a38-a6b2-4a8f-96d5-801725dfdfc4"),
                Name = "Boeing CRJ-100"
            });
        }
    }

}
