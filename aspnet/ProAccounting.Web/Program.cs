
using Microsoft.EntityFrameworkCore;
using ProAccounting.Application.Interfaces;
using ProAccounting.Application.Services;
using ProAccounting.Application.Services.Clients;

namespace ProAccounting.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                        name: "Default",
                        policy => policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                    );
            });


            builder.Services.AddDbContext<ProAccountingDbContext>(opt => opt.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            builder.Services.AddTransient<IClientService, ClientsService>();
            //builder.Services.AddTransient<IInvoiceService, InvoiceService>();
            //builder.Services.AddTransient<ILedgerService, LedgerService>();
            //builder.Services.AddTransient<IPaymentService, PaymentService>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseCors("Default");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
