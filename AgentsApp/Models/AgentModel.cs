using AgentsApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AgentsApp.Models
{
    /// <summary>
    /// Модель агента
    /// </summary>
    class AgentModel : IComparable<AgentModel>
    {
        public int Id { get; private set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string ImageToken { get; set; }

        public List<AgentModel> AgentModels { get; set; }

        /// <summary>
        /// Перевод Agent в AgentModel
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public static AgentModel AgentToAgentModel(Agent agent)
        {
            AgentModel agentModel = new AgentModel()
            {
                Id = agent.Id,
                Name = agent.Name,
                Email = agent.Name,
                ContactNumber = agent.ContactNumber,
                ImageToken = agent.ImageToken
            };

            return agentModel;
        }

        public Agent ToAgent()
        {
            return new Agent()
            {
                Id = this.Id,
                Name = this.Name,
                Email = this.Email,
                ContactNumber = this.ContactNumber,
                ImageToken = this.ImageToken
            };
        }

        /// <summary>
        /// Асинхронный перевод списка Agent в список AgentModel
        /// </summary>
        /// <param name="agents"></param>
        /// <returns></returns>
        public async Task<List<AgentModel>> AgentsListToAgentModelsList(List<Agent> agents)
        {
            return await Task.Run(() => _AgentsListToAgentModelsList(agents));
        }

        /// <summary>
        /// Асинхронная функция для перевода
        /// </summary>
        /// <param name="agents"></param>
        /// <returns></returns>
        private List<AgentModel> _AgentsListToAgentModelsList(List<Agent> agents)
        {
            AgentModels = new List<AgentModel>();
            foreach (var agent in agents)
            {
                
                AgentModels.Add(new AgentModel()
                {
                    Id = agent.Id,
                    Name = agent.Name,
                    Email = agent.Email,
                    ContactNumber = agent.ContactNumber,
                    ImageToken = agent.ImageToken
                });
            }
            AgentModels.Sort();
            return AgentModels;
        }

        /// <summary>
        /// Загрузка данных из БД
        /// </summary>
        /// <returns></returns>
        public static AgentModel LoadDataFromDataBase()
        {
            List<Agent> agents = new List<Agent>();
            using (var db = new AgentContext())
            {
                agents = db.Agents.ToList();
            }
            AgentModel agentModel = new AgentModel();
            agentModel._AgentsListToAgentModelsList(agents);
            return agentModel;
        }

        /// <summary>
        /// Реализация метода CompareTo для сортировки агентов по имени.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(AgentModel other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}