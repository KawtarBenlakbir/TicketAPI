using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add the database context to use Entity Framework with SQL Server or PostgreSQL
builder.Services.AddDbContext<TicketContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// If using PostgreSQL, replace the above line with:
// options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

// Registering controllers
builder.Services.AddControllers();

// Configure CORS to allow your frontend to communicate with the backend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()   // Allow requests from any origin
              .AllowAnyHeader()   // Allow any headers in the requests
              .AllowAnyMethod()); // Allow all HTTP methods (GET, POST, PUT, DELETE)
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Provides detailed error pages during development
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll"); // Enable the "AllowAll" CORS policy

app.UseAuthorization();

app.MapControllers(); // Map controller routes (to handle API requests)

// Run the application
app.Run();
