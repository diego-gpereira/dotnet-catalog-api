# API de Catálogo em ASP.NET Core 8

![.NET](https://img.shields.io/badge/.NET-8-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-8-4E257C?style=for-the-badge&logo=entity-framework&logoColor=white)
![xUnit](https://img.shields.io/badge/xUnit-Testes-blue?style=for-the-badge&logo=xunit&logoColor=white)

Este projeto é uma API RESTful completa para gerenciamento de produtos e categorias, construída com as práticas mais recentes do ecossistema .NET. O objetivo é demonstrar a criação de um back-end robusto, testável e bem-estruturado.

Desenvolvida com **.NET 8** e **C# 12**, a API segue os princípios de arquitetura limpa, separando as responsabilidades em diferentes camadas para facilitar a manutenção e a escalabilidade. O acesso a dados é gerenciado pelo **Entity Framework Core 8**, e a qualidade do código é assegurada por uma suíte de **testes unitários** com xUnit.

---

### 🚦 Status do Projeto
**Em Desenvolvimento Ativo**

---

### ✨ Funcionalidades Principais

* **Arquitetura RESTful:** Endpoints bem definidos seguindo os padrões HTTP para operações CRUD.
* **Acesso a Dados com EF Core:** Utilização do principal ORM do mercado .NET para mapeamento objeto-relacional.
* **Validação de Dados (FluentValidation):** Regras de validação robustas para garantir a integridade dos dados.
* **Testes Unitários (xUnit):** Cobertura de testes para as regras de negócio, assegurando a qualidade e prevenindo regressões.
* **Estrutura Organizada:** Código com clara separação de responsabilidades (Controllers, Data, Models).
* **Documentação com Swagger:** Geração automática de uma documentação interativa da API.

---

### 🛠️ Tecnologias Utilizadas

* **Framework Principal:** .NET 8
* **Linguagem:** C# 12
* **Arquitetura da API:** ASP.NET Core 8
* **Acesso a Dados:** Entity Framework Core 8
* **Testes:** xUnit
* **Validação:** FluentValidation
* **Banco de Dados (Desenvolvimento):** Provedor In-Memory do EF Core
* **Documentação:** Swagger (OpenAPI)

---

### 🚀 Como Executar o Projeto

1.  Clone o repositório:
    ```bash
    git clone https://github.com/diego-gpereira/dotnet-catalog-api.git
    ```
2.  Navegue até a pasta do projeto:
    ```bash
    cd dotnet-catalog-api
    ```
3.  Restaure as dependências do projeto:
    ```bash
    dotnet restore
    ```
4.  Execute a aplicação:
    ```bash
    dotnet run --project src/CatalogoApi.csproj
    ```
5.  Acesse a documentação do Swagger em `https://localhost:7199/swagger` (ou a porta indicada no terminal).

---

### 🗺️ Roadmap (Próximos Passos)

* [ ] Migração do provedor de dados de In-Memory para SQL Server.
* [ ] Implementação de autenticação e autorização com JWT.
