# VanderPet

Aplicativo multiplataforma para organização de serviços e agendamentos de pets, desenvolvido como projeto de estudo com .NET MAUI.

## Objetivo

Construir uma experiência mobile e desktop para apoiar o cadastro de pets e o fluxo de agendamento de serviços.

## Funcionalidades exploradas

- Cadastro de usuários e pets
- Login e recuperação de senha
- Agendamento de serviços
- Confirmação e histórico de agendamentos
- Modelagem de espécies, raças e serviços

## Tecnologias

- C#
- .NET MAUI
- XAML
- .NET 10

## Estrutura do projeto

| Diretório | Responsabilidade |
| --- | --- |
| `VanderPet/Models` | Entidades como Pet, Agendamento, Espécies, Raças e Serviços |
| `VanderPet/Views` | Telas da aplicação e fluxo de navegação |
| `VanderPet/Resources` | Ícones, imagens, fontes e outros recursos visuais |
| `VanderPet/Platforms` | Configurações específicas por plataforma |

## Plataformas

O projeto usa .NET MAUI com suporte configurado para Android, iOS, Mac Catalyst e Windows.

## Como executar

1. Instale o SDK do .NET e a carga de trabalho do MAUI.

   ```bash
   dotnet workload install maui
   ```

2. Restaure e execute a solução:

   ```bash
   dotnet restore VanderPet.slnx
   dotnet build VanderPet.slnx
   ```

3. Selecione uma plataforma compatível no Visual Studio e execute o projeto.

## Aprendizados

- Desenvolvimento multiplataforma com uma única base de código.
- Estruturação de interfaces com XAML.
- Modelagem de entidades para um domínio de serviços pet.
- Organização de telas e navegação em aplicações MAUI.

## Próximos passos

- Persistir dados localmente ou por API.
- Adicionar validações de formulário e testes.
- Evoluir o fluxo de agendamento com notificações e disponibilidade de horários.
