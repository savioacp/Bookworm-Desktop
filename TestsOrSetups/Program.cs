using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bookworm_Desktop;
using SugmaState;

namespace TestsOrSetups
{
	class Program
	{

		static void Main(string[] args)
		{
			Console.WriteLine("Adicionando Funcionários");
			AddFuncEntries();
			Console.ReadKey();
		}

		static void AddFuncEntries()
		{
			using (var db = new TCCFEntities())
			{
				db.tblCargo.RemoveRange(db.tblCargo);
				db.tblFuncionario.RemoveRange(db.tblFuncionario);
				var cargoDesenvolvedor = db.tblCargo.Add(new tblCargo
				{
					NivelAcesso = 0,
					NomeCargo = "Desenvolvedor",
				}) ;

				db.tblCargo.Add(new tblCargo
				{
					NivelAcesso = 0,
					NomeCargo = "Diretor",
				});

				db.tblCargo.Add(new tblCargo
				{
					NivelAcesso = 1,
					NomeCargo = "Secretaria",
				});

				db.SaveChanges();

				(string salt, string senha) = Authentication.RegisterUser("admin");
				Console.WriteLine($"Senha: {senha}\nSalt: {salt}\nSenha original: {"admin"}");
				Console.WriteLine(Authentication.LogUserIn("admin", senha, salt));

				db.tblFuncionario.Add(new tblFuncionario
				{
					Nome = "Sávio Alves Cabelo Pereira",
					RG = "506039997",
					Telefone = "11968518997",
					CPF = "47939319876",
					Email = "developer@bookworm.com",
					Endereco = "Rua Castanhal, 165",
					tblCargo = cargoDesenvolvedor,
					ImagemFunc = File.ReadAllBytes(@"C:\Users\CakeIsALie\Pictures\23015585.jpg"),
					Salt = salt,
					Senha = senha
				});

				(salt, senha) = Authentication.RegisterUser("admin");

				db.tblFuncionario.Add(new tblFuncionario
				{
					Nome = "Juliana Craveiro Fusco",
					RG = "123456789",
					Telefone = "11987654321",
					CPF = "12345678901",
					Email = "juliana.fusco@bookworm.com",
					Endereco = "Rua Lá pá",
					tblCargo = cargoDesenvolvedor,
					ImagemFunc = File.ReadAllBytes(@"C:\Users\CakeIsALie\source\repos\Bookworm Desktop\Bookworm Desktop\Resources\Images\perfil.jpg"),
					Salt = salt,
					Senha = senha
				});


				(salt, senha) = Authentication.RegisterUser("admin");

				db.tblFuncionario.Add(new tblFuncionario
				{
					Nome = "Guilherme Souza Panza",
					RG = "123456789",
					Telefone = "11987654321",
					CPF = "12345678901",
					Email = "guilherme.panza@bookworm.com",
					Endereco = "Rua Pan zza",
					tblCargo = cargoDesenvolvedor,
					ImagemFunc = File.ReadAllBytes(@"C:\Users\CakeIsALie\Pictures\guilherme.jpg"),
					Salt = salt,
					Senha = senha
				});

				(salt, senha) = Authentication.RegisterUser("admin");
				
				db.tblFuncionario.Add(new tblFuncionario
				{
					Nome = "Beatriz Silvério Martins dos Santos",
					RG = "123456789",
					Telefone = "11987654321",
					CPF = "12345678901",
					Email = "beatriz.santos@bookworm.com",
					Endereco = "Rua Abandonou a ZL",
					tblCargo = cargoDesenvolvedor,
					ImagemFunc = File.ReadAllBytes(@"C:\Users\CakeIsALie\Pictures\beatrix.jpg"),
					Salt = salt,
					Senha = senha
				});

				db.SaveChanges();

			}
		}
	}
}
