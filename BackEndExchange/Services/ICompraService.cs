using BackEndExchange.Model.Request;


namespace BackEndExchange.Services
{
    public interface ICompraService
    {

        public void add(FacturaModel model);

        public string delete( FacturaModel model);
    }
}