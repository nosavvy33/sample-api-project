using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleAPIProject.Administrators;
using SampleAPIProject.Models;
using System;

namespace SampleAPIProject.Controllers
{
    [ApiController]
    [Route("api/cliente/")]
    public class ClienteController : Controller
    {

        private readonly ILogger<ClienteController> _logger;
        private readonly IAdministrator<Cliente> clienteAdministrator;

        public ClienteController(ILogger<ClienteController> logger, IAdministrator<Cliente> clienteAdministrator)
        {
            _logger = logger;
            this.clienteAdministrator = clienteAdministrator;
        }

        /// <summary>
        /// Create client
        /// </summary>
        /// <param name="cliente">input client</param>
        /// <returns></returns>
        [HttpPost("creacliente")]
        public ActionResult Create([FromBody] Cliente cliente)
        {
            try
            {
                return Ok(this.clienteAdministrator.Add(cliente));
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, "Method: " + HttpContext.Request.Method.ToString() + "\nPath: " + HttpContext.Request.Path.ToString());
                return NotFound(exception);
            }
        }

        /// <summary>
        /// List clients
        /// </summary>
        /// <returns>List of clients</returns>
        [HttpGet("listclientes")]
        public ActionResult List() 
        {
            try
            {
                return Ok(this.clienteAdministrator.GetAll());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Method: " + HttpContext.Request.Method.ToString() + "\nPath: " + HttpContext.Request.Path.ToString());
                return NotFound(exception);
            }
        }

        /// <summary>
        /// Calculate Clients mean age and its standard deviation
        /// </summary>
        /// <returns>Clients Mean Age and its Standard Deviation</returns>
        [HttpGet("kpideclientes")]
        public ActionResult Kpi()
        {
            try
            {
                return Ok(this.clienteAdministrator.CalculateKPI());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Method: " + HttpContext.Request.Method.ToString() + "\nPath: " + HttpContext.Request.Path.ToString());
                return NotFound(exception);
            }
        }

    }
}
