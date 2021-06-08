using System;
using System.Collections.Generic;
using System.Text;

namespace SecureSilo.Shared
{
    public static class Constants
    {

        //TODO: Parametrizar?
        public const double humedadValue = 14;
        public const double temperaturaValue = 24;
        public const double dioxidValue = 10;

        public const double humedadMaxValue = 16;
        public const double temperaturaMaxValue = 30;
        public const double dioxidMaxValue = 15;

        //Estados
        public const string Ok = "OK";
        public const string SinDatos = "SIN_DATOS";
        public const string Advertencia = "ADVERTENCIA";
        public const string Alerta = "ALERTA";

    }

}
