using ReservationAPI.Model;

namespace ReservationAPI.Repository.Abstract
{
    public interface ITableRepository
    {
        Task<Table> GetTableByIdAsync(Guid id);
        Task<Table> GetTableByNumberAsync(string number);
        Task<List<Table>> GetTablesAsync();
        Task<Table> GetOptimalTableAsync(int questCount, DateTime date);
        Task<Table> SaveTableAsync(Table table);
        Task<bool> DeleteTableByIdAsync(Guid id);
    }
}
