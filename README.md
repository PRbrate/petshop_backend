# PetShop Backend

Este projeto é um sistema backend para gerenciamento de um pet shop, desenvolvido em C# utilizando o .NET Framework.

## Funcionalidades

- Gerenciamento de agendamentos de serviços
- Cadastro e gerenciamento de clientes e seus pets
- Controle de serviços oferecidos
- Autenticação e autorização de usuários

## Estrutura do Projeto

O projeto está organizado em várias camadas para promover uma arquitetura limpa e modular:

- `PetShop.Api`: Camada de apresentação (API)
- `PetShop.Application`: Lógica de aplicação
- `PetShop.Core`: Componentes centrais e interfaces
- `PetShop.Data`: Acesso a dados e repositórios
- `PetShop.Domain`: Entidades de domínio
- `PetShop.Facade`: Fachada para integração entre camadas
- `Appointments.Application`: Aplicação relacionada a agendamentos
- `Appointments.Data`: Acesso a dados de agendamentos
- `Application.Domain`: Domínio da aplicação

## Tecnologias Utilizadas

- C#
- .NET Framework
- ASP.NET Web API
- Entity Framework

## Como Executar o Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/PRbrate/petshop_backend.git