# Expense Control System

Sistema full stack para controle de despesas pessoais e empresariais, desenvolvido como projeto de portfólio com foco em boas práticas de arquitetura, testes automatizados e automação de processos.

O objetivo principal foi simular um cenário próximo ao ambiente profissional, desde a organização do código até a entrega contínua via CI/CD.

## Demonstração do Sistema

<div align="center">

### Dashboard Principal

![Dashboard](https://github.com/user-attachments/assets/2d843168-e2f5-462d-b9c9-04023703402b)

Visão geral do sistema com resumo financeiro.

### Gestão de Pessoas

![Pessoas](https://github.com/user-attachments/assets/e6ae9b66-2653-4ca5-882c-31a1423b8110)

CRUD completo de pessoas cadastradas no sistema.

### Categorias de Transações

![Categorias](https://github.com/user-attachments/assets/135b2ac3-69e2-49c1-bb91-dec8eb4c2e61)

Categorias organizadas por finalidade (Despesa, Receita ou ambos).

### Registro de Transações

![Transações](https://github.com/user-attachments/assets/fa822089-e747-4842-b7b5-4826fea826a0)

Formulário com validações em tempo real e aplicação de regras de negócio.

### Relatórios Financeiros

![Relatórios](https://github.com/user-attachments/assets/cd56628d-c59c-4b94-bde7-053d027c229a)

Resumo financeiro por pessoa, com total de receitas, despesas e saldo.

</div>

## Funcionalidades

### Frontend (React)

* Dashboard com resumo financeiro
* Cadastro de transações (entradas e saídas)
* Gerenciamento de categorias
* Controle de pessoas e fornecedores
* Interface responsiva
* Componentes reutilizáveis com Tailwind CSS

### Backend (.NET 8)

* API RESTful
* Persistência de dados com Entity Framework Core
* Arquitetura em camadas
* Validações com FluentValidation
* Testes unitários

### DevOps

* Pipeline de CI/CD com GitHub Actions
* Execução automática de testes
* Build automatizado da aplicação
* Verificação básica de qualidade de código

---

## Tecnologias Utilizadas

### Frontend

* React
* TypeScript
* Tailwind CSS
* React Router DOM
* Axios
* React Hook Form
* Zod
* Recharts

### Backend

* .NET 8
* Entity Framework Core
* Swagger / OpenAPI
* xUnit
* Moq
* FluentValidation
* AutoMapper

### DevOps e Ferramentas

* GitHub Actions
* Git
* ESLint
* Prettier

---

## Decisões Técnicas

* Separação clara entre frontend e backend para facilitar manutenção e escalabilidade.
* Arquitetura em camadas no backend visando melhor organização do domínio e facilidade para testes.
* Uso do Tailwind CSS para acelerar o desenvolvimento da interface e manter consistência visual.
* GitHub Actions adotado para simular um fluxo real de integração e entrega contínua.
* Testes unitários implementados para garantir confiabilidade das regras de negócio.

---

## Como Executar o Projeto

### Pré-requisitos

* Node.js 18+
* .NET 8 SDK
* Git
* SQL Server

### Passo 1: Clonar o repositório

```bash
git clone https://github.com/danielNevesSilva/ExpenseControlSystem.git
cd ExpenseControlSystem
```

### Passo 2: Backend

```bash
cd Backend
dotnet restore
```

Configure a connection string no arquivo `appsettings.json`.

Execute as migrações:

```bash
dotnet ef database update
```

Inicie a API:

```bash
dotnet run --project ExpenseControlSystem.API
```

---

## Status do Projeto

Projeto funcional e em evolução, com possibilidade de inclusão de novas funcionalidades e melhorias conforme aprendizado contínuo.

---

## Repositório

[https://github.com/danielNevesSilva/ExpenseControlSystem](https://github.com/danielNevesSilva/ExpenseControlSystem)
