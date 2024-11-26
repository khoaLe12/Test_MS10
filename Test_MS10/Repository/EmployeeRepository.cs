using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Test_MS10.Entity;

namespace Test_MS10.Repository
{
    public interface IEmployeeRepository
    {
        Task<int> GetLatestOrder();
        IQueryable<Employee> Get(Expression<Func<Employee, bool>> where);
        Task AddAsync(Employee employee);
        void Update(Employee employee);
        void Remove(Employee employee);
        Task<bool> SaveChangesAsync();
        void Dispose();
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Employee> dbSet;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = _dbContext.Set<Employee>();
        }

        public IQueryable<Employee> Get(Expression<Func<Employee, bool>> where)
        {
            return dbSet.Where(where).AsNoTracking();
        }

        public async Task AddAsync(Employee employee)
        {
            await dbSet.AddAsync(employee);
        }

        public void Remove(Employee employee)
        {
            dbSet.Remove(employee);
        }

        public void Update(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Modified;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> GetLatestOrder()
        {
            DbContextFactory dbFactory = new DbContextFactory();
            using var dbContext = dbFactory.CreateDbContext(Array.Empty<string>());

            using var connection = dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT NEXT VALUE FOR Test_MS10.CountBy1";

            var result = await command.ExecuteScalarAsync();

            int nextValue = result != null ? Convert.ToInt32(result) : 0;

            return nextValue;
        }
    }
}
