using AutoMapper;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Domain.Models;
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
        private IGenericRepository<StudentGrade>? _grade;
        private IGenericRepository<TeacherClass>? _teacherClasses;



        private ITeacherRepository? _TeacherRepo;
        private IClassRepository? _classRepo;
        private IGradeRepository? _GradeRepo;
        private ITeacherClassRepository? _teacherClass;

        public IGenericRepository<Student> Students => _students ??= new GenericRepository<Student>(_context);
        public IGenericRepository<Teacher> Teachers => _teachers ??= new GenericRepository<Teacher>(_context);
        public IGenericRepository<Subject> Subjects => _subjects ??= new GenericRepository<Subject>(_context);
        public IGenericRepository<Class> Classes => _classes ??= new GenericRepository<Class>(_context);
        public IGenericRepository<Classroom> ClassRoomes => _classRoom ??= new GenericRepository<Classroom>(_context);
        public IGenericRepository<Attendance> Attendances => _attendance ??= new GenericRepository<Attendance>(_context);
        public IGradeRepository GradesReopsitory => _GradeRepo ??= new GradeRepository(_context);
        public IGenericRepository<TeacherClass> TeacherClasses => _teacherClasses ??= new GenericRepository<TeacherClass>(_context);



        public ITeacherRepository TeacherRepository => _TeacherRepo ??= new TeacherRepository(_context,_mapper);
        public IClassRepository classRepository    =>  _classRepo   ??= new ClassRepository(_context,_mapper);

        public IGradeRepository Grades => _GradeRepo ??= new GradeRepository(_context);

        public ITeacherClassRepository TeacherClass => _teacherClass ??= new TeacherClassRepository(_context,_mapper);

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
