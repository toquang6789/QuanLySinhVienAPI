using Microsoft.EntityFrameworkCore;
using QuanLySinhVienAPI;
using QuanLySinhVienAPI.Entities;
using QuanLySinhVienAPI.Service;

var builder = WebApplication.CreateBuilder(args);

var ConnectionStrings = builder.Configuration.GetConnectionString("quanlysinhvien");
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(ConnectionStrings);
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ISVService, SinhVienService>();
builder.Services.AddScoped<IGVService,GiangVienService>();
builder.Services.AddScoped<IKHService, KhoaHocService>();
builder.Services.AddScoped<IMHService, MonHocService>();
builder.Services.AddScoped<ILHService,LopHocService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
