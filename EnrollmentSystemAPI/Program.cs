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

app.Use(async (context, next) =>
{
    if (context.Request.Method == "POST" && context.Request.Path.Value?.Contains("/api/section") == true)
    {
        var random = new Random();
        int grade = random.Next(75, 101);
        context.Items["GeneratedGrade"] = grade;
    }

    await next(context);
});

app.UseMiddleware<RandomGradeGeneratorMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
