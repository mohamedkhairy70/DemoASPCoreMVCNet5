using DemoASPCoreMVCNet5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoASPCoreMVCNet5.Repository
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
