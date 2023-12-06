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

builder.Services.AddDbContext<ApplicationContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("sqlDB")));


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();


builder.Services.AddTransient(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

//builder.Services.AddTransient<ICouponService, CouponService>();


builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddTransient<IPatientService, PatientService>();


builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder();


//builder.Services.AddAuthentication().AddFacebook(facebookoptions =>
//{
//    facebookoptions.AppId = builder.Configuration["authentication:facebook:382368500806561"];
//    facebookoptions.AppSecret = builder.Configuration["authentication:facebook:29bd1b9d86bff65b14f5b1f40309ec0e"];
//});







var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

}
app.UseStaticFiles();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

//app.MapControllers();
//app.MapIdentityApi<IdentityUser>();

app.Run();
