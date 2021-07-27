using SecureSilo.Shared;

namespace SecureSilo.Server.Helpers
{
    public class EstrategiaA : Strategy
    {
        public override double Total(double total)
        {
            var porcentaje = (total * Constants.DESCUENTO_15) / 100;
            total = total - porcentaje;
            return total;
        }
    }
}

