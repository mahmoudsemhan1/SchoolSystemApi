
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

        Task<int> CompleteAsync();
    }
}
