using Application.Services.Abstractions;
using Infrastructure.Configurations;
using Infrastructure.Integrations.Invoices;
using Infrastructure.Integrations.QuickBooks;
using QuickBooksIntegration.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.Configure<QuickBooksSettings>(builder.Configuration.GetSection("QuickBooks"));
builder.Services.AddScoped<QuickBooksAuthService>();
//builder.Services.AddScoped<IInvoicesService, InvoicesService>();
builder.Services.AddScoped<IInvoicesService, MockInvoicesService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // angular url 
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuickBooks API v1");
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
// Use CORS
app.UseCors("AllowAllOrigins");

// Add the custom error-handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();