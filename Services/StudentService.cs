using Microsoft.EntityFrameworkCore;
using StudentLibrary.Data;
using StudentLibrary.Entities;

namespace StudentLibrary.Service;

public class StudentService : IStudentService
{
    private readonly ILogger<StudentService> _logger;
    private readonly StudentDbContext _context;

    public StudentService (ILogger<StudentService> logger, StudentDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public async Task<(bool IsSucces, Exception e)> DeleteStudentAsync(Guid id)
    {
        try
        {
            var result = await GetStudentByIdAsync(id);
            _context.Students.Remove(result);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Student deleted");
            return (true, null);
        }
        catch (Exception e)
        {
            
            _logger.LogError($"Student isn't deleted");
            return (false, e);
        }
    }

    public async Task<List<Student>> GetAllStudentAsync()
        => await _context.Students.ToListAsync();

    public async Task<Student> GetStudentByIdAsync(Guid id)
    {
        return await _context.Students.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<(bool IsSucces, Exception e)> InsertStudentAsync(Student student)
    {
        try
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New Student is added");
            return (true, null);
        }
        catch(Exception e)
        {
            _logger.LogError($"New Student was not added");
            return (false, e);
        }
    }

    internal Task UpdateStudentAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool IsSucces, Exception e)> UpdateStudentAsync(Student student)
    {
        try
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Student Informations is updated {student.Id}");
            return (true, null);
        }
        catch(Exception e)
        {
            _logger.LogError($"Student informations isn't updated {student.Id}");
            return (false, e);
        }
    }
}