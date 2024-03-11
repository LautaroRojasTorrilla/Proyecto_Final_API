# Proyecto Final API

Este proyecto representa una alternativa al desarrollo de una API robusta, diseñada para ser compatible con diversos sistemas de comercio electrónico u otras entidades similares. La API ofrece una variedad de endpoints que abarcan funcionalidades claves para facilitar la integración y el manejo eficiente de datos.

## Tecnologías Utilizadas

- ASP.NET 6
- Entity Framework
- SQL Server

## Características Principales

- **Arquitectura RESTful:** La API sigue los principios RESTful, utilizando HTTP y endpoints para gestionar recursos como productos, pedidos y clientes.

- **Compatibilidad con Comercio Electrónico:** Diseñada específicamente para integrarse con sistemas de comercio electrónico, la API ofrece funcionalidades esenciales para este entorno.

- **Estructura en Capas:** La aplicación se organiza en capas, separando responsabilidades para mejorar la escalabilidad y el mantenimiento a largo plazo.

- **Scaffolding:** Se ha utilizado scaffolding en ASP.NET 6 para generar automáticamente archivos y códigos base, acelerando el desarrollo inicial.

- **Manejo Eficiente de Datos:** La API utiliza modelos para definir entidades del dominio y aprovecha Entity Framework para interactuar con la base de datos, garantizando un manejo eficiente de los datos.

## Estructura del Proyecto

El proyecto sigue una arquitectura basada en capas para facilitar la modularidad y el mantenimiento. A continuación, se describen brevemente las principales capas:

1. **Models:**
   - En esta capa se definen las entidades del dominio.

2. **Database:**
   - Contiene la configuración y migraciones de Entity Framework para la base de datos.

3. **DTO:**
   - Aquí se encuentran los Objetos de Transferencia de Datos (DTO) que se utilizan para transferir información entre las capas.

4. **Mapper:**
   - Contiene los mapeadores que facilitan la conversión entre entidades del dominio y DTO.

5. **Service:**
   - La capa de servicio implementa la lógica de negocio y actúa como intermediario entre los controladores y la capa de acceso a datos.

6. **Controllers:**
   - En esta capa se encuentran los controladores de la API, que manejan las solicitudes HTTP y las respuestas correspondientes.

## Contribuciones y Autoría

Este proyecto es de autoría de Lautaro Rojas Torrilla y se proporciona tal cual. Se agradecen las contribuciones y sugerencias. 
Si deseas contribuir, sigue estos pasos:

1. Realiza un fork del proyecto.
2. Crea una rama con tu nueva característica (git checkout -b feature/nueva-caracteristica).
3. Haz commit de tus cambios (git commit -m 'Añade nueva característica').
4. Haz push a la rama (git push origin feature/nueva-caracteristica).
5. Abre un pull request.
