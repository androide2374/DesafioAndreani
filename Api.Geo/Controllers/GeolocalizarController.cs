using Api.Geo.Configuration;
using Api.Geo.Context;
using Api.Geo.Models.Request;
using Api.Geo.Models.Tables;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Geo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GeolocalizarController : ControllerBase
    {

        private readonly ILogger<GeolocalizarController> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly RabbitMqConfiguration _rabbit;
        private readonly RabbitManagement _rabbitManagement;

        public GeolocalizarController(ILogger<GeolocalizarController> logger,
            IConfiguration configuration,
            AppDbContext context,
            RabbitMqConfiguration rabbit,
            RabbitManagement rabbitManagement)
        {
            _rabbitManagement = rabbitManagement;
            _logger = logger;
            _configuration = configuration;
            _context = context;
            _rabbit = rabbit;
        }

        [HttpPost]
        public ActionResult Post(GeolocalizarRequest model)
        {
            try
            {
                PedidoGeo pedido = new PedidoGeo()
                {
                    Calle = model.Calle,
                    Ciudad = model.Ciudad,
                    CP = model.CP,
                    Numero = model.Numero,
                    Pais = model.Pais,
                    Provincia = model.Provincia
                };
                if (ModelState.IsValid)
                {
                    _context.PedidoGeo.Add(pedido);
                    _context.SaveChanges();
                    _rabbitManagement.PublishMessage(pedido);
                    return StatusCode(202, new { id = pedido.Id });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(404, "Error");
            }
            return StatusCode(404, "Error");

        }
    }
}
