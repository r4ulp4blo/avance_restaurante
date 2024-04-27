using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace restaurante.SignalR
{
    public class EstadoHub : Hub
    {
        public async Task ActualizarEstado(int idDetalle, string nuevoEstado)
        {
            await Clients.All.SendAsync("EstadoActualizado", idDetalle, nuevoEstado);
        }
    }
}
