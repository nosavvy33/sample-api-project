using SampleAPIProject.FirebaseContext;
using SampleAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPIProject.Repositories
{
    public class ClienteRepository: IClienteRepository
    {

        string collectionName = "personas";
        BaseRepository repo;
        public ClienteRepository()
        {
            repo = new BaseRepository(collectionName);
        }

        public Cliente Add(Cliente record) => repo.Add(record);

        public bool Delete(Cliente record) => repo.Delete(record);

        public Cliente Get(Cliente record) => repo.Get(record);

        public List<Cliente> GetAll() => repo.GetAll<Cliente>();

        public bool Update(Cliente record) => repo.Update(record);
    }
}
