using ExpenseManagementSystem1.Models;
using ExpenseManagementSystem1.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
//Add DbContext
builder.Services.AddDbContext<ExpenseMSystemContext>(ServiceLifetime.Transient);

//For Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ExpenseMSystemContext>();


// Add services to the container.
//For Authentication
builder.Services.AddAuthentication(Option =>
{
    Option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    Option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

})
    //Adding JwtBearer
    .AddJwtBearer(Option =>
    {
        Option.SaveToken = true;
        Option.RequireHttpsMetadata = false;
        Option.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    }) ;
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
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

//Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
