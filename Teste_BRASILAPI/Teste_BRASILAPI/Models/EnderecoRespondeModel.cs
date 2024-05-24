using System.Dynamic;
using System.Net;

namespace Teste_BRASILAPI.Models
{
    public class EnderecoRespondeModel<T> where T : class
    {
        public HttpStatusCode CodigoHttp {  get; set; }

        public T? DadosRetorno { get; set; }

        public ExpandoObject ErrorRetorno { get; set; }
    }
}
