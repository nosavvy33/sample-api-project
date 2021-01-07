using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPIProject.FirebaseContext
{
    public interface IFirestoreContext<T>
    {
        T Get(T record);
        List<T> GetAll();
        T Add(T record);
        bool Update(T record);
        bool Delete(T record);
    }
}
