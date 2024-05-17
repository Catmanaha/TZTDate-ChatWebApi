using System.Reflection;
using TZTDate.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddResponseCompression(opts =>
{
   opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
         new[] { "application/octet-stream" });
});
builder.Services.InitResponse();
builder.Services.Inject();
builder.Services.InitSwagger();
builder.Services.InitAuthentication(builder.Configuration);
builder.Services.Configure(builder.Configuration);
builder.Services.InitDbContext(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(configurations => configurations.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("TZTDate-ChatWebApi")));

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddScoped<ValidationFilterAttribute>();

builder.Services.AddSignalR();

builder.Services.Configure<ApiBehaviorOptions>(
    options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
  options.AddPolicy("BlazorWasmPolicy", corsBuilder =>
  {
    corsBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
  });
});

var app = builder.Build();

app.MapHub<ChatHub>("/chat");
app.MapHub<NotificaitionHub>("/notifications");
app.UseSwagger();
app.UseSwaggerUI();
app.UseResponseCompression();
app.UseCors("BlazorWasmPolicy");
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
