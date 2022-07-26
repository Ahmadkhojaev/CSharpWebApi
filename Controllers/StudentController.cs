using Microsoft.AspNetCore.Mvc;
using StudentLibrary.Mapper;
using StudentLibrary.Model;
using StudentLibrary.Service;

namespace StudentLibrary.Controller;

[ApiController]
[Route("/api/controller")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly StudentService _service;

    public StudentController(ILogger<StudentController> logger, StudentService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("addstudent")]
    public async Task <IActionResult> AddStudent([FromForm] StudentModel model)
    {
        if(ModelState.IsValid)
        {
            await _service.InsertStudentAsync(model.ToEntity());
            return Ok(model);
        }
        return BadRequest();
    }

    [HttpGet("/getallstudent")]
    public async Task<IActionResult> GetAllStudent()
    {
        var result = await _service.GetAllStudentAsync();
        return Ok(result);
    }

    [HttpGet("/getstudentbyid")]
    public async Task<IActionResult> GetStudentByIdAsync(Guid id)
    {
        var result = await _service.GetStudentByIdAsync(id);
        if(result != null)
        {
        return Ok(result);
        }
        return Ok("This student doesn't exist");
    }

    [HttpDelete("/deletestudentbyid")]
    public async Task<IActionResult> DeleteStudentById(Guid id)
    {
        var result = await _service.DeleteStudentAsync(id);
        return Ok("Student deleted");
    }

       [HttpPut("/updatestudentbyid")]
    public async Task<IActionResult> UpdateStudentById(StudentModel model, Guid id)
    {
        var student = await _service.GetStudentByIdAsync(id);
        student.FirstName = model.FirstName;
        student.LastName = model.LastName;

        var result = await _service.UpdateStudentAsync(student);
        var error = !result.IsSucces;
        var message = result.e is null ? "Success" : result.e.Message;
        return Ok(new {error, message});
    }
}