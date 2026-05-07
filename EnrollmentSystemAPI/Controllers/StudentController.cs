using EnrollmentSystemApi.DTOs.Students;
using EnrollmentSystemApi.Services.Sections;
using EnrollmentSystemApi.Services.Students;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentSystemApi.Controllers;

[ApiController]
[Route("api/section/{sectionId}/student")]
public class StudentController(IStudentService studentServices, ISectionService sectionService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<StudentResponseDTO>> GetAll(int sectionId)
    {
        var section = sectionService.GetSectionById(sectionId);
        if (section is null) return NotFound();
        var students = studentServices.GetStudentsBySectionCode(section.Code);
        return Ok(students);
    }

    [HttpGet("{studentId:int}")]
    public IActionResult GetById(int sectionId, int studentId)
    {
        var section = sectionService.GetSectionById(sectionId);
        if (section is null) return NotFound();
        var student = studentServices.GetStudentBySectionCodeAndId(section.Code, studentId);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpGet("search")]
    public ActionResult<List<StudentResponseDTO>> Search(
        int sectionId,
        [FromQuery] string? firstName,
        [FromQuery] string? lastName,
        [FromQuery] string? gender,
        [FromQuery] int? age)
    {
        var section = sectionService.GetSectionById(sectionId);
        if (section is null) return NotFound();
        var students = studentServices.GetStudentsBySectionCode(section.Code, firstName, lastName, gender, age);
        return Ok(students);
    }

    [HttpPost]
    public IActionResult Create(int sectionId, [FromBody] StudentCreateDTO dto)
    {
        var section = sectionService.GetSectionById(sectionId);
        if (section is null) return NotFound();

        if (HttpContext.Items.TryGetValue("GeneratedGrade", out var gradeValue) && gradeValue is int grade)
        {
            dto.GeneratedGrade = grade;
        }

        var created = studentServices.CreateInSection(section.Code, dto);
        if (created is null) return BadRequest();
        return CreatedAtAction(nameof(GetById), new { sectionId, studentId = created.Id }, created);
    }

    [HttpPut("{studentId:int}")]
    public IActionResult Update(int sectionId, int studentId, [FromBody] StudentUpdateDTO dto)
    {
        var section = sectionService.GetSectionById(sectionId);
        if (section is null) return NotFound();
        var ok = studentServices.UpdateInSection(section.Code, studentId, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpPatch("{studentId:int}")]
    public IActionResult Patch(int sectionId, int studentId, [FromBody] StudentPatchDTO dto)
    {
        var section = sectionService.GetSectionById(sectionId);
        if (section is null) return NotFound();
        var ok = studentServices.PatchInSection(section.Code, studentId, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{studentId:int}")]
    public IActionResult Delete(int sectionId, int studentId)
    {
        var section = sectionService.GetSectionById(sectionId);
        if (section is null) return NotFound();
        var ok = studentServices.DeleteInSection(section.Code, studentId);
        return ok ? NoContent() : NotFound();
    }
}
