using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nico.Assistant.Models
{
    public class ActionAgentModel
    {
        public string name { get; set; } = "None";
        public string before { get; set; } = string.Empty;

        public string mode { get; set; } = "Rag";

        public string after { get; set; }
        public string descrip { get; set; } = string.Empty;

        public string message { get; set; } = "None";


        public Dictionary<string, string> parameters { get; set; } = new();
    }
}
