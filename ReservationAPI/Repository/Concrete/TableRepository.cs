using Microsoft.EntityFrameworkCore;
using ReservationAPI.Model;
using ReservationAPI.Repository.Abstract;

namespace ReservationAPI.Repository.Concrete
{
    public class TableRepository : ITableRepository
    {
        public async Task<Table> GetTableByIdAsync(Guid id)
        {
            using (var context = new ReservationApiDbContext())
            {
                return await context.Tables
                    .FindAsync(id);
            }
        }

        public async Task<Table> GetTableByNumberAsync(string number)
        {
            using (var context = new ReservationApiDbContext())
            {
                return await context.Tables
                    .Where(x => x.Number.Equals(number))
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<List<Table>> GetTablesAsync()
        {
            using (var context = new ReservationApiDbContext())
            {
                return await context.Tables
                    .AsNoTracking()
                    .OrderBy(x => x.Number)
                    .ToListAsync();
            }
        }

        public async Task<Table> GetOptimalTableAsync(int questCount, DateTime date)
        {
            using (var context = new ReservationApiDbContext())
            {
                return await context.Tables
                    .AsNoTracking()
                    .Include(x => x.Reservations)
                    .Where(x => x.Capacity >= questCount)
                    .Where(x => !x.Reservations.Any(x => x.Date.Equals(date)))
                    .OrderBy(x => x.Capacity)
                    .ThenBy(x => x.Number)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<Table> SaveTableAsync(Table table)
        {
            using (var context = new ReservationApiDbContext())
            {
                await context.Tables.AddAsync(table);
                await context.SaveChangesAsync();
                return table;
            }
        }

        public async Task<bool> DeleteTableByIdAsync(Guid id)
        {
            using (var context = new ReservationApiDbContext())
            {
                await context.Tables
                    .Where(x => x.Id == id)
                    .ExecuteDeleteAsync();
                return true;
            }
        }
    }
}
