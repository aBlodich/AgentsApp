using Microsoft.EntityFrameworkCore;

namespace AgentsApp.Database
{
    /// <summary>
    /// Создает БД, содержащуюю агентов
    /// </summary>
    class AgentContext : DbContext
    {
        /// <summary>
        /// Хранимые в БД агенты
        /// </summary>
        public DbSet<Agent> Agents { get; set; }

        public AgentContext()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Конфигурация БД, создание SQLite базы с именем Agent.db
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName = Agent.db");
        }
    }
}