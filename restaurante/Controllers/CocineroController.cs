﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurante.Models;
using System.Runtime.Intrinsics.Arm;

namespace restaurante.Controllers
{ 
    public class CocineroController : Controller
    {
        private readonly restauranteDbContext _restauranteDbContext;

        public CocineroController (restauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        // Acción para cambiar el estado del pedido
        [HttpPost]
        public IActionResult CambiarEstado(int id)
        {
            // Buscar el detalle del pedido en la base de datos por su ID
            var detallePedido = _restauranteDbContext.detallePedido.FirstOrDefault(d => d.id_Detalle == id);

            if (detallePedido == null)
            {
                // Si el detalle del pedido no se encuentra, devolver un error o realizar alguna otra acción
                return NotFound();
            }

            // Actualizar el estado del detalle del pedido
            if (detallePedido.estado == "Solicitado")
            {
                detallePedido.estado = "Preparando";
            }
            else if (detallePedido.estado == "Preparando")
            {
                detallePedido.estado = "Finalizado";
            }
            else
            {
                // Si ya está finalizado, no se realiza ningún cambio
                return Ok();
            }

            // Guardar los cambios en la base de datos
            _restauranteDbContext.SaveChanges();

            // Devolver una respuesta exitosa
            return Ok();
        }


        public IActionResult Index()
        {
            //var listadoDeMesas = (from m in _restauranteDbContext.mesa 
            //                      select new { 
            //                          id_Mesa = m.id_Mesa 
            //                      }).ToList();
            //ViewData["listadoDeMesas"] = listadoDeMesas;

            // Obtener listado de pedidos por mesa
            var listadoDePedidosPorMesa = (from e in _restauranteDbContext.detallePedido
                                           join p in _restauranteDbContext.plato on e.id_Plato equals p.id_Plato
                                           group new { e, p } by e.id_Mesa into g
                                           select new
                                           {
                                               id_Mesa = g.Key,
                                               Pedidos = g.Select(pedido => new
                                               {
                                                   id_Detalle = pedido.e.id_Detalle,
                                                   id_Plato = pedido.e.id_Plato,
                                                   precio =pedido.p.precio,
                                                   nombrePlato = pedido.p.nombre,
                                                   estado = pedido.e.estado,
                                                   cantidadPlato = pedido.e.cantidadPlato,
                                                   Comentarios = pedido.e.Comentarios
                                               }).ToList()
                                           }).ToList();

            ViewData["listadoDePedidosPorMesa"] = listadoDePedidosPorMesa;



            var listadoDePedidos = (from e in _restauranteDbContext.detallePedido 
                                    join m in _restauranteDbContext.plato on e.id_Plato equals m.id_Plato
                                    select new {
                                        id_Detalle = e.id_Detalle,
                                        id_Plato = e.id_Plato,
                                        nombrePlato = m.nombre,
                                        id_Mesa = e.id_Mesa,
                                        estado = e.estado,
                                        cantidadPlato = e.cantidadPlato,
                                        Comentarios = e.Comentarios
                                    }).ToList();



            ViewData["listadoDePedidos"] = listadoDePedidos;

            var listadoPlatosPorMesa = (from e in _restauranteDbContext.detallePedido
                                        join m in _restauranteDbContext.plato on e.id_Plato equals m.id_Plato
                                        select new { 
                                            nombrePlato = m.nombre,
                                            precio = m.precio
                                        });

            ViewData["listadoPlatosPorMesa"] = listadoPlatosPorMesa;

            return View();
        }


    }
}
