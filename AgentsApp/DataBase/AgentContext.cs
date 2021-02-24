using AgentsApp.Models;
using Microsoft.EntityFrameworkCore;


namespace AgentsApp.Database
{
    //База данных агентов
    class AgentContext : DbContext
    {
        //Хранимые в БД агенты
        public DbSet<AgentModel> Agents { get; set; }

        public AgentContext()
        {
            //Создание БД при ее отсутсвии
            Database.EnsureCreated();
        }

        //Конфигурация БД, создание SQLite базы с именем Agent.db
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName = Agent.db");
        }
    }
}
