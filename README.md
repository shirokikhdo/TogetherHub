# TogetherHub

Together Hub — это платформа для создания и управления топиками с интегрированной системой аутентификации и авторизации.

## Архитектура проекта

Проект построен с использованием Clean Architecture, применяющей подходы:
- CQRS (Command Query Responsibility Segregation)
- Mediator Pattern
- Domain-Driven Design

### Слои архитектуры
- Domain: Содержит основные сущности, value objects и бизнес-правила.
- Application: Отвечает за бизнес-логику и реализацию процессов, таких как команды и запросы.
- Infrastructure: Реализует взаимодействие с базой данных, внешними сервисами и поддерживает Identity.
- API: Включает контроллеры и конфигурацию веб-приложения.

### Особенности реализации
- **Использование Value Objects**: Для работы с идентификаторами и значениями, обеспечивая безопасность и неизменяемость данных.
- **Поддержка мягкого удаления (Soft Delete)**: Удаленные сущности сохраняются в базе, но становятся недоступными.
- **Расширенная обработка исключений**: Централизованный обработчик с формированием стандартизированных JSON-ответов.
- **Гибкая система авторизации**: Реализация и настройка политик и требований.
- **Автомаппинг DTO и сущностей**: С использованием AutoMapper для упрощения преобразования данных между слоями.

### Бизнес-логика
- Регистрация и аутентификация пользователей
- Создание и управление топиками
- Система ролей участников (организатор, спикер, участник)
- Комментирование топиков

## Технологии

- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) - платформа для разработки
- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet) - фреймворк для создания веб-приложений
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM для работы с базами данных
- [SQLite](https://www.sqlite.org/) - cистема управления базами данных

## Библиотеки
- Для дизайна Entity Framework Core:
```
dotnet add package Microsoft.EntityFrameworkCore.Design
```
- Для нормальной работы Swagger:
```
dotnet add package Swashbuckle.AspNetCore
```
- Для автоматизации сопоставление объектов разных типов:
```
dotnet add package AutoMapper
```
- Для аутентификации с использованием JWT:
```
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```
- Для работы с JWT:
```
dotnet add package Microsoft.IdentityModel.JsonWebTokens
```
- Для использования Identity с Entity Framework Core:
```
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```
- Для работы с базами данных SQLite в Entity Framework Core:
```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```
- Для использования паттера медиатор:
```
dotnet add package MediatR
```

## Установка

1. Клонируйте репозиторий:
```
git clone https://github.com/shirokikhdo/TogetherHub.git
cd TogetherHub
```

2. Восстановите зависимости:

```
dotnet restore
```

3. Создайте базу данных и примените миграции:

```
dotnet ef database update
```
   
4. Запустите приложение:

```
dotnet run
```

## API Endpoints

### AuthController
 - **POST /api/Auth/login**
	- **Описание:** Выполняет вход пользователя в систему.
	- **Тело запроса:** `LoginIdentityUserDto`
	- **Ответы:**
		- 200 OK: Пользователь вошел в систему
		- 400 Bad Request: Неправильно введен пароль
		- 404 Not Found: Пользователь не найден в системе
- **POST /api/Auth/register**
	- **Описание:** Выполняет регистрацию пользователя в систему.
	- **Тело запроса:** `RegisterIdentityUserDto`
	- **Ответы:**
		- 200 OK: Пользователь вошел в систему
		- 400 Bad Request: Пользователь с данными уже существует
		- 500 Internal Server Error: Ошибка при создании пользователя

### TopicsController
- **GET /api/Topics**
	- **Описание:** Получает список всех тем в системе.
	- **Ответы:**
		- 200 OK: Существующие контакты
- **GET /api/Topics/{id}**
	- **Описание:** Получает список всех тем в системе.
	- **Параметры:** `id`
	- **Ответы:**
		- 200 OK: Существующие темы
		- 404 Not Found: Тема не найдена в системе
- **POST /api/Topics**
	- **Описание:** Создает новую тему.
	- **Тело запроса:** `CreateTopicDto`
	- **Ответы:**
		- 201 Created: Тема создана
- **PUT /api/Topics/{id}**
	- **Описание:** Обновляет существующую тему по идентификатору.
	- **Параметры:** `id`
	- **Тело запроса:** `UpdateTopicDto`
	- **Ответы:**
		- 200 OK: Тема обновлена
		- 404 Not Found: Тема не найдена в системе
- **DELETE /api/Topics/{id}**
	- **Описание:** Удаляет тему по идентификатору.
	- **Ответы:**
		- 200 OK: Тема удалена
        - 404 Not Found: Тема не найдена в системе