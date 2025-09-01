
- **Backend**: .NET 9, ASP.NET Core
- **Padrão de Arquitetura**: Clean Architecture & CQRS com MediatR
- **Mensageria**: RabbitMQ com MassTransit (Padrão Pub/Sub)
- **Banco de Dados**: PostgreSQL
- **ORM**: Entity Framework Core
- **Armazenamento de Arquivos**: MinIO (Compatível com S3)
- **Banco de Notificações**: MongoDB
- **Containerização**: Docker & Docker Compose

## Pré-requisitos

### 2. Inicie os Serviços de Infraestrutura

O projeto utiliza um arquivo `docker-compose.yml` para orquestrar todos os contêineres de infraestrutura (PostgreSQL, MinIO, RabbitMQ, MongoDB). Para iniciá-los, execute o seguinte comando na raiz do projeto:

```bash
docker-compose up -d

Este comando irá baixar as imagens necessárias e iniciar os serviços em segundo plano (`-d`).

- **PostgreSQL**: Acessível na porta `5432`.
- **MinIO**:
  - **Serviço**: Acessível na porta `9000`.
  - **Console Web**: Acesse em `http://localhost:9001`. Use as credenciais `minioadmin` / `minioadmin` para fazer login.
- **RabbitMQ**:
  - **Serviço**: Acessível na porta `5672`.
  - **Console Web**: Acesse em `http://localhost:15672`. Use as credenciais `user` / `password` para fazer login.
- **MongoDB**: Acessível na porta `27017`.
 
### 3. Execute as Migrações do Banco de Dados

Com o contêiner do PostgreSQL em execução, precisamos criar a estrutura de tabelas no banco de dados. Use o Entity Framework Core para aplicar as migrações:

