using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repo;
using Service;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("sqlDB")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddTransient(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IDoctorService, DoctorService>();

//builder.Services.AddTransient<ICouponService, CouponService>();


builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddTransient<IPatientService, PatientService>();







var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
