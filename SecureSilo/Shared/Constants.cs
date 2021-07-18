namespace SecureSilo.Shared
{
    public static class Constants
    {
        enum EnumPagos
        {
           Pagado,
           Debe,
           Generado
        }
        public const string PAGADO = "PAGADO";
        public const string DEBE = "DEBE";
        public const string GENERADO = "GENERADO";

        public const string FORMA_DE_PAGO_MERCADOPAGO = "MERCADOPAGO";
        public const string FORMA_DE_PAGO_TRANSFERENCIA_EFECTIVO = "TRANS/EFT";
        public const string FORMA_DE_PAGO_CREDITO = "TARJETA_CREDITO";

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
    }

}
