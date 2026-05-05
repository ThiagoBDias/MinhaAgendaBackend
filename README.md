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

## 📱 Integração com Ecossistema
Esta API foi projetada para alimentar o **T-Control**, um aplicativo Android para gestão pessoal de gastos e tarefas. 
A arquitetura foi pensada para suportar múltiplas requisições simultâneas e garantir a integridade dos dados financeiros e de produtividade do usuário.


## ⚙️ Como executar o projeto
1. Clone este repositório.
2. Certifique-se de ter o **SDK do .NET** instalado.
3. No terminal, dentro da pasta do projeto, execute:
   ```bash
   dotnet ef database update
   dotnet run
