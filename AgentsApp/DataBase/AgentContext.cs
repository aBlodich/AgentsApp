using AgentsApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentsApp.DataBase
{
    //База данных агентов
    class AgentContext : DbContext
    {
        //Хранимые в БД агенты
        public DbSet<Agent> Agents { get; set; }

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
