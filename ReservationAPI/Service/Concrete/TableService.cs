using ReservationAPI.Model;
using ReservationAPI.Repository.Abstract;
using ReservationAPI.Repository.Concrete;
using ReservationAPI.Service.Abstract;

namespace ReservationAPI.Service.Concrete
{
    public class TableService : ITableService
    {
        private ITableRepository _tableRepository;

        public TableService()
        {
            _tableRepository = new TableRepository();
        }

        public async Task<Table> GetTableByIdAsync(Guid id)
        {
            return await _tableRepository.GetTableByIdAsync(id);
        }

        public async Task<Table> GetTableByNumberAsync(string number)
        {
            return await _tableRepository.GetTableByNumberAsync(number);
        }

        public async Task<List<Table>> GetTablesAsync()
        {
            return await _tableRepository.GetTablesAsync();
        }

        public async Task<Table> GetOptimalTableAsync(int questCount, DateTime date)
        {
            return await _tableRepository.GetOptimalTableAsync(questCount, date.Date);
        }

        public async Task<Table> SaveTableAsync(Table table)
        {
            return await _tableRepository.SaveTableAsync(table);
        }

        public async Task<bool> DeleteTableByIdAsync(Guid id)
        {
            return await _tableRepository.DeleteTableByIdAsync(id);
        }
    }
}
