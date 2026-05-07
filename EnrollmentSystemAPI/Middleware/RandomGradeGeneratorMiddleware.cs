using Microsoft.AspNetCore.Http;

namespace EnrollmentSystemApi.Middleware;

public class RandomGradeGeneratorMiddleware(RequestDelegate next)
{
    private static readonly Random Random = new();

    public async Task InvokeAsync(HttpContext context)
    {
        var grade = Random.Next(75, 101);
        context.Response.Headers["GeneratedGrade"] = grade.ToString();
        await next(context);
    }
}
