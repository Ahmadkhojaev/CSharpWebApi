using Microsoft.EntityFrameworkCore;
using StudentLibrary.Entities;

namespace StudentLibrary.Data;

public class StudentDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    public StudentDbContext(DbContextOptions<StudentDbContext> options)
    : base(options){}
}