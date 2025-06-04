# Projeto de Validação de Usuários (.NET + MSTest + Cake + FluentValidation)

## 🧾 Descrição

Este projeto implementa uma biblioteca para validar dados de usuários, incluindo nome, e-mail, senha e data de nascimento. Utiliza:

- .NET 9
- MSTest para testes unitários
- FluentValidation como dependência externa
- Cake para build, testes, relatórios e empacotamento

---

## 🧩 Instalação (Gerenciamento de Dependências)

Clone o projeto:

```bash
git clone 
cd
```

Instale as dependências:

```bash
dotnet restore
```

Dependência usada:
- `FluentValidation` (`dotnet add package FluentValidation`)

---

## ⚙️ Como Executar o Projeto

```bash
dotnet build
dotnet test
```

## ⚙️ Para rodar o app

```bash
dotnet run --project UserValidationApp/UserValidationApp.csproj  
```

---

## 🚧 Como Executar o Build + Testes + Pacote com Cake

### 1. Instale a ferramenta Cake:

```bash
dotnet new tool-manifest
dotnet tool install Cake.Tool
```

### 2. Execute o pipeline (clean, restore, build, test):

```bash
dotnet cake
```

---

## 🧪 Testes

Foram criados 20 testes no total:
- 10 testes positivos (usuários válidos)
- 10 testes negativos (usuários inválidos)

### Exemplos de validações negativas:
- Nome vazio
- Nome muito curto
- Email inválido
- Senha sem maiúscula
- Senha sem número
- Data de nascimento < 18 anos

### Estratégia:
1. Criamos um modelo `User`.
2. Validamos com `UserValidator` usando FluentValidation.
3. Criamos testes para cada regra positiva e negativa com MSTest.

---

## 🏗️ Pipeline (Cake)

O pipeline foi criado usando o `Cake` com as seguintes etapas:
- Clean (`bin/` e `obj/`)
- Restore (dependências)
- Build (`dotnet build`)
- Test (`dotnet test`)
- (Opcional) Empacotar

Script usado: `build.cake`

---

## 📁 Organização de Pastas

```
UserValidationProject/
├── UserValidationLibrary/       # Código principal (validações)
├── UserValidationTests/         # Testes MSTest
├── UserValidationApp/           # Código simples para testar usuários
├── build.cake                   # Script do pipeline
└── README.md
```

---

## 📦 Empacotamento

Você pode empacotar o projeto com:

```bash
dotnet pack UserValidationLibrary/UserValidationLibrary.csproj
```

O pacote `.nupkg` será gerado na pasta `bin/Release`.

---
