using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Bookworm_Desktop;
using SugmaState;
using TestsOrSetups.Properties;

namespace TestsOrSetups
{
    class Program
    {

        static void Main(string[] args)
        {
            //var funcTask = Task.Run(() =>
            //{
            //    Console.WriteLine("Adicionando Funcionários");
            //    AddFuncEntries();
            //    Console.WriteLine("Funcionários adicionados");
            //});

            //var clientTask = Task.Run(() =>
            //{
            //    Console.WriteLine("Adicionando Leitores");
            //    AddClientEntries();
            //    Console.WriteLine("Leitores adicionados");
            //});


            //funcTask.GetAwaiter().GetResult();
            //clientTask.GetAwaiter().GetResult();

            AddEventos();

            Console.WriteLine("Tasks completas");

            Console.WriteLine("Saindo em ");

            for (int i = 3; i > 0; i--)
            {
                Console.Write($"\r{i}");
                Thread.Sleep(1000);
            }
            Console.WriteLine("\nSaindo");
        }

        static void AddEventos()
        {
            using (var db = new TCCFEntities())
            {
                db.tblEvento.RemoveRange(db.tblEvento);

                db.tblEvento.AddRange(new[]
                {
                    new tblEvento()
                    {
                        Email = "apresentador@gamer.com",
                        Titulo = "02/12/2020 - Apresentação do TCC de Informática",
                        Descricao = "Apresentação do trabalho de conclusão de curso do terceiro ano de informática",
                        Responsavel = "Luiz Ricardo"
                    },

                    new tblEvento()
                    {
                        Email = "apresentador@gamer.com",
                        Titulo = "03/12/2020 - Apresentação do TCC de Informática",
                        Descricao = "Apresentação do trabalho de conclusão de curso do terceiro ano de informática",
                        Responsavel = "Luiz Ricardo"
                    },
                });

                db.SaveChanges();
            }
        }
        static void AddClientEntries()
        {
            using (var db = new TCCFEntities())
            {
                db.tblFavoritos.RemoveRange(db.tblFavoritos);
                db.tblReserva.RemoveRange(db.tblReserva);
                db.tblLeitor.RemoveRange(db.tblLeitor);
                db.tblTipoLeitor.RemoveRange(db.tblTipoLeitor);

                db.tblGeneroProduto.RemoveRange(db.tblGeneroProduto);
                db.tblGenero.RemoveRange(db.tblGenero);
                db.tblProduto.RemoveRange(db.tblProduto);

                var TipoLeitorNormal = db.tblTipoLeitor.Add(new tblTipoLeitor
                {
                    TipoLeitor = "Normal"
                });



                var generoAventura = db.tblGenero.Add(new tblGenero()
                {
                    NomeGenero = "Aventura"
                });

                var generoFantasia = db.tblGenero.Add(new tblGenero()
                {
                    NomeGenero = "Fantasia"
                });

                var generoRomance = db.tblGenero.Add(new tblGenero()
                {
                    NomeGenero = "Romance"
                });

                var generoTristeza = db.tblGenero.Add(new tblGenero()
                {
                    NomeGenero = "Tristeza"
                });

                var generoTerror = db.tblGenero.Add(new tblGenero()
                {
                    NomeGenero = "Terror"
                });

                var generoSuspense = db.tblGenero.Add(new tblGenero()
                {
                    NomeGenero = "Suspense"
                });

                var generoCatólico = db.tblGenero.Add(new tblGenero()
                {
                    NomeGenero = "Católico"
                });

                var generoAnimação = db.tblGenero.Add(new tblGenero()
                {
                    NomeGenero = "Animação"
                });

                var generoArte = db.tblGenero.Add(new tblGenero()
                {
                    NomeGenero = "Arte"
                });

                var generoMangá = db.tblGenero.Add(new tblGenero()
                {
                    NomeGenero = "Mangá"
                });

                var generoRock = db.tblGenero.Add(new tblGenero()
                {
                    NomeGenero = "Rock"
                });

                var livroSociedadeAnel = db.tblProduto.Add(new tblProduto()
                {
                    ISBN = "85-336-1337-7",
                    NomeLivro = "A Sociedade do Anel",
                    AutoresLivro = "J. R. R. Tolkien",
                    AnoEdicao = new DateTime(2003, 1, 1),
                    Setor = 1,
                    Fileira = 1,
                    Prateleira = 1,
                    TipoProduto = "Livro",
                    Editora = "George Allen & Unwin",
                    DescricaoProd = "Este livro leva-nos para um mundo onde anéis forjados por anões reinam com o seu poder. No entanto, há um anel que é o mais poderoso de todos e, se cair em mãos erradas, pode ter um poder destrutivo. É o que, infelizmente acontece. Mas depois, este perde-se e passado algum tempo, vai parar às mãos de um hobbit chamado Frodo Bolseiro e, este, ao não saber o que fazer com ele, decide consultar um amigo feiticeiro de nome Gandalf, o Cinzento. Fica decidido ir à bela cidade élfica de Valfenda para fazer um conselho, ministrado pelo sábio Elrond, que determinará quem vai à terra sombria de Mordor e destruir lá, na Montanha da Perdição, o Anel, o único lugar em que tal artefato pode ser destruído. São escolhido o hobbit e mais oito companheiros para realizar tal perigosa missão: os também hobbits Samwise \"Sam\" Gamgi (fiel companheiro de Frodo), Meriadoc \"Merry\" Brandebuque e Peregrin \"Pippin\" Tûk (representando os hobbits), o mago Gandalf (representando a ordem mágica dos Istari), os humanos Aragorn e Boromir (representando os homens), o elfo Legolas (representando os elfos) e o anão Gimli (representando os anões).",
                    ImagemProd = Resources.a_sociedade_do_anel.ToBytes()
                });
                db.tblGeneroProduto.Add(new tblGeneroProduto
                {
                    tblGenero = generoAventura,
                    tblProduto = livroSociedadeAnel
                });
                db.tblGeneroProduto.Add(new tblGeneroProduto
                {
                    tblGenero = generoFantasia,
                    tblProduto = livroSociedadeAnel
                });

                var livroElaESeuGato = db.tblProduto.Add(new tblProduto()
                {
                    ISBN = "978-85-8362-234-5",
                    NomeLivro = "Ela e o Seu Gato",
                    AutoresLivro = "Makoto Shinkai, Tsubasa Yamaguchi",
                    AnoEdicao = new DateTime(2006, 1, 1),
                    Setor = 1,
                    Fileira = 2,
                    Prateleira = 1,
                    TipoProduto = "Livro",
                    Editora = "NewPOP",
                    DescricaoProd = "\"Era um dia de chuva, no começo da primavera. Eu fui acolhido por ela;\" Um gato e uma garota que mora sozinha se conhecem na primavera... Ao viver sozinha, ela aprende a alegria e a solidão de ser independente, enquanto o gato, que recebeu o nome de Chobi, descobri a existência do mundo através dessa garota. O tempo desses dois passa lentamente, mas a severidade do mundo acaba por alcançá-la...",
                    ImagemProd = Resources.ela_e_seu_gato.ToBytes(),
                });

                db.tblGeneroProduto.Add(new tblGeneroProduto
                {
                    tblGenero = generoRomance,
                    tblProduto = livroElaESeuGato
                });
                db.tblGeneroProduto.Add(new tblGeneroProduto
                {
                    tblGenero = generoTristeza,
                    tblProduto = livroElaESeuGato
                });

                var livroZelda = db.tblProduto.Add(new tblProduto()
                { // 
                    ISBN = "978-85-4261-093-2",
                    NomeLivro = "The Legend Of Zelda: Majora's Mask - A Link To The",
                    AutoresLivro = "Akira Himekawa",
                    AnoEdicao = new DateTime(2018, 1, 1),
                    Setor = 1,
                    Fileira = 3,
                    Prateleira = 1,
                    TipoProduto = "Revista",
                    Editora = "Planet Manga",
                    DescricaoProd = "Em sua viagem de treinamento, Link tem sua ocarina roubada por uma estranha criatura mascarada e é transformado em um deku scrub! Agora, Link deve correr contra o tempo para salvar a Cidade Relógio da destruição iminente e recuperar a Máscara de Majora! Na segunda história, Link acorda inquieto ao ouvir um chamado.A princesa Zelda foi raptada pelo maligno Agahnim, um seguidor de Ganon que deseja a Triforce para si.Link parte em uma jornada para enfrentar o feiticeiro e descobrir a verdade sobre seu passado.",
                    ImagemProd = Resources.the_legend_of_zelda.ToBytes()
                });
                db.tblGeneroProduto.Add(new tblGeneroProduto
                {
                    tblGenero = generoAventura,
                    tblProduto = livroZelda
                });
                db.tblGeneroProduto.Add(new tblGeneroProduto
                {
                    tblGenero = generoFantasia,
                    tblProduto = livroZelda
                });

                var livroZelda2 = db.tblProduto.Add(new tblProduto()
                { // 
                    ISBN = "978-85-4261-093-2",
                    NomeLivro = "The Legend Of Zelda: Majora's Mask - A Link To The",
                    AutoresLivro = "Akira Himekawa",
                    AnoEdicao = new DateTime(2018, 1, 1),
                    Setor = 1,
                    Fileira = 4,
                    Prateleira = 1,
                    TipoProduto = "Revista",
                    Editora = "Planet Manga",
                    DescricaoProd = "Em sua viagem de treinamento, Link tem sua ocarina roubada por uma estranha criatura mascarada e é transformado em um deku scrub! Agora, Link deve correr contra o tempo para salvar a Cidade Relógio da destruição iminente e recuperar a Máscara de Majora! Na segunda história, Link acorda inquieto ao ouvir um chamado.A princesa Zelda foi raptada pelo maligno Agahnim, um seguidor de Ganon que deseja a Triforce para si.Link parte em uma jornada para enfrentar o feiticeiro e descobrir a verdade sobre seu passado.",
                    ImagemProd = Resources.the_legend_of_zelda.ToBytes()
                });
                db.tblGeneroProduto.Add(new tblGeneroProduto
                {
                    tblGenero = generoAventura,
                    tblProduto = livroZelda2
                });
                db.tblGeneroProduto.Add(new tblGeneroProduto
                {
                    tblGenero = generoFantasia,
                    tblProduto = livroZelda2
                });


                var (salt, senha) = Authentication.RegisterUser("12345");
                db.tblLeitor.Add(new tblLeitor()
                {
                    tblTipoLeitor = TipoLeitorNormal,
                    Nome = "Leitor Sávio Alves",
                    CPF = "47939319876",
                    RG = "506039997",
                    DataCadastro = DateTime.Now,
                    DataNasc = new DateTime(2003, 06, 06),
                    Email = "savioacp@gmail.com",
                    Endereco = "Rua Castanhal, 165",
                    ImagemLeitor = File.ReadAllBytes(@"C:\Users\CakeIsALie\Pictures\23015585.jpg"),
                    Telefone = "11968518997",
                    Salt = salt,
                    Senha = senha,
                    tblFavoritos = new[]
                    {
                        new tblFavoritos
                        {
                            tblProduto = livroSociedadeAnel
                        }
                    },
                    tblReserva = new[]
                    {
                        new tblReserva
                        {
                            DataReserva = DateTime.Now,
                            tblProduto = livroSociedadeAnel
                        }
                    }
                });

                (salt, senha) = Authentication.RegisterUser("12345");
                db.tblLeitor.Add(new tblLeitor()
                {
                    tblTipoLeitor = TipoLeitorNormal,
                    Nome = "Leitora Juliana Craveiro",
                    RG = "123456789",
                    Telefone = "11987654321",
                    CPF = "12345678910",
                    DataCadastro = DateTime.Now,
                    DataNasc = new DateTime(2003, 04, 22),
                    Email = "julic@gmail.com",
                    Endereco = "Rua Lá pá",
                    ImagemLeitor = File.ReadAllBytes(@"C:\Users\CakeIsALie\source\repos\Bookworm Desktop\Bookworm Desktop\Resources\Images\perfil.jpg"),
                    Salt = salt,
                    Senha = senha,
                    tblReserva = new[]
                    {
                        new tblReserva
                        {
                            DataReserva = DateTime.Now,
                            tblProduto = livroElaESeuGato
                        }
                    }
                });

                db.SaveChanges();
            }
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
                    NomeCargo = "Desenvolvimento",
                });

                db.tblCargo.Add(new tblCargo
                {
                    NivelAcesso = 0,
                    NomeCargo = "Diretoria",
                });

                db.tblCargo.Add(new tblCargo
                {
                    NivelAcesso = 1,
                    NomeCargo = "Secretaria",
                });

                var cargoEstagiário = db.tblCargo.Add(new tblCargo
                {
                    NivelAcesso = 2,
                    NomeCargo = "Estágio",
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
