using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Repositories;
using MongoDB.Driver;
using CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;

namespace CourseAppCourseService_Infrastructure.DbPattenrs
{
    public class UnitOfWork : IUnitOfWork
    { 
        public ICourseRepository Courses { get; set; }
        public ILessonRepository Lessons { get; set; }
        public IQuizRepository Quizzes { get; set; }
        
        private readonly IClientSessionHandle _session;
        private readonly List<Action> _operations;

        public IDisposable Session => _session;

        public UnitOfWork(IMongoClient mongoClient, ICourseDbContext context)
        {
            _session = mongoClient.StartSession();
            _operations = new List<Action>();

            Courses = new CourseRepository(context);
            Lessons = new LessonRepository(context);
            Quizzes = new QuizRepository(context);
        }

        public void AddOperation(Action operation)
        {
            _operations.Add(operation);
        }

        public void CleanOperations()
        {
            _operations.Clear();
        }

        public async Task CommitChangesAsync()
        {
            _session.StartTransaction();

            try
            {
                foreach (var operation in _operations)
                {
                    operation.Invoke();
                }

                await _session.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _session.AbortTransactionAsync();
                throw;
            }
            finally
            {
                CleanOperations();
            }
        }
    }
}
