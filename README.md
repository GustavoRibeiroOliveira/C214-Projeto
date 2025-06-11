# Projeto de Validação de Usuários (.NET + MSTest + Cake + FluentValidation)

## 🧾 Descrição

Este projeto implementa uma biblioteca para validar dados de usuários, incluindo nome, e-mail, cep, endereço, telefone, senha e data de nascimento. Utiliza:

- .NET 9
- MSTest para testes unitários
- FluentValidation como dependência externa
- Cake para build, testes, relatórios e empacotamento

---

## 🧩 Instalação (Gerenciamento de Dependências)

Clone o projeto:

```bash
git clone https://github.com/GustavoRibeiroOliveira/C214-Projeto
cd C214-Projeto
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

Comando para rodar os testes:

```bash
dotnet test
```

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

O pipeline foi implementado com o [Cake (C# Make)](https://cakebuild.net/), uma ferramenta de automação de build. As etapas são descritas no arquivo `build.cake` localizado na raiz do projeto.

### 🔄 Etapas da Pipeline

| Etapa                      | O que faz                                                                 |
|---------------------------|---------------------------------------------------------------------------|
| `Clean`                   | Limpa os diretórios `bin/`, `obj/` e `TestResults/`                       |
| `Restore`                 | Restaura todas as dependências do projeto com `dotnet restore`            |
| `Build`                   | Compila a solução com `dotnet build` no modo Release                      |
| `Test`                    | Executa os testes com `dotnet test`, gerando arquivos `.trx` e cobertura |
| `Generate-Coverage-Report`| Gera um relatório HTML com base na cobertura de código (Coverlet + ReportGenerator) |
| `Generate-Test-Report`    | Converte o resultado dos testes (`.trx`) em um relatório HTML amigável    |
| `Default`                 | Executa todas as etapas acima                                             |

### 🚀 Comandos para rodar a pipeline

1. Certifique-se de que os requisitos estão instalados:

```bash
dotnet tool restore
```

2. Execute o pipeline:

```bash
dotnet cake
```

📈 Relatórios
Após rodar o dotnet cake, dois relatórios são gerados automaticamente:

✅ Relatório de cobertura de código
Local: ./TestResults/CoverageReport/index.html

Geração: via Coverlet + ReportGenerator

```bash
start ./TestResults/CoverageReport/index.html
```

✅ Relatório de testes (TRX → HTML)
Local: ./TestResults/TestReport.html

Geração: via projeto auxiliar TrxToHtml que converte .trx em HTML

```bash
start ./TestResults/TestReport.html
```

---

## 📁 Organização de Pastas

```
UserValidationProject/
├── UserValidationLibrary/       # Código principal (validações)
├── UserValidationTests/         # Testes MSTest
├── UserValidationApp/           # Código simples para testar usuários
├── TrxToHtml/                   # Código para converter arquivo .trx em HTML
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
