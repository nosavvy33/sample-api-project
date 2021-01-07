using SampleAPIProject.Models;
using SampleAPIProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleAPIProject.Administrators
{
    public class Administrator : IAdministrator<Cliente>
    {

        private readonly IClienteRepository clienteRepository;

        public Administrator(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public Cliente Add(Cliente record)
        {
            try
            {
                
                TimeSpan timeDiff = DateTime.Now.Date - record.Nacimiento;
                record.Edad = new DateTime(timeDiff.Ticks).Year;
                return this.clienteRepository.Add(record);
            }
            catch (Exception exception) 
            {
                throw exception;
            }
        }

        public KPICliente CalculateKPI()
        {
            try
            {
                var clients = this.clienteRepository.GetAll();

                var averageAge = clients.Average(client => client.Edad);

                double sumOfSquaresOfDifferences = clients.Select(val => (val.Edad - averageAge) * (val.Edad - averageAge)).Sum();
                double standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / clients.Count);

                return new KPICliente() { EdadPromedio = averageAge, DesviacionEstandar = standardDeviation };
            }
            catch (Exception exception) 
            {
                throw exception;
            }
        }

        public bool Delete(Cliente record)
        {
            try
            {
                return this.clienteRepository.Delete(record);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public Cliente Get(Cliente record)
        {
            try
            {
                return this.clienteRepository.Get(record);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<Cliente> GetAll()
        {
            try
            {
                return this.clienteRepository.GetAll();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool Update(Cliente record)
        {
            try
            {
                return this.clienteRepository.Update(record);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
