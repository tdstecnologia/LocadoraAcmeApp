using LocadoraAcmeApp.AcessoDados.Interfaces;
using LocadoraAcmeApp.Models;

namespace LocadoraAcmeApp.AcessoDados.Repositorios
{
    public class CarroRepositorio : RepositorioGenerico<Carro>, ICarroRepositorio
    {
        public CarroRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}
