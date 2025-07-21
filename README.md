# BloodBank

O **BloodBank** é um sistema para gerenciamento de bancos de sangue, desenvolvido em ASP.NET Core 8.0, seguindo princípios de Clean Architecture, DDD, SOLID e foco em código sustentável.

## ✨ Funcionalidades

- 🏥 **Gerenciamento de hospitais:** cadastro, edição e consulta de hospitais parceiros.
- 👥 **Gerenciamento de doadores:** cadastro, edição e consulta de doadores.
- 👤 **Gerenciamento de usuários:** registro e autenticação.
- 🔄 **Doações e Transferências:** registro de doações e transferências de sangue para hospitais.
- 🩸 **Controle de estoque de sangue:** consulta, atualização automática e monitoramento por tipo sanguíneo.
- 📧 **Notificações automáticas:** envio de alertas por e-mail para usuários responsáveis quando o estoque atinge níveis críticos.
- 🔐 **Autenticação via JWT:** segurança nas rotas da API.
- 📄 **Paginação:** consultas paginadas para grandes volumes de dados.

## 🛠️ Tecnologias e Padrões Utilizados

- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **PostgreSQL**
- **MediatR**
- **Swagger/OpenAPI**
- **FluentValidation**
- **JWT Bearer Authentication**
- **SendGrid (envio de e-mails)**
- **Testes Unitários (xUnit, Moq, Bogus, FluentAssertions)**

**Padrões e boas práticas:**

- Clean Architecture (Arquitetura Limpa)
- Domain-Driven Design (DDD)
- Repository Pattern
- Unit of Work Pattern
- CQRS (Command Query Responsibility Segregation)
- Result Pattern
- Injeção de Dependência (DI)
- Middleware global para exceções
- Eventos de domínio

## 🚀 Como rodar o projeto

1. Clone este repositório
2. Configure a string de conexão do PostgreSQL e as chaves do SendGrid no arquivo `appsettings.json`.
3. Navegue até o projeto Infrastructure:
   ```bash
   cd src/BloodBank.Infrastructure/
   ```
4. Execute as migrations do Entity Framework Core:
   ```bash
   dotnet ef database update -s ../BloodBank.API
   ```
5. Rode o projeto:
   ```bash
   dotnet run
   ```

## 🤝 Contribuição

Sinta-se à vontade para abrir issues, sugerir melhorias ou enviar pull requests!

---

Feito com 💙 por Gabriel
