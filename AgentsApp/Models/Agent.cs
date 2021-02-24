using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentsApp.Models
{
    class Agent : IComparable<Agent>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }

        public string ImageToken { get; set; }

        public int CompareTo(Agent other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
