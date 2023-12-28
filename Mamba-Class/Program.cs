using FluentValidation.AspNetCore;
using Mamba_Class.DAL;
using Mamba_Class.DTOs.MemberDto;
using Mamba_Class.MappingProfile;
using Microsoft.EntityFrameworkCore;
using MyBizz.Business.Services.Implementations;
using MyBizz.Business.Services.Interfaces;
using MyBizz.Core.Repositories.Interfaces;
using MyBizz.Data.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(typeof(MemberUpdateDto).Assembly);
});

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProfessionService , ProfessionService>();
builder.Services.AddScoped<IProfessionRepository , ProfessionRepository>();
builder.Services.AddScoped<IMemberRepository , MemberRepository>();
builder.Services.AddScoped<IMemberService , MemberService>();

builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlServer("Server=MSI;Database=MBizz-API-1;Trusted_Connection=True;TrustServerCertificate=true");

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
