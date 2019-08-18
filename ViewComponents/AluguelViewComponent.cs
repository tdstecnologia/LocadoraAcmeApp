using LocadoraAcmeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.ViewComponents
{
    public class AluguelViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;

        public AluguelViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int CarroId)
        {
            return View(await _contexto.Carros.FirstOrDefaultAsync(c => c.CarroId == CarroId));
        }
    }
}
