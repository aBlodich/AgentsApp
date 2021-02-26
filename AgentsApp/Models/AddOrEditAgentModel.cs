using AgentsApp.Commands;
using AgentsApp.Database;

namespace AgentsApp.Models
{
    /// <summary>
    /// Модель добавления или редактирования агентов
    /// </summary>
    class AddOrEditAgentModel
    {
        public string Token { get; set; } = null;

        /// <summary>
        /// Сохранение данных в базу данных
        /// </summary>
        /// <param name="agent">Записываемый агент</param>
        /// <param name="name">Имя агента</param>
        /// <param name="contactNumber">Номер телефона агента</param>
        /// <param name="email">Email агента</param>
        /// <param name="isEdit">Флаг режима, запись нового агента или его редактирование</param>
        public void SaveDataToDataBaseAsync(Agent agent, string name, string contactNumber, string email, bool isEdit)
        {
            using (var db = new AgentContext())
            {
                if (agent == null)
                {
                    agent = new Agent()
                    {
                        Name = name,
                        ContactNumber = contactNumber,
                        Email = email
                    };
                }
                else
                {
                    agent.Name = name;
                    agent.ContactNumber = contactNumber;
                    agent.Email = email;
                }
                if (Token != null)
                {
                    agent.ImageToken = Token;
                }
                if (isEdit)
                {
                    db.Agents.Update(agent);
                }
                else
                {
                    db.Agents.Add(agent);
                }
                db.SaveChanges();
            }
            Navigation.GoToMainPage();
        }
    }
}