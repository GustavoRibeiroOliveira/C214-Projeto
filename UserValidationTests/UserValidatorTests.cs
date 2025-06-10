using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserValidationLibrary.Models;
using UserValidationLibrary.Services;

namespace UserValidationTests;

[TestClass]
public class UserValidatorTests
{
    private UserValidator validator = new();

    private User ValidUser => new()
    {
        Name = "Maria Silva",
        Email = "maria.silva@teste.com",
        Password = "Senha123",
        BirthDate = DateTime.Today.AddYears(-25),
        CPF = "11144477735",
        Telefone = "11987654321",
        CEP = "12345678",
        Endereco = "Rua das Flores, 123"
    };

    // ======== TESTES POSITIVOS =========
    // -------------------------------------

    [TestMethod]
    public void UsuarioValido_DeveSerValido()
    {
        var user = new User
        {
            Name = "Ana Maria Braga",
            Email = "ana_braga@example.com",
            Password = "Senha123",
            BirthDate = DateTime.Today.AddYears(-25),
            CPF = "12345678901",
            CEP = "12345678",
            Telefone = "11987654321",
            Endereco = "Rua das Flores, 123"
        };

        var result = validator.Validate(user);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void NomeComMinimoDoisCaracteres_DeveSerValido()
    {
        var user = new User
        {
            Name = "Justin",
            Email = "Justin@example.com",
            Password = "Abc123",
            BirthDate = DateTime.Today.AddYears(-40),
            CPF = "98765432100",
            CEP = "87654321",
            Telefone = "21987654321",
            Endereco = "Av. Brasil, 456"
        };

        var result = validator.Validate(user);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void SenhaComLetraMaiusculaMinusculaENumero_DeveSerValida()
    {
        var user = new User
        {
            Name = "Kikomoto",
            Email = "Kikomoto@example.com",
            Password = "XyZ123",
            BirthDate = DateTime.Today.AddYears(-22),
            CPF = "75489033341",
            CEP = "53223528",
            Telefone = "31987654321",
            Endereco = "Rua Central, 789"
        };

        var result = validator.Validate(user);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void EmailValido_DeveSerAceito()
    {
        var user = new User
        {
            Name = "Bruna Oliver",
            Email = "bruna.oliveira@empresa.com",
            Password = "Segura456",
            BirthDate = DateTime.Today.AddYears(-29),
            CPF = "55566677788",
            CEP = "55667788",
            Telefone = "41987654321",
            Endereco = "Praça da Sé, 100"
        };

        var result = validator.Validate(user);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void UsuarioCom18AnosHoje_DeveSerValido()
    {
        var user = new User
        {
            Name = "Daniel",
            Email = "daniel@example.com",
            Password = "Senha123",
            BirthDate = DateTime.Today.AddYears(-18),
            CPF = "99988877766",
            CEP = "99887766",
            Telefone = "51987654321",
            Endereco = "Alameda Santos, 200"
        };

        var result = validator.Validate(user);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void CPFValido_DeveSerAceito()
    {
        var user = new User
        {
            Name = "Lucas",
            Email = "lucas@example.com",
            Password = "Senha123",
            BirthDate = DateTime.Today.AddYears(-30),
            CPF = "12345678901",
            Telefone = "11987654321",
            CEP = "12345678",
            Endereco = "Rua das Palmeiras, 45"
        };

        var result = validator.Validate(user);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void TelefoneValido_DeveSerAceito()
    {
        var user = new User
        {
            Name = "Mariana Souza",
            Email = "mariana@example.com",
            Password = "Senha123",
            BirthDate = DateTime.Today.AddYears(-28),
            CPF = "98765432100",
            Telefone = "2198765432",
            CEP = "87654321",
            Endereco = "Av. Central, 300"
        };

        var result = validator.Validate(user);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void CEPValido_DeveSerAceito()
    {
        var user = ValidUser;
        user.CEP = "13579135";

        var result = validator.Validate(user);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void EnderecoNaoVazio_DeveSerValido()
    {
        var user = ValidUser;
        user.Endereco = "Rua Nova Esperança, 789";

        var result = validator.Validate(user);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void UsuarioComTelefoneDe11Digitos_DeveSerValido()
    {
        var user = ValidUser;
        user.Telefone = "11999999999"; // 11 dígitos

        var result = validator.Validate(user);

        Assert.IsTrue(result.IsValid);
    }


    // ======== TESTES NEGATIVOS =========
    // -------------------------------------

    [TestMethod]
    public void NomeVazio_DeveSerInvalido()
    {
        var user = new User
        {
            Name = "",
            Email = "teste@email.com",
            Password = "Senha123",
            BirthDate = DateTime.Today.AddYears(-20),
            CPF = "12345678901",
            CEP = "12345678",
            Telefone = "11987654321",
            Endereco = "Rua das Flores, 123"
        };

        var result = validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(e => e.PropertyName == "Name"));
    }

    [TestMethod]
    public void EmailInvalido_DeveSerInvalido()
    {
        var user = new User
        {
            Name = "Pedro",
            Email = "email-sem-arroba.com",
            Password = "Senha123",
            BirthDate = DateTime.Today.AddYears(-20),
            CPF = "12345678901",
            CEP = "12345678",
            Telefone = "11987654321",
            Endereco = "Rua das Flores, 123"
        };

        var result = validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(e => e.PropertyName == "Email"));
    }

    [TestMethod]
    public void SenhaSemMaiuscula_DeveSerInvalida()
    {
        var user = new User
        {
            Name = "Maria",
            Email = "maria@email.com",
            Password = "senha123", // sem maiuscula
            BirthDate = DateTime.Today.AddYears(-30),
            CPF = "12345678901",
            CEP = "12345678",
            Telefone = "11987654321",
            Endereco = "Rua das Flores, 123"
        };

        var result = validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(e => e.ErrorMessage.Contains("letra maiúscula")));
    }

    [TestMethod]
    public void UsuarioMenorDe18_DeveSerInvalido()
    {
        var user = new User
        {
            Name = "João",
            Email = "joao@email.com",
            Password = "Senha123",
            BirthDate = DateTime.Today.AddYears(-17).AddDays(1), //ainda nao fez 18
            CPF = "12345678901",
            CEP = "12345678",
            Telefone = "11987654321",
            Endereco = "Rua das Flores, 123"
        };

        var result = validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(e => e.PropertyName == "BirthDate"));
    }

    [TestMethod]
    public void SenhaMenorQue6Caracteres_DeveSerInvalida()
    {
        var user = new User
        {
            Name = "Laura",
            Email = "laura@email.com",
            Password = "A1b", // curta demais
            BirthDate = DateTime.Today.AddYears(-21),
            CPF = "12345678901",
            CEP = "12345678",
            Telefone = "11987654321",
            Endereco = "Rua das Flores, 123"
        };

        var result = validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(e => e.PropertyName == "Password"));
    }

    [TestMethod]
    public void CPFInvalido_DeveSerRejeitado()
    {
        var user = new User
        {
            Name = "Felipe",
            Email = "felipe@example.com",
            Password = "Senha123",
            BirthDate = DateTime.Today.AddYears(-25),
            CPF = "12345", //CPF invalido (curto dms)
            Telefone = "11987654321",
            CEP = "12345678",
            Endereco = "Rua das Flores, 123"
        };

        var result = validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(e => e.PropertyName == "CPF"));
    }

    [TestMethod]
    public void TelefoneInvalido_DeveSerRejeitado()
    {
        var user = new User
        {
            Name = "Fernanda",
            Email = "fernanda@example.com",
            Password = "Senha123",
            BirthDate = DateTime.Today.AddYears(-30),
            CPF = "12345678901",
            Telefone = "123abc4567", //telefone invalido (com letras)
            CEP = "12345678",
            Endereco = "Rua das Flores, 123"
        };

        var result = validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(e => e.PropertyName == "Telefone"));
    }

    [TestMethod]
    public void CEPInvalido_DeveSerRejeitado()
    {
        var user = ValidUser;
        user.CEP = "ABCDE123"; // letras, inválido

        var result = validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(e => e.PropertyName == "CEP"));
    }

    [TestMethod]
    public void EnderecoVazio_DeveSerInvalido()
    {
        var user = ValidUser;
        user.Endereco = "";

        var result = validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(e => e.PropertyName == "Endereco"));
    }

    [TestMethod]
    public void SenhaSemNumero_DeveSerInvalida()
    {
        var user = ValidUser;
        user.Password = "SenhaSemNumero"; // não tem número

        var result = validator.Validate(user);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(e => e.ErrorMessage.Contains("número")));
    }
}
