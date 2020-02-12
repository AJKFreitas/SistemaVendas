FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY ./code /app
ENV ASPNETCORE_ENVIRONMENT=Staging
ENTRYPOINT ["dotnet", "SistemaVendas.Api.dll"]