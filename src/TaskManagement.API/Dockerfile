# Build stage com SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copia código fonte e testes
COPY ./src ./src
COPY ./tests ./tests

# Copia o script wait-for.sh e dá permissão
COPY src/TaskManagement.API/wait-for.sh /wait-for.sh
RUN chmod +x /wait-for.sh

# Restaura dependências
RUN dotnet restore "src/TaskManagement.API/TaskManagement.API.csproj"

# Publica a aplicação (SEM testes e cobertura aqui)
RUN dotnet publish "src/TaskManagement.API/TaskManagement.API.csproj" -c Debug -o /app/publish

# Runtime stage com runtime + netcat + app publicada
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Instala netcat para o wait-for.sh funcionar
RUN apt-get update && apt-get install -y netcat-openbsd && rm -rf /var/lib/apt/lists/*

# Copia app publicado do build
COPY --from=build /app/publish ./

# Copia o script wait-for.sh e dá permissão
COPY --from=build /wait-for.sh /wait-for.sh
RUN chmod +x /wait-for.sh

ENTRYPOINT ["/wait-for.sh", "dotnet", "TaskManagement.API.dll"]
