using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingSystemMobile.Models;

namespace VendingSystemMobile.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "VendingLocal.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<LocalServiceTask>().Wait();
        }

        // Метод для получения ВСЕХ задач из локальной БД (для MainPage)
        public Task<List<LocalServiceTask>> GetTasksAsync()
        {
            return _database.Table<LocalServiceTask>().ToListAsync();
        }

        // Метод для сохранения/обновления ОДНОЙ задачи (пригодится на DetailsPage)
        public Task<int> SaveTaskAsync(LocalServiceTask task)
        {
            if (task.Id != 0)
            {
                return _database.UpdateAsync(task);
            }
            else
            {
                return _database.InsertAsync(task);
            }
        }

        // --- ВОТ ОН, НЕДОСТАЮЩИЙ МЕТОД ---
        // Метод для массового сохранения СПИСКА задач (для LoginPage)
        public async Task SaveTasksAsync(List<LocalServiceTask> tasks)
        {
            // InsertOrReplaceAsync - очень удобная команда.
            // Если задача с таким Id уже есть, он ее обновит.
            // Если нет - добавит новую.
            foreach (var task in tasks)
            {
                await _database.InsertOrReplaceAsync(task);
            }
        }
    }
}
