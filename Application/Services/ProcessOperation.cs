using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nico.Assistant.Interface;
using Nico.Assistant.Models;

namespace Application.Services
{
    public class ProcessOperation : IProcessOperation
    {
        public Task<ResponseNico> ExecuteOperation(ActionAgentModel process)
        {
            throw new NotImplementedException();
        }
    }
}
