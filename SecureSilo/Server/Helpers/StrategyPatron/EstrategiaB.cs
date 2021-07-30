using SecureSilo.Shared;

namespace SecureSilo.Server.Helpers
{
    public class EstrategiaB : Strategy
    {
        public override double Total(double total)
        {
            var porcentaje = (total * Constants.DESCUENTO_10) / 100;
            total = total - porcentaje;
            return total;
        }
    }
}
