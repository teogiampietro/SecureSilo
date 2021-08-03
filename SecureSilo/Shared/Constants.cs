namespace SecureSilo.Shared
{
    public static class Constants
    {
        //Categorias suscripciones
        public const int CATEGORIA_BASE = 1;
        public const int CATEGORIA_STANDAR = 2;
        public const int CATEGORIA_PRO = 3;
        public const int CATEGORIA_PREMIUM = 4;

        //Descuentos
        public const double DESCUENTO_10 = 10;
        public const double DESCUENTO_15 = 15;

        //Estados Suscripcion
        public const string PAGADO = "PAGADO";
        public const string DEBE = "DEBE";
        public const string GENERADO = "GENERADO";

        //Formas de pago
        public const int FORMA_PAGO_EFECTIVO = 1;
        public const int FORMA_PAGO_TRANSFERENCIA = 2;
        public const int FORMA_PAGO_MERCADOPAGO = 3;
        public const int FORMA_PAGO_TARJETA_CREDITO = 4;

        //Estados
        public const string Ok = "OK";
        public const string SinEstado = "SIN_DATOS";
        public const string Advertencia = "ADVERTENCIA";
        public const string Alerta = "ALERTA";
        public const string Default = "DEFAULT";

        //Riesgos
        public const string Alto = "ALTO";
        public const string Medio = "MEDIO";
        public const string Bajo = "BAJO";
        public const string NoValue = "SIN_RIESGO";

        public const string idTelegram = "1809980038:AAH51WjegA2rQ27xs5EOiHxevDTtgNOPVu4";
        public const string nombreEmpresa = "AlarmSilo";
        public const string gmail = "@gmail.com";
        
    }

}
