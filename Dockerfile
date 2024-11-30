# ���������� ����������� ����� ��� .NET SDK ��� ����� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# ������������� ������� ���������� ������ ����������
WORKDIR /app

# �������� ����� �������
COPY ["SoundUp/SoundUp.csproj", "SoundUp/"]

# ��������������� ����������� �������
RUN dotnet restore "SoundUp/SoundUp.csproj"

# �������� ���� �������� ��� ������� � ���������
COPY . ./

# ������ ������
RUN dotnet build "SoundUp/SoundUp.csproj" -c Release -o /app/build

# ������������� Entity Framework CLI
RUN dotnet tool install --global dotnet-ef

# ��������� ����������
RUN dotnet publish "SoundUp/SoundUp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ������� ����� ��� �������� ����������
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# ������������� ������� ����������
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# �������� ��������� ���������� �� ����������� �����
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# ������������� ����� ����� ��� ����������
ENTRYPOINT ["dotnet", "SoundUp.dll"]

