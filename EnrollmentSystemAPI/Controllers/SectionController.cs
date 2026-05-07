using EnrollmentSystemApi.DTOs.Sections;
using EnrollmentSystemApi.Services.Sections;
using EnrollmentSystemApi.Services.Students;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentSystemApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionController(ISectionService sectionService, IStudentService studentService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<SectionResponseDTO>> GetAll()
    {
        return Ok(sectionService.GetAllSections());
    }

    [HttpGet("{sectionId:int}/student/{studentId:int}")]
    public IActionResult GetStudentInSection(int sectionId, int studentId)
    {
        var section = sectionService.GetSectionById(sectionId);
        if (section is null)
        {
            return NotFound($"Section {sectionId} not found.");
        }

        var student = studentService.GetStudentBySectionCodeAndId(section.Code, studentId);
        if (student is null)
        {
            return NotFound($"Student {studentId} not found in section {sectionId}.");
        }

        return Ok(student);
    }
}
