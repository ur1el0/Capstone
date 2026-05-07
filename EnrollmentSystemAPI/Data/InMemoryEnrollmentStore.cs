using EnrollmentSystemApi.Models;

namespace EnrollmentSystemApi.Data;

public class InMemoryEnrollmentStore
{
    public List<Section> Sections { get; } =
    [
        new Section { Id = 1, Code = "MB02" }
    ];

    public List<Student> Students { get; } =
    [
        new Student { Id = 1, FirstName = "Roose", LastName = "Zaño", Age = 21, SectionId = 1, Gender = "M", Grades = 85 },
        new Student { Id = 2, FirstName = "Mike", LastName = "Zaño", Age = 31, SectionId = 1, Gender = "M", Grades = 90 }
    ];
}
