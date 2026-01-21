Expense Control System

Sistema de controle de gastos residenciais desenvolvido para o processo seletivo da Maxiprod.

Tecnologias

Back-end
	•	.NET 8
	•	Entity Framework Core
	•	SQLite
	•	AutoMapper
	•	FluentValidation
	•	xUnit + Moq

Front-end
	•	React 18
	•	TypeScript
	•	Vite
	•	Axios
	•	Tailwind CSS

Funcionalidades
	•	Cadastro de pessoas (CRUD)
	•	Cadastro de categorias (Expense / Income / Both)
	•	Cadastro de transações com regras de negócio
	•	Relatórios de totais por pessoa
	•	Validações complexas (menor de idade, compatibilidade)
	•	Testes unitários e de integração
Aquitetura

ExpenseControlSystem/
├── src/
│   ├── Backend/ (Clean Architecture)
│   │   ├── Domain/
│   │   ├── Application/
│   │   ├── Infrastructure/
│   │   └── API/
│   └── Frontend/ (React + TypeScript)
└── tests/

![WhatsApp Image 2026-01-19 at 20 06 42](https://github.com/user-attachments/assets/2d843168-e2f5-462d-b9c9-04023703402b)
![WhatsApp Image 2026-01-19 at 20 07 04](https://github.com/user-attachments/assets/e6ae9b66-2653-4ca5-882c-31a1423b8110)
![WhatsApp Image 2026-01-19 at 20 07 28](https://github.com/user-attachments/assets/135b2ac3-69e2-49c1-bb91-dec8eb4c2e61)
![WhatsApp Image 2026-01-19 at 20 09 51](https://github.com/user-attachments/assets/fa822089-e747-4842-b7b5-4826fea826a0)
![WhatsApp Image 2026-01-19 at 20 10 16](https://github.com/user-attachments/assets/cd56628d-c59c-4b94-bde7-053d027c229a)
