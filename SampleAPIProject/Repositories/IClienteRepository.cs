using SampleAPIProject.FirebaseContext;
using SampleAPIProject.Models;

namespace SampleAPIProject.Repositories
{
    public interface IClienteRepository: IFirestoreContext<Cliente>
    {
    }
}
