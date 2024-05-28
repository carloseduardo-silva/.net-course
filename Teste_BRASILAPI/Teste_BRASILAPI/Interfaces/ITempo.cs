using Teste_BRASILAPI.Models;

namespace Teste_BRASILAPI.Interfaces
{
    public interface ITempo
    {

        Task<TempoModel> BuscarCidade(string cidade);

        Task<Clima> Previsao(int cityCode);
    }
}
