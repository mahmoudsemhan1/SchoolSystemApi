using AutoMapper;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;
using SchoolSystem.Infrastructure.Repositories;

namespace SchoolSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolSystemDbContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(SchoolSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
          
        }

        private IGenericRepository<Student>? _students;
        private IGenericRepository<Teacher>? _teachers;
        private IGenericRepository<Subject>? _subjects;
        private IGenericRepository<Class>? _classes;
        private IGenericRepository<Classroom>? _classRoom;
        private IGenericRepository<Attendance>? _attendance;



        private ITeacherRepository? _TeacherRepo;
        private IClassRepository? _classRepo;

        public IGenericRepository<Student> Students => _students ??= new GenericRepository<Student>(_context);
        public IGenericRepository<Teacher> Teachers => _teachers ??= new GenericRepository<Teacher>(_context);
        public IGenericRepository<Subject> Subjects => _subjects ??= new GenericRepository<Subject>(_context);
        public IGenericRepository<Class> Classes => _classes ??= new GenericRepository<Class>(_context);
        public IGenericRepository<Classroom> ClassRoomes => _classRoom ??= new GenericRepository<Classroom>(_context);
        public IGenericRepository<Attendance> Attendances => _attendance ??= new GenericRepository<Attendance>(_context);



        public ITeacherRepository TeacherRepository => _TeacherRepo ??= new TeacherRepository(_context,_mapper);
        public IClassRepository classRepository    =>  _classRepo   ??= new ClassRepository(_context,_mapper);


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
