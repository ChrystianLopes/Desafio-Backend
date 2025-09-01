# Desafio de Backend - Plataforma de Aluguel de Motos

Este repositório contém o código-fonte de uma API RESTful para uma plataforma de aluguel de motos. A aplicação foi desenvolvida em .NET 9, seguindo os princípios da **Clean Architecture** e do padrão **CQRS** para garantir um código desacoplado, manutenível, escalável e testável.

## Funcionalidades

- **Gerenciamento de Motos**: Operações CRUD (Criar, Ler, Atualizar, Deletar) para o cadastro de motos.
- **Gerenciamento de Entregadores**: Cadastro de entregadores, incluindo validações de CNH.
- **Upload de CNH**: Endpoint para upload da imagem da CNH do entregador, com armazenamento em um serviço de object storage compatível com S3 (MinIO).
- **Gerenciamento de Aluguéis**: Endpoints para criar e gerenciar aluguéis de motos, com cálculo automático de custos baseado no plano de aluguel e data de devolução.
- **Mensageria Assíncrona**: Utiliza o padrão Pub/Sub com RabbitMQ e MassTransit para processar tarefas em segundo plano (como o envio de notificações), sem impactar a performance da API.
- **Consulta de Notificações**: Endpoint para consultar as notificações geradas e armazenadas no MongoDB.

## Tecnologias Utilizadas

- **Backend**: .NET 9, ASP.NET Core
- **Padrão de Arquitetura**: Clean Architecture & CQRS com MediatR
- **Mensageria**: RabbitMQ com MassTransit (Padrão Pub/Sub)
- **Banco de Dados Principal**: PostgreSQL
- **ORM**: Entity Framework Core
- **Armazenamento de Arquivos**: MinIO (Compatível com S3)
- **Banco de Notificações**: MongoDB
- **Containerização**: Docker & Docker Compose

## Pré-requisitos

Antes de começar, certifique-se de que você tem os seguintes softwares instalados na sua máquina:

- .NET 9 SDK
- Docker Desktop (que inclui Docker e Docker Compose)

## Como Executar o Projeto

Siga os passos abaixo para configurar e rodar a aplicação localmente.

### 1. Clone o Repositório

```bash
git clone https://github.com/seu-usuario/Desafio-Backend.git
cd Desafio-Backend
