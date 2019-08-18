using LocadoraAcmeApp.AcessoDados.Interfaces;
using LocadoraAcmeApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LocadoraAcmeApp.AcessoDados.Repositorios
{
    public class AluguelRepositorio : RepositorioGenerico<Aluguel>, IAluguelRepositorio
    {

        private readonly Contexto _contexto;

        public AluguelRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> VerificarReserva(string usuarioId, int carroId, string dataInicio, string dataFim)
        {
            return await _contexto.Alugueis.AnyAsync(a => a.UsuarioId == usuarioId && a.CarroId == carroId && a.Inicio == dataInicio && a.Fim == dataFim);
        }
    }
}
