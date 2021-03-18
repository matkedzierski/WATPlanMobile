using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using WATPlanMobile.Models;

namespace WATPlanMobile.Controllers
{
    public class PlanyDB
    {
        private readonly SQLiteAsyncConnection database;

        public PlanyDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<PlanModel>().Wait();
        }

        public Task<List<PlanModel>> GetPlansAsync()
        {
            //Get all notes.
            return database.Table<PlanModel>().ToListAsync();
        }

        public Task<PlanModel> GetPlanAsync(string id)
        {
            // Get a specific note.
            return database.Table<PlanModel>()
                .Where(plan => plan.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SavePlanAsync(PlanModel plan)
        {
            return database.InsertOrReplaceAsync(plan);
        }

        public Task<int> DeletePlanAsync(PlanModel plan)
        {
            return database.DeleteAsync(plan);
        }
    }
}