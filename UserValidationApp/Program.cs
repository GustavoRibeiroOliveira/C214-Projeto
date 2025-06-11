using System;
using UserValidationLibrary.Models;
using UserValidationLibrary.Services;

class Program
{
    static void Main()
    {
        var validator = new UserValidator();

        var users = new List<User>
        {
            new User
            {
                Name = "João Silva",
                Email = "joao.silva@example.com",
                Password = "Senha123",
                BirthDate = DateTime.Today.AddYears(-30),
                CPF = "12345678901",
                Endereco = "Rua A, 123",
                CEP = "12345678",
                Telefone = "11987654321"
            },
            new User
            {
                Name = "", // Nome inválido
                Email = "maria.souza@example.com",
                Password = "Senha123",
                BirthDate = DateTime.Today.AddYears(-25),
                CPF = "98765432100",
                Endereco = "Rua B, 456",
                CEP = "87654321",
                Telefone = "21987654321"
            },
            new User
            {
                Name = "Carlos Lima",
                Email = "carlos.limasemarroba", // Email inválido
                Password = "Abc123",
                BirthDate = DateTime.Today.AddYears(-20),
                CPF = "11223344556",
                Endereco = "Rua C, 789",
                CEP = "13579135",
                Telefone = "31987654321"
            },
            new User
            {
                Name = "Julia Fernandes",
                Email = "julia@example.com",
                Password = "123456", // Sem letra maiúscula
                BirthDate = DateTime.Today.AddYears(-22),
                CPF = "99887766554",
                Endereco = "Av. Paulista, 1000",
                CEP = "11223344",
                Telefone = "41987654321"
            },
            new User
            {
                Name = "Lucas Rocha",
                Email = "lucas.rocha@example.com",
                Password = "SenhaSegura1",
                BirthDate = DateTime.Today.AddYears(-19),
                CPF = "55443322119",
                Endereco = "Alameda Santos, 200",
                CEP = "66778899",
                Telefone = "51912345678"
            }
        };

        Console.WriteLine("==== Validação de Usuários ====\n");

        foreach (var user in users)
        {
            var result = validator.Validate(user);

            Console.WriteLine($"Usuário: {user.Name}");
            Console.WriteLine($"Status: {(result.IsValid ? "✅ Válido" : "❌ Inválido")}");

            if (!result.IsValid)
            {
                Console.WriteLine("Erros:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($" - {error.PropertyName}: {error.ErrorMessage}");
                }
            }

            Console.WriteLine(new string('-', 40));
        }

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}
