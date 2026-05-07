using Microsoft.AspNetCore.Builder;

namespace EnrollmentSystemApi.Middleware;

public static class RandomGradeGeneratorMiddlewareExtension
{
    public static IApplicationBuilder UseRandomGradeGenerator(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RandomGradeGeneratorMiddleware>();
    }
}
