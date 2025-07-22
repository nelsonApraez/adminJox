using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JOX.Assistant.Interface;
using JOX.Assistant.Models;

namespace Application.Services
{
    public class ProcessOperation : IProcessOperation
    {
        public Task<ResponseJOX> ExecuteOperation(ActionAgentModel process)
        {
            throw new NotImplementedException();
        }
    }
}
