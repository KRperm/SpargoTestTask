using AutoMapper;
using WebApi.Dto.Pharmacy;
using WebApi.Dto.Product;
using WebApi.Dto.Shipment;
using WebApi.Dto.Warehouse;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

namespace WebApi
{
    public class Programm
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddDbContext<DataContext>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var mapperConfiguration = new MapperConfiguration(x =>
            {
                x.CreateMap<Pharmacy, PharmacyDto>();
                x.CreateMap<Product, ProductDto>();
                x.CreateMap<Shipment, ShipmentDto>();
                x.CreateMap<Warehouse, WarehouseDto>();

                x.CreateMap<CreatePharmacyRequest, Pharmacy>();
                x.CreateMap<CreateProductRequest, Product>();
                x.CreateMap<CreateShipmentRequest, Shipment>();
                x.CreateMap<CreateWarehouseRequest, Warehouse>();
            });
            var mapper = mapperConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddScoped<ICompanyService, CompanyService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();
                context.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
