using BackEndExchange.Model.PropositoGeneral;
using BackEndExchange.Model.Request;


namespace BackEndExchange.Services
{
    public interface ICompraService
    {

        public RespuestaModel add(ConfirmarCompraModel model);

        public string delete( FacturaModel model);
    }
}
