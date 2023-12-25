using FluentValidation.AspNetCore;
using Mamba_Class.DAL;
using Mamba_Class.DTOs.MemberDto;
using Mamba_Class.MappingProfile;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(typeof(MemberUpdateDto).Assembly);
});

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlServer("Server=DESKTOP-0HH3DC0\\SQLEXPRESS;Database=MBizz-API-1;Trusted_Connection=True;TrustServerCertificate=true");

});

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
