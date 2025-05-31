# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем и восстанавливаем зависимости
COPY ["trSys.csproj", "./"]
RUN dotnet restore "trSys.csproj"

# Копируем все файлы проекта
COPY . .

# Собираем приложение
RUN dotnet build "trSys.csproj" -c Release -o /app/build

# Публикуем приложение
FROM build AS publish
RUN dotnet publish "trSys.csproj" -c Release -o /app/publish

# Этап рантайма
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Копируем опубликованное приложение
COPY --from=publish /app/publish .

# Добавляем путь к инструментам в PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Указываем порты
EXPOSE 80
EXPOSE 443

# Запускаем миграции и приложение
ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet trsys.dll"]