using Microsoft.AspNetCore.Mvc;
using restaurante.Models;

namespace restaurante.Controllers
{
    public class CajeroController : Controller
    {
        private readonly restauranteDbContext _restauranteDbContext;

        public CajeroController(restauranteDbContext context)
        {
            _restauranteDbContext = context;
        }

        public IActionResult Index()
        {

            var listadoDeMesas = (from m in _restauranteDbContext.mesa
                                  where m.estado == "ocupada"
                                  select m
                                   ).ToList() ;

            ViewData["listadoDeMesas"] = listadoDeMesas;
            var detallesPedidos = (from m in _restauranteDbContext.mesa
                                   join p in _restauranteDbContext.detallePedido on m.id_Mesa equals p.id_Mesa
                                   join f in _restauranteDbContext.factura on p.id_Detalle equals f.id_Detalle
                                   join c in _restauranteDbContext.caja on f.id_Caja equals c.id_Caja
                                   select m
                                  ).ToList();


            return View();



        }

       
        
    }
}
