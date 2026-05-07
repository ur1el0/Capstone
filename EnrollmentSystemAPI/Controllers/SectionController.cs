using EnrollmentSystemApi.DTOs.Sections;
using EnrollmentSystemApi.Services.Sections;
using EnrollmentSystemApi.Services.Students;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentSystemApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionController(ISectionService sectionsService, IStudentService studentServices) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<SectionResponseDTO>> GetAll()
    {
        return Ok(sectionsService.GetAllSections());
    }

    [HttpGet("{sectionId:int}/student/{studentId:int}")]
    public IActionResult GetStudentInSection(int sectionId, int studentId)
    {
        var section = sectionsService.GetSectionById(sectionId);
        if (section is null)
        {
            return NotFound($"Section {sectionId} not found.");
        }

        var student = studentServices.GetStudentBySectionCodeAndId(section.Code, studentId);
        if (student is null)
        {
            return NotFound($"Student {studentId} not found in section {sectionId}.");
        }

        return Ok(student);
    }
}
