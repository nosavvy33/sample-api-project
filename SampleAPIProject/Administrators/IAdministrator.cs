using SampleAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPIProject.Administrators
{
    public interface IAdministrator<T>
    {
        T Get(T record);
        List<T> GetAll();
        T Add(T record);
        bool Update(T record);
        bool Delete(T record);
        KPICliente CalculateKPI();
    }
}
