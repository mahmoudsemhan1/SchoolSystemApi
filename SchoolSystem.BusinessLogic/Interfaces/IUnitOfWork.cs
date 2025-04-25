
using SchoolSystem.Domain.Models;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Student> Students { get; }
        IGenericRepository<Teacher> Teachers { get; }
        IGenericRepository<Subject> Subjects { get; }
        IGenericRepository<Class> Classes { get; }
        IGenericRepository<Classroom> ClassRoomes { get; }
        IGenericRepository<Attendance> Attendances { get; }
        IGenericRepository<TeacherClass> TeacherClasses { get; }

        ITeacherRepository TeacherRepository { get; }
        IClassRepository classRepository { get; }
        IGradeRepository Grades { get; }
        ITeacherClassRepository TeacherClass { get; }

        Task<int> CompleteAsync();
    }
}
