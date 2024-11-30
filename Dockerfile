# Используем официальный образ для .NET SDK для этапа сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем рабочую директорию внутри контейнера
WORKDIR /app

# Копируем файлы проекта
COPY ["SoundUp/SoundUp.csproj", "SoundUp/"]

# Восстанавливаем зависимости проекта
RUN dotnet restore "SoundUp/SoundUp.csproj"

# Копируем весь исходный код проекта в контейнер
COPY . ./

# Строим проект
RUN dotnet build "SoundUp/SoundUp.csproj" -c Release -o /app/build

# Устанавливаем Entity Framework CLI
RUN dotnet tool install --global dotnet-ef

# Публикуем приложение
RUN dotnet publish "SoundUp/SoundUp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Базовый образ для рабочего контейнера
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Устанавливаем рабочую директорию
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Копируем собранное приложение из предыдущего этапа
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Устанавливаем точку входа для контейнера
ENTRYPOINT ["dotnet", "SoundUp.dll"]

