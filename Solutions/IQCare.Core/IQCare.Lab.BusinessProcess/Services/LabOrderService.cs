using IQCare.Lab.BusinessProcess.Commands;
using IQCare.Lab.Core.Models;
using IQCare.Lab.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Lab.BusinessProcess.Services
{
    public interface ILabOrderService
    {
        Task<int> AddLabOrder(AddLabOrderCommand request);
    }

}
