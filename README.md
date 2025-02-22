# TogetherHub

Together Hub � ��� ��������� ��� �������� � ���������� �������� � ��������������� �������� �������������� � �����������.

## ����������� �������

������ �������� � �������������� Clean Architecture, ����������� �������:
- CQRS (Command Query Responsibility Segregation)
- Mediator Pattern
- Domain-Driven Design

### ���� �����������
- Domain: �������� �������� ��������, value objects � ������-�������.
- Application: �������� �� ������-������ � ���������� ���������, ����� ��� ������� � �������.
- Infrastructure: ��������� �������������� � ����� ������, �������� ��������� � ������������ Identity.
- API: �������� ����������� � ������������ ���-����������.

### ����������� ����������
- **������������� Value Objects**: ��� ������ � ���������������� � ����������, ����������� ������������ � �������������� ������.
- **��������� ������� �������� (Soft Delete)**: ��������� �������� ����������� � ����, �� ���������� ������������.
- **����������� ��������� ����������**: ���������������� ���������� � ������������� ������������������� JSON-�������.
- **������ ������� �����������**: ���������� � ��������� ������� � ����������.
- **����������� DTO � ���������**: � �������������� AutoMapper ��� ��������� �������������� ������ ����� ������.

### ������-������
- ����������� � �������������� �������������
- �������� � ���������� ��������
- ������� ����� ���������� (�����������, ������, ��������)
- ��������������� �������

## ����������

- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) - ��������� ��� ����������
- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet) - ��������� ��� �������� ���-����������
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM ��� ������ � ������ ������
- [SQLite](https://www.sqlite.org/) - c������ ���������� ������ ������

## ����������
- ��� ������� Entity Framework Core:
```
dotnet add package Microsoft.EntityFrameworkCore.Design
```
- ��� ���������� ������ Swagger:
```
dotnet add package Swashbuckle.AspNetCore
```
- ��� ������������� ������������� �������� ������ �����:
```
dotnet add package AutoMapper
```
- ��� �������������� � �������������� JWT:
```
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```
- ��� ������ � JWT:
```
dotnet add package Microsoft.IdentityModel.JsonWebTokens
```
- ��� ������������� Identity � Entity Framework Core:
```
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```
- ��� ������ � ������ ������ SQLite � Entity Framework Core:
```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```
- ��� ������������� ������� ��������:
```
dotnet add package MediatR
```

## ���������

1. ���������� �����������:
```
git clone https://github.com/shirokikhdo/TogetherHub.git
cd TogetherHub
```

2. ������������ �����������:

```
dotnet restore
```

3. �������� ���� ������ � ��������� ��������:

```
dotnet ef database update
```
   
4. ��������� ����������:

```
dotnet run
```

## API Endpoints

### AuthController
 - **POST /api/Auth/login**
	- **��������:** ��������� ���� ������������ � �������.
	- **���� �������:** `LoginIdentityUserDto`
	- **������:**
		- 200 OK: ������������ ����� � �������
		- 400 Bad Request: ����������� ������ ������
		- 404 Not Found: ������������ �� ������ � �������
- **POST /api/Auth/register**
	- **��������:** ��������� ����������� ������������ � �������.
	- **���� �������:** `RegisterIdentityUserDto`
	- **������:**
		- 200 OK: ������������ ����� � �������
		- 400 Bad Request: ������������ � ������� ��� ����������
		- 500 Internal Server Error: ������ ��� �������� ������������

### TopicsController
- **GET /api/Topics**
	- **��������:** �������� ������ ���� ��� � �������.
	- **������:**
		- 200 OK: ������������ ��������
- **GET /api/Topics/{id}**
	- **��������:** �������� ������ ���� ��� � �������.
	- **���������:** `id`
	- **������:**
		- 200 OK: ������������ ����
		- 404 Not Found: ���� �� ������� � �������
- **POST /api/Topics**
	- **��������:** ������� ����� ����.
	- **���� �������:** `CreateTopicDto`
	- **������:**
		- 201 Created: ���� �������
- **PUT /api/Topics/{id}**
	- **��������:** ��������� ������������ ���� �� ��������������.
	- **���������:** `id`
	- **���� �������:** `UpdateTopicDto`
	- **������:**
		- 200 OK: ���� ���������
		- 404 Not Found: ���� �� ������� � �������
- **DELETE /api/Topics/{id}**
	- **��������:** ������� ���� �� ��������������.
	- **������:**
		- 200 OK: ���� �������
        - 404 Not Found: ���� �� ������� � �������