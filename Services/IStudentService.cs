using StudentLibrary.Entities;

namespace StudentLibrary.Service;

public interface IStudentService
{
    Task<Student> GetStudentByIdAsync(Guid id);
    Task<List<Student>> GetAllStudentAsync();

    Task<(bool IsSucces, Exception e)> InsertStudentAsync(Student student);
    Task<(bool IsSucces, Exception e)> UpdateStudentAsync(Student student);
    Task<(bool IsSucces, Exception e)> DeleteStudentAsync(Guid id);
}