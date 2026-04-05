# Backend CSharp - API REST con ASP.NET Core

API REST desarrollada con ASP.NET Core 8 para gestionar cervezas y marcas, con arquitectura por capas (Controller-Service-Repository), validaciones con FluentValidation y persistencia con Entity Framework Core sobre SQL Server.

Adicionalmente, el proyecto incluye endpoints de ejemplo para operaciones matematicas, personas en memoria y consumo de API externa de posts.

## Tabla de contenido

1. [Descripcion del proyecto](#descripcion-del-proyecto)
2. [Tecnologias](#tecnologias)
3. [Arquitectura y estructura](#arquitectura-y-estructura)
4. [Requisitos previos](#requisitos-previos)
5. [Configuracion](#configuracion)
6. [Ejecucion local](#ejecucion-local)
7. [Migraciones de base de datos](#migraciones-de-base-de-datos)
8. [Documentacion y prueba de endpoints](#documentacion-y-prueba-de-endpoints)
9. [Endpoints principales](#endpoints-principales)
10. [Mejoras recomendadas](#mejoras-recomendadas)

## Descripcion del proyecto

Este backend expone endpoints para:

- CRUD parcial de cervezas (listar, obtener por id, crear, actualizar).
- CRUD parcial de marcas (listar, obtener por id, crear, actualizar).
- Consulta de posts desde JSONPlaceholder (servicio externo).
- Endpoints de ejemplo para personas y operaciones numericas.

La capa de datos utiliza EF Core con SQL Server y ya incluye migraciones en el repositorio.

## Tecnologias

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core 8
- SQL Server
- FluentValidation
- Swagger / OpenAPI (Swashbuckle)
- Inyeccion de dependencias nativa de ASP.NET Core

## Arquitectura y estructura

Estructura principal del workspace:

- [Backend.sln](Backend.sln): solucion principal.
- [Backend](Backend): proyecto Web API.
- [Postman](Postman): coleccion para pruebas manuales.

Estructura dentro de Backend:

- [Backend/Controllers](Backend/Controllers): endpoints HTTP.
- [Backend/Services](Backend/Services): logica de negocio y orquestacion.
- [Backend/Repository](Backend/Repository): acceso a datos.
- [Backend/Models](Backend/Models): entidades y DbContext.
- [Backend/DTOs](Backend/DTOs): contratos de entrada/salida.
- [Backend/Validators](Backend/Validators): reglas de validacion.
- [Backend/Migrations](Backend/Migrations): historial de migraciones EF Core.
- [Backend/Program.cs](Backend/Program.cs): configuracion de servicios y pipeline.

## Requisitos previos

- SDK de .NET 8 instalado.
- Docker Desktop (recomendado para macOS).
- SQL Server disponible (local o remoto) si no usas Docker.
- (Opcional) herramienta EF Core CLI:

```bash
dotnet tool install --global dotnet-ef
```

Verificacion rapida:

```bash
dotnet --version
dotnet ef --version
```

## Configuracion

### 0) Levantar SQL Server con Docker (recomendado en macOS)

Desde la raiz del repositorio:

```bash
docker compose up -d
```

Esto usa [docker-compose.yml](docker-compose.yml) y expone SQL Server en `localhost:1433`.

### 1) Cadena de conexion

Configura la cadena en [Backend/appsettings.json](Backend/appsettings.json) dentro de ConnectionStrings -> StoreConnection.

Ejemplo actual del proyecto:

```json
"StoreConnection": "Server=localhost,1433;Database=STORE;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;Encrypt=False;"
```

Si usas autenticacion SQL, ajusta usuario y password segun tu entorno.

### 2) Entorno de desarrollo

El perfil de lanzamiento usa ASPNETCORE_ENVIRONMENT=Development y habilita Swagger automaticamente en ese entorno.

Puertos definidos en [Backend/Properties/launchSettings.json](Backend/Properties/launchSettings.json):

- HTTP: http://localhost:5085
- HTTPS: https://localhost:7055

## Ejecucion local

Desde la raiz del repositorio:

```bash
dotnet restore
dotnet build
dotnet run --project Backend/Backend.csproj
```

Al iniciar en Development, abre Swagger en:

- http://localhost:5085/swagger
- https://localhost:7055/swagger

## Migraciones de base de datos

Aplicar migraciones existentes:

```bash
dotnet ef database update --project Backend/Backend.csproj
```

Si te falla `dotnet ef` con "You must install .NET to run this application" en macOS (caso comun por arquitectura x64/arm64), ejecuta:

```bash
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef
dotnet ef --version
```

Si persiste, exporta DOTNET_ROOT y vuelve a cargar la shell:

```bash
echo 'export DOTNET_ROOT=$(dirname $(dirname $(which dotnet)))' >> ~/.zshrc
echo 'export PATH=$PATH:$HOME/.dotnet/tools' >> ~/.zshrc
source ~/.zshrc
```

Crear una nueva migracion (si haces cambios en entidades):

```bash
dotnet ef migrations add NombreMigracion --project Backend/Backend.csproj
dotnet ef database update --project Backend/Backend.csproj
```

## Documentacion y prueba de endpoints

- Swagger UI para explorar y ejecutar endpoints.
- Archivo HTTP de ejemplo: [Backend/Backend.http](Backend/Backend.http).
- Coleccion Postman: [Postman/Backend_CSharp.postman_collection.json](Postman/Backend_CSharp.postman_collection.json).

## Endpoints principales

Base URL local:

- http://localhost:5085
- https://localhost:7055

### Beer

- GET /api/Beer
- GET /api/Beer/{id}
- POST /api/Beer
- PUT /api/Beer/{id}

### Brand

- GET /api/Brand
- GET /api/Brand/{id}
- POST /api/Brand
- PUT /api/Brand/{id}

### Posts (API externa)

- GET /api/Posts

### People (datos en memoria)

- GET /api/People/all
- GET /api/People/{id}
- GET /api/People/search/{search}

### Operation

- GET /api/Operation?a=10&b=2 (suma)
- POST /api/Operation (resta, body con A y B)
- PUT /api/Operation?a=10&b=2 (multiplicacion)
- DELETE /api/Operation?a=10&b=2 (division)

## Mejoras recomendadas

- Agregar autenticacion/autorizacion (JWT).
- Implementar manejo global de excepciones con middleware.
- Incorporar pruebas unitarias e integracion.
- Completar operaciones faltantes (delete en Beer/Brand) si el dominio lo requiere.
- Centralizar versionado de API y estandar de respuestas.

---

Si quieres, puedo preparar tambien una version corta del README para portafolio (orientada a reclutadores) y mantener esta como documentacion tecnica completa.
