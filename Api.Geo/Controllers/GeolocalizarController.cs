using Api.Geo.Context;
using Api.Geo.Models.Request;
using Api.Geo.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Geo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GeolocalizarController : ControllerBase
    {

        private readonly AppDbContext _context;

        public GeolocalizarController(AppDbContext context)
        {
            _context = context;
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
                    //_rabbitManagement.PublishMessage(pedido);
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
