# Салон красоты — Лабораторные работы по C# / ASP.NET Core

Данный репозиторий содержит решение лабораторных работ по дисциплине, связанной с разработкой на платформе .NET. 
Проект представляет собой серверное приложение (Web API) для автоматизации работы салона красоты.

## Структура решения (Чистая архитектура)

Решение разделено на слои согласно принципам Clean Architecture:
* Salon.Domain — Ядро приложения. Содержит доменные модели (Client, Master, Service, Appointment), перечисления, интерфейсы репозиториев и DataSeed (тестовые данные).
* Salon.Contracts — Слой контрактов. Содержит DTO (Data Transfer Objects) и интерфейсы сервисов (IAnalyticsService, IClientService и т.д.).
* Salon.Application — Слой бизнес-логики. Содержит реализации сервисов (аналитика, CRUD-операции, маппинг).
* Salon.Infrastructure.EntityFrameworkCore — Слой инфраструктуры для работы с реальной БД (DbContext, EF-репозитории, миграции).
* Salon.Api — Веб-слой. Контроллеры, настройка DI, Swagger.
* Salon.AppHost — Оркестратор .NET Aspire (точка входа для запуска).
* Salon.ServiceDefaults — Общие настройки для Aspire.
* Salon.Tests — Проект с юнит-тестами (xUnit).


### Реализованные аналитические запросы:
* GET /api/Analytics/masters-experience — Вывод мастеров со стажем не менее 5 лет.
* GET /api/Analytics/free-slots/{masterId} — Поиск свободных окошек выбранного мастера.
* GET /api/Analytics/top-services — Топ-5 наиболее популярных услуг.
* GET /api/Analytics/repeat-appointments — Количество повторных записей клиентов за последний месяц.
* GET /api/Analytics/loyal-clients — Клиенты, записанные к нескольким мастерам.

## Лабораторная работа 3: Переход на реальную базу данных (MySQL + EF Core)

В рамках 3-й лабораторной работы было выполнено:
1. Переход с In-Memory коллекций на реальную базу данных MySQL.
2. Настроен Entity Framework Core: создан SalonDbContext, настроены связи между сущностями (OnModelCreating).
3. Создана и применена миграция InitialCreate для создания таблиц в БД.
4. Настроено автоматическое применение миграций и начальное заполнение данными (DataSeed) при старте приложения.
5. Aspire настроен на запуск сервера и базы данных (MySQL в Docker-контейнере) одной командой.

## Технологии
* C# / .NET 10
* ASP.NET Core Web API
* Entity Framework Core (Pomelo MySQL)
* MySQL (в Docker-контейнере)
* .NET Aspire (оркестрация)
* OpenAPI + Swagger UI (документация и тестирование)
* xUnit (тестирование)
* Dependency Injection (внедрение зависимостей)

## Как запустить проект
Убедитесь, что Docker Desktop запущен (зеленая иконка в трее).
Склонируйте репозиторий:
   ```bash
   git clone <ссылка_на_репозиторий>
Откройте файл решения Salon.slnx в Visual Studio.

В «Обозревателе решений» нажмите правой кнопкой мыши на проект Salon.AppHost и выберите «Назначить запускаемым проектом» (Set as Startup Project).

Нажмите F5 для запуска.

Откроется Aspire Dashboard. Дождитесь, пока ресурсы mysql и salon-api загорятся зеленым (Running).

Найдите карточку salon-api и нажмите на ссылку (например, https://localhost:7094).

В браузере перейдите по адресу https://localhost:7094/swagger (порт смотрите в Aspire Dashboard или консоли), чтобы открыть Swagger UI.