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

            // Crear una lista para almacenar los detalles de los pedidos por mesa
            var detallesPedidosPorMesa = new List<dynamic>();

            // Para cada mesa ocupada, obtener los detalles de los pedidos
            foreach (var mesa in listadoDeMesas)
            {
                var detallePedidos = _restauranteDbContext.detallePedido
                                        .Where(p => p.id_Mesa == mesa.id_Mesa)
                                        .ToList();

                // Agregar los detalles de los pedidos por mesa a la lista
                detallesPedidosPorMesa.Add(new
                {
                    Mesa = mesa,
                    DetallesPedidos = detallePedidos
                });
            }

            // Pasar los datos a la vista
            ViewData["DetallesPedidosPorMesa"] = detallesPedidosPorMesa;



            return View();



        }

       
        
    }
}
