using ReservationAPI.Model;

namespace ReservationAPI.Service.Abstract
{
    public interface ITableService
    {
        Task<Table> GetTableByIdAsync(Guid id);
        Task<Table> GetTableByNumberAsync(string number);
        Task<List<Table>> GetTablesAsync();
        Task<Table> GetOptimalTableAsync(int questCount, DateTime date);
        Task<Table> SaveTableAsync(Table table);
        Task<bool> DeleteTableByIdAsync(Guid id);
    }
}
