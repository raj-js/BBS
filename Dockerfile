FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["EDoc2.FAQ.Api/EDoc2.FAQ.Api.csproj", "EDoc2.FAQ.Api/"]
COPY ["EDoc2.FAQ.Core.Infrastructure/EDoc2.FAQ.Core.Infrastructure.csproj", "EDoc2.FAQ.Core.Infrastructure/"]
COPY ["EDoc2.FAQ.Core.Domain/EDoc2.FAQ.Core.Domain.csproj", "EDoc2.FAQ.Core.Domain/"]
COPY ["EDoc2.FAQ.EventBus.RabbitMQ/EDoc2.FAQ.EventBus.RabbitMQ.csproj", "EDoc2.FAQ.EventBus.RabbitMQ/"]
COPY ["EDoc2.FAQ.EventBus/EDoc2.FAQ.EventBus.csproj", "EDoc2.FAQ.EventBus/"]
COPY ["EDoc2.FAQ.Core.Application/EDoc2.FAQ.Core.Application.csproj", "EDoc2.FAQ.Core.Application/"]
COPY ["EDoc2.FAQ.Notification.Mail/EDoc2.FAQ.Notification.Mail.csproj", "EDoc2.FAQ.Notification.Mail/"]
RUN dotnet restore "EDoc2.FAQ.Api/EDoc2.FAQ.Api.csproj"
COPY . .
WORKDIR "/src/EDoc2.FAQ.Api"
RUN dotnet build "EDoc2.FAQ.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EDoc2.FAQ.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EDoc2.FAQ.Api.dll"]