## MinhaAgenda API (Backend)
API REST desenvolvida com ASP.NET Core para gestão de tarefas e compromissos, servindo como o core do ecossistema T-Control. O foco deste projeto é demonstrar práticas de Clean Code, persistência de dados robusta e segurança.

## 🚀 Tecnologias e Ferramentas
* **Framework:** .NET (C#)
* **Web Framework:** ASP.NET Core Web API
* **ORM:** Entity Framework Core (Code First)
* **Banco de Dados:** SQL Server / SQLite
* **Segurança:** Autenticação e Autorização via JWT (JSON Web Token)

## 🏗️ Diferenciais Técnicos
* **Padrão Repository:** Separação da lógica de persistência da regra de negócio para facilitar testes e manutenção.
* **Migrations:** Gerenciamento de versionamento de banco de dados via EF Core para garantir integridade entre ambientes.
* **Clean API:** Endpoints documentados e estruturados seguindo rigorosamente os princípios REST.

  ## 📍 Endpoints Principais
| Verbo | Endpoint | Descrição |
|-------|----------|-----------|
| POST | `/auth/login` | Autenticação e geração de Token JWT |
| GET | `/tasks` | Retorna todas as tarefas cadastradas |
| POST | `/tasks` | Cria uma nova tarefa no banco de dados |
| DELETE | `/tasks/{id}` | Remove uma tarefa específica por ID |
