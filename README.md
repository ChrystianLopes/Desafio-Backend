# Desafio Backend

Este é um projeto para gerenciar o aluguel de motos para entregadores.

## 📖 Sobre o Projeto

A aplicação consiste em uma API para gerenciar o aluguel de motos para entregadores. Uma vez que um entregador esteja registrado e com uma locação ativa, ele poderá aceitar e realizar entregas de pedidos disponíveis na plataforma.

Ela foi desenvolvida seguindo as melhores práticas de desenvolvimento de software, como arquitetura limpa (Clean Architecture) e injeção de dependência.

---

## 🚀 Tecnologias Utilizadas

Este projeto foi desenvolvido com as seguintes tecnologias:

- **[.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)**: Framework principal para a construção da aplicação.
- **[ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/core/)**: Para a criação da API RESTful.
- **[Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)**: ORM para a comunicação com o banco de dados.
- **[PostgreSQL](https://www.postgresql.org/)**: Banco de dados relacional para persistência de dados estruturados (ex: locações, entregadores).
- **[MongoDB](https://www.mongodb.com/pt-br)**: Banco de dados NoSQL para armazenar dados não estruturados ou de alta volumetria (ex: logs de notificação).
- **[RabbitMQ](https://www.rabbitmq.com/)**: Sistema de mensageria para comunicação assíncrona entre serviços (ex: notificação de novos pedidos).
- **[MinIO](https://min.io/)**: Serviço de armazenamento de objetos compatível com S3, para guardar arquivos como a imagem da CNH dos entregadores.

---

## 📋 Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:

- [Git](https://git-scm.com)
- [.NET 8 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- Um editor de código de sua preferência, como [Visual Studio](https://visualstudio.microsoft.com/pt-br/) ou [VS Code](https://code.visualstudio.com/download)
- [Docker](https://www.docker.com/products/docker-desktop/) (Altamente recomendado para rodar PostgreSQL, MongoDB, RabbitMQ e MinIO).

---

## ⚙️ Configuração e Instalação

Siga os passos abaixo para configurar e executar o projeto localmente.

**1. Clone o repositório:**

```bash
git clone https://github.com/ChrystianLopes/Desafio-Backend
cd Desafio-Backend
```

**2. Configure as variáveis de ambiente:**

Os arquivos de configuração `appsettings.json` e `appsettings.Development.json` não são versionados por segurança. Você precisará criar o seu.

Crie um arquivo chamado `appsettings.Development.json` na raiz do projeto principal (onde está o `Program.cs`) e adicione o seguinte conteúdo, substituindo pela sua string de conexão:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=NOME_DO_BANCO;Username=SEU_USUARIO;Password=SUA_SENHA;"
  },
  "MongoDb": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "NOME_DO_BANCO_MONGO"
  },
  "RabbitMq": {
    "HostName": "localhost",
    "UserName": "guest",
    "Password": "guest"
  },
  "Minio": {
    "Endpoint": "localhost:9000",
    "AccessKey": "minioadmin",
    "SecretKey": "minioadmin",
    "BucketName": "imagens-cnh"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```
> **Nota:** A string de conexão acima é um exemplo para SQL Server. Adapte-a conforme o banco de dados que você estiver utilizando.

**3. Restaure as dependências do projeto:**

```bash
dotnet restore
```

**4. Aplique as Migrations do Entity Framework Core:**

Este comando criará o banco de dados e todas as tabelas necessárias com base no modelo de dados.

```bash
# Certifique-se de estar no diretório do projeto que contém o DbContext
dotnet ef database update
```

**5. Execute a aplicação:**

```bash
# O comando 'watch' reiniciará a aplicação automaticamente sempre que um arquivo for alterado.
dotnet watch run
```

Após a execução, a API estará disponível em `https://localhost:PORTA` e `http://localhost:PORTA`, onde `PORTA` é a porta definida no arquivo `launchSettings.json`.

---
