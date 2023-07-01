# Imagen base que utilizará el contenedor
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Establece el directorio de trabajo dentro del contenedor
WORKDIR /app

# Copia los archivos de tu proyecto al directorio de trabajo del contenedor
COPY . ./

# Ejecuta el comando 'dotnet restore' para restaurar las dependencias
RUN dotnet restore

# Ejecuta el comando 'dotnet publish' para compilar y publicar tu proyecto
RUN dotnet publish -c Release -o out

# Crea la imagen final del contenedor utilizando el runtime de .NET
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Establece el comando de inicio del contenedor
ENTRYPOINT ["dotnet", "BackEnd-SocialE.dll"]
