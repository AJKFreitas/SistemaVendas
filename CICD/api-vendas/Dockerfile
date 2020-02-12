FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY ./code /app
EXPOSE 80 443
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet", "SistemaVendas.Api.dll"]