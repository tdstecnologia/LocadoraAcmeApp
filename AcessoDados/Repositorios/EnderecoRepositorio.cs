using LocadoraAcmeApp.AcessoDados.Interfaces;
using LocadoraAcmeApp.Models;

namespace LocadoraAcmeApp.AcessoDados.Repositorios
{
    public class EnderecoRepositorio : RepositorioGenerico<Endereco>, IEnderecoRepositorio
    {
        public EnderecoRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}
