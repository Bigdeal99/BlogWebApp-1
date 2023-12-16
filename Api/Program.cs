using api.Middleware;
using infrastructure;
using infrastructure.Repositories;
using service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString,
        dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());
}

if (builder.Environment.IsProduction())
{
    builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString);
}


builder.Services.AddSingleton<BlogRepository>();
builder.Services.AddSingleton<CommentRepository>();
builder.Services.AddSingleton<AdminRepository>();
builder.Services.AddSingleton<CategoryRepository>();

builder.Services.AddSingleton<BlogService>();
builder.Services.AddSingleton<AdminService>();
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<CommentService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//var frontEndRelativePath = "./../frontend/www";
//builder.Services.AddSpaStaticFiles(conf => conf.RootPath = frontEndRelativePath);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

//app.UseSpaStaticFiles();


app.MapControllers();
app.UseMiddleware<GlobalExceptionHandler>();
app.Run();