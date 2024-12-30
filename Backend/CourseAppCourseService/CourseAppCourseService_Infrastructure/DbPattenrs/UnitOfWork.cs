using CourseAppCourseService_Application.Interfaces;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure.DbPattenrs
{
    public class UnitOfWork(IMongoClient mongoClient, ICourseDbContext context) : IUnitOfWork
    {
        private readonly IClientSessionHandle _session = mongoClient.StartSession();
        private readonly ICourseDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private List<Action> _operations { get; set; } = new();

        public IDisposable Session => _session;

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
