using EnrollmentSystemApi.Data;
using EnrollmentSystemApi.Middleware;
using EnrollmentSystemApi.Services.Sections;
using EnrollmentSystemApi.Services.Students;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSingleton<InMemoryEnrollmentStore>();
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddSingleton<ISectionService, SectionService>();

var app = builder.Build();

app.UseMiddleware<RandomGradeGeneratorMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
