# SecureSilo

## Resumen
SecureSilo es un sistema de seguridad y control sobre silos bolsa. 
Permite obtener mediciones diarias gracias a dispositivos incorporados a los silos bolsa, mediante los cuales a travez de actualizaciones via SMS-3G el sistema se nutre de información en tiempo real.

También cuenta con sensores de movimiento, por lo cual controla la integridad del grano, ya que si se detecta alguna anomalía en ellos, enviará mensajes de alarma y llamará a la lista de números que el ciente desea.

Otro módulo importante para los administradores, es el módulo de suscripciones, donde podrás generar anualmente ordenes de pago para los usuarios, notificando por email donde y cómo pagar la mensualidad. Ya que de no hacerlo el sistema deniega el acceso al usuario, aunque sigue tomando las mediciones diarias para una futura regularidad de la suscripción.

## Tecnologías aplicadas

- .NET Core 3.1
- SQL Server
- Entity Framework Core 3.1
- AspNetCore
- Microsoft Indentity
- Swagger
- TelegramBot
- MailKit

- Razor pages 3.0
- WA - WebAssembly
- Bootstrap
