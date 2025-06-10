using System;
using UserValidationLibrary.Models;
using UserValidationLibrary.Services;

class Program
{
    static void Main()
    {
        var validator = new UserValidator();

        var user = new User
        {
            Name = "João Silva",
            Email = "joao.silva@example.com",
            Password = "Senha123!",
            BirthDate = DateTime.Today.AddYears(-30),
            CPF = "12345678901",
            Endereco = "Rua A, 123",
            CEP = "12345678",
            Telefone = "11987654321"
        };

        var result = validator.Validate(user);

        Console.WriteLine($"Validação do usuário {user.Name}: {(result.IsValid ? "Válido" : "Inválido")}");

        if (!result.IsValid)
        {
            Console.WriteLine("Erros:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($" - {error.ErrorMessage}");
            }
        }
    }
}
