# Repositorio de clase

## Planillas

- [Planilla con cuentas de GitHub y grupos de obligatorio](https://docs.google.com/spreadsheets/d/1bLGm9OaKtU-h75YThEI3SMGPkhzEKzBQqGd4hjxmsp0/edit?usp=sharing)


## Introducción

**El contenido de cada clase esta separado en las ramas subidas**. Este repositorio tiene como objetivo servir de apoyo a las clases de tecnología de la materia Diseño de Aplicaciones 2.


### Herramientas necesarias para este curso

- [.NET Core](https://dotnet.microsoft.com/download)
- [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads) / [SQL Server para MAC](https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&pivots=cs1-bash)
- [Postman](https://www.postman.com/)
- [Angular](https://angular.io/)
- [Node](https://nodejs.org/es/)
- [Git](https://git-scm.com/)

## Docentes

- :space_invader: Sebastián Bañales
- :space_invader: Matias Salles

## Docker (opcional Windows, obligatorio Mac/Linux)
```shell
docker run --name sql_server_da2 -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=mystrongPassword1234$" -e "MSSQL_PID=Express" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```
Si quieren ver detalles de las opciones y otras variantes del comando:
- [SQL Server Docker](https://hub.docker.com/_/microsoft-mssql-server)

### Crear la base de datos

(Una vez que tenemos el contenedor de Docker o la instancia de SQL Server corriendo)
para crear la base de datos y el ejecutar las migraciones en orden hasta la mas reciente, pararse en la carpeta de Vidly.DataAccess y ejecutar el siguiente comando

```shell
dotnet ef database update -s ../Vidly.Migrations -v  
```

Explicación:

- -s Indica el proyecto de Startup necesario para las migrations (En nuestro caso es un proyecto de aplicación de consola)
- -v (opcional) Indica que el output de la ejecución sea verbose y por lo tanto imprima un detalle de lo que esta haciendo. Esta bueno para ver como funciona

### Enlaces sobre comandos de migrations, como hacer Rollback, generar scripts de SQL, aplicar hasta una migración en sepcifico, etc.

- [EF Core Managing Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli)
- [EF Core Applying Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli)
- [EF Core DbContext Info](https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/)
- [DbContext al ejecutar migrations](https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/)
