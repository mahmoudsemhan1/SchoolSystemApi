
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Student> Students { get; }
        IGenericRepository<Teacher> Teachers { get; }
        ITeacherRepository TeacherRepository { get; }
        IClassRepository classRepository { get; }
        IGenericRepository<Subject> Subjects { get; }
        IGenericRepository<Class> Classes { get; }
        IGenericRepository<Classroom> ClassRoomes { get; }
        IGenericRepository<Attendance> Attendances { get; }

        Task<int> CompleteAsync();
    }
}
