namespace AgentsApp.Database
{
    /// <summary>
    /// Описание агента для базы данных
    /// </summary>
    class Agent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }

        public string ImageToken { get; set; }
    }
}
