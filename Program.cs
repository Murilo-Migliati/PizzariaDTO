using Microsoft.EntityFrameworkCore;
using Pizzaria;
using Pizzaria.Data;
using Pizzaria.Repositories;
using Pizzaria.Services;
using Pizzaria.Dto;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<PizzariaContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IGenericService<ClienteDto>, ClienteService>();
builder.Services.AddScoped<IGenericService<PizzasDto>, PizzasService>();
builder.Services.AddScoped<IGenericService<VendaDto>, VendasService>();
builder.Services.AddScoped<IGenericService<PizzasVendaDto>, PizzasVendasService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
