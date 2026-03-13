// AED-II__Listagem_alunos

//// fazer programa que salva dados de alunos dentro do Vetor, apos apertar salvar, salva em um arquivo .dat
//// todos os dados devem rodar dentro do vetor e serão salvos somente apos salvar
//// as classes devem ser feitas por fora e o vetor deve ser refetente a sua classe
//// funções devem ser executadas conforme escolha do usuario

using System;
using System.IO;

[Serializable]
public class Disciplina
{
    public string CodDisciplina { get; set; }
    public string NomeDisciplina { get; set; }
    public int NotaMin { get; set; }
}

[Serializable]
public class Matricula
{
    public string MatriculaAluno { get; set; }
    public int Nota1 { get; set; }
    public int Nota2 { get; set; }
}

[Serializable]
public class Alunos
{
    public int Matricula { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
}

public static class Cadastro
{
    // Arrays internos
    private static Disciplina[] disciplinas;
    private static Alunos[] alunos;
    private static Matricula[] matriculas;

    private static int contadorDisciplinas = 0;
    private static int contadorAlunos = 0;
    private static int contadorMatriculas = 0;

    // Nomes dos arquivos
    private const string ARQUIVO_DISCIPLINAS = "Disciplinas.dat";
    private const string ARQUIVO_ALUNOS = "Alunos.dat";
    private const string ARQUIVO_MATRICULAS = "Matriculas.dat";

    // Inicializar arrays
    public static void Inicializar(int maxDisciplinas, int maxAlunos, int maxMatriculas)
    {
        disciplinas = new Disciplina[maxDisciplinas];
        alunos = new Alunos[maxAlunos];
        matriculas = new Matricula[maxMatriculas];
    }

    // ═══════════════════════════════════════════
    // MÉTODOS DE CADASTRO
    // ═══════════════════════════════════════════

    public static void CadastrarDisciplina()
    {
        if (contadorDisciplinas >= disciplinas.Length)
        {
            Console.WriteLine("❌ Limite de disciplinas atingido!");
            return;
        }

        Console.WriteLine("\n=== CADASTRO DE DISCIPLINA ===");

        Disciplina disciplina = new Disciplina();

        Console.Write("Código da Disciplina: ");
        disciplina.CodDisciplina = Console.ReadLine();

        Console.Write("Nome da Disciplina: ");
        disciplina.NomeDisciplina = Console.ReadLine();

        Console.Write("Nota Mínima: ");
        disciplina.NotaMin = int.Parse(Console.ReadLine());

        disciplinas[contadorDisciplinas] = disciplina;
        contadorDisciplinas++;

        Console.WriteLine("✓ Disciplina cadastrada com sucesso!");
    }

    public static void CadastrarAluno()
    {
        if (contadorAlunos >= alunos.Length)
        {
            Console.WriteLine("❌ Limite de alunos atingido!");
            return;
        }

        Console.WriteLine("\n=== CADASTRO DE ALUNO ===");

        Alunos aluno = new Alunos();

        Console.Write("Matrícula: ");
        aluno.Matricula = int.Parse(Console.ReadLine());

        Console.Write("Nome: ");
        aluno.Nome = Console.ReadLine();

        Console.Write("Idade: ");
        aluno.Idade = int.Parse(Console.ReadLine());

        alunos[contadorAlunos] = aluno;
        contadorAlunos++;

        Console.WriteLine("✓ Aluno cadastrado com sucesso!");
    }

    public static void CadastrarMatricula()
    {
        if (contadorMatriculas >= matriculas.Length)
        {
            Console.WriteLine("❌ Limite de matrículas atingido!");
            return;
        }

        Console.WriteLine("\n=== CADASTRO DE MATRÍCULA ===");

        Matricula matricula = new Matricula();

        Console.Write("Matrícula do Aluno: ");
        matricula.MatriculaAluno = Console.ReadLine();

        Console.Write("Nota 1: ");
        matricula.Nota1 = int.Parse(Console.ReadLine());

        Console.Write("Nota 2: ");
        matricula.Nota2 = int.Parse(Console.ReadLine());

        matriculas[contadorMatriculas] = matricula;
        contadorMatriculas++;

        Console.WriteLine("✓ Matrícula cadastrada com sucesso!");
    }

    // ═══════════════════════════════════════════
    // MÉTODOS DE OBTENÇÃO DE DADOS
    // ═══════════════════════════════════════════

    public static Disciplina[] ObterDisciplinas()
    {
        Disciplina[] resultado = new Disciplina[contadorDisciplinas];
        for (int i = 0; i < contadorDisciplinas; i++)
        {
            resultado[i] = disciplinas[i];
        }
        return resultado;
    }

    public static Alunos[] ObterAlunos()
    {
        Alunos[] resultado = new Alunos[contadorAlunos];
        for (int i = 0; i < contadorAlunos; i++)
        {
            resultado[i] = alunos[i];
        }
        return resultado;
    }

    public static Matricula[] ObterMatriculas()
    {
        Matricula[] resultado = new Matricula[contadorMatriculas];
        for (int i = 0; i < contadorMatriculas; i++)
        {
            resultado[i] = matriculas[i];
        }
        return resultado;
    }

    // ═══════════════════════════════════════════
    // SALVAR EM ARQUIVOS .DAT SEPARADOS
    // ═══════════════════════════════════════════

    public static void SalvarDisciplinas()
    {
        try
        {
            using (FileStream fs = new FileStream(ARQUIVO_DISCIPLINAS, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Salvar contador
                writer.Write(contadorDisciplinas);

                // Salvar cada disciplina
                for (int i = 0; i < contadorDisciplinas; i++)
                {
                    writer.Write(disciplinas[i].CodDisciplina);
                    writer.Write(disciplinas[i].NomeDisciplina);
                    writer.Write(disciplinas[i].NotaMin);
                }
            }

            Console.WriteLine($"✓ {contadorDisciplinas} disciplinas salvas em '{ARQUIVO_DISCIPLINAS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao salvar disciplinas: {ex.Message}");
        }
    }

    public static void SalvarAlunos()
    {
        try
        {
            using (FileStream fs = new FileStream(ARQUIVO_ALUNOS, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Salvar contador
                writer.Write(contadorAlunos);

                // Salvar cada aluno
                for (int i = 0; i < contadorAlunos; i++)
                {
                    writer.Write(alunos[i].Matricula);
                    writer.Write(alunos[i].Nome);
                    writer.Write(alunos[i].Idade);
                }
            }

            Console.WriteLine($"✓ {contadorAlunos} alunos salvos em '{ARQUIVO_ALUNOS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao salvar alunos: {ex.Message}");
        }
    }

    public static void SalvarMatriculas()
    {
        try
        {
            using (FileStream fs = new FileStream(ARQUIVO_MATRICULAS, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Salvar contador
                writer.Write(contadorMatriculas);

                // Salvar cada matrícula
                for (int i = 0; i < contadorMatriculas; i++)
                {
                    writer.Write(matriculas[i].MatriculaAluno);
                    writer.Write(matriculas[i].Nota1);
                    writer.Write(matriculas[i].Nota2);
                }
            }

            Console.WriteLine($"✓ {contadorMatriculas} matrículas salvas em '{ARQUIVO_MATRICULAS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao salvar matrículas: {ex.Message}");
        }
    }

    // Salvar todos os arquivos de uma vez
    public static void SalvarTodos()
    {
        Console.WriteLine("\n💾 Salvando todos os dados...");
        SalvarDisciplinas();
        SalvarAlunos();
        SalvarMatriculas();
        Console.WriteLine("✓ Todos os dados foram salvos com sucesso!\n");
    }

    // ═══════════════════════════════════════════
    // CARREGAR DE ARQUIVOS .DAT SEPARADOS
    // ═══════════════════════════════════════════

    public static void CarregarDisciplinas()
    {
        try
        {
            if (!File.Exists(ARQUIVO_DISCIPLINAS))
            {
                Console.WriteLine($"⚠ Arquivo '{ARQUIVO_DISCIPLINAS}' não encontrado.");
                return;
            }

            using (FileStream fs = new FileStream(ARQUIVO_DISCIPLINAS, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Carregar contador
                contadorDisciplinas = reader.ReadInt32();

                // Carregar cada disciplina
                for (int i = 0; i < contadorDisciplinas; i++)
                {
                    disciplinas[i] = new Disciplina
                    {
                        CodDisciplina = reader.ReadString(),
                        NomeDisciplina = reader.ReadString(),
                        NotaMin = reader.ReadInt32()
                    };
                }
            }

            Console.WriteLine($"✓ {contadorDisciplinas} disciplinas carregadas de '{ARQUIVO_DISCIPLINAS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao carregar disciplinas: {ex.Message}");
        }
    }

    public static void CarregarAlunos()
    {
        try
        {
            if (!File.Exists(ARQUIVO_ALUNOS))
            {
                Console.WriteLine($"⚠ Arquivo '{ARQUIVO_ALUNOS}' não encontrado.");
                return;
            }

            using (FileStream fs = new FileStream(ARQUIVO_ALUNOS, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Carregar contador
                contadorAlunos = reader.ReadInt32();

                // Carregar cada aluno
                for (int i = 0; i < contadorAlunos; i++)
                {
                    alunos[i] = new Alunos
                    {
                        Matricula = reader.ReadInt32(),
                        Nome = reader.ReadString(),
                        Idade = reader.ReadInt32()
                    };
                }
            }

            Console.WriteLine($"✓ {contadorAlunos} alunos carregados de '{ARQUIVO_ALUNOS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao carregar alunos: {ex.Message}");
        }
    }

    public static void CarregarMatriculas()
    {
        try
        {
            if (!File.Exists(ARQUIVO_MATRICULAS))
            {
                Console.WriteLine($"⚠ Arquivo '{ARQUIVO_MATRICULAS}' não encontrado.");
                return;
            }

            using (FileStream fs = new FileStream(ARQUIVO_MATRICULAS, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Carregar contador
                contadorMatriculas = reader.ReadInt32();

                // Carregar cada matrícula
                for (int i = 0; i < contadorMatriculas; i++)
                {
                    matriculas[i] = new Matricula
                    {
                        MatriculaAluno = reader.ReadString(),
                        Nota1 = reader.ReadInt32(),
                        Nota2 = reader.ReadInt32()
                    };
                }
            }

            Console.WriteLine($"✓ {contadorMatriculas} matrículas carregadas de '{ARQUIVO_MATRICULAS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao carregar matrículas: {ex.Message}");
        }
    }

    // Carregar todos os arquivos de uma vez
    public static void CarregarTodos()
    {
        Console.WriteLine("\n📂 Carregando dados salvos...");
        CarregarDisciplinas();
        CarregarAlunos();
        CarregarMatriculas();
        Console.WriteLine("✓ Carregamento concluído!\n");
    }
}

// Main code
public class Program
{
    public static void Main()
    {
        // Inicializar sistema
        Cadastro.Inicializar(maxDisciplinas: 50, maxAlunos: 200, maxMatriculas: 500);

        // Carregar dados salvos anteriormente
        Cadastro.CarregarTodos();

        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║   SISTEMA ACADÊMICO - MENU         ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.WriteLine("1  - Cadastrar Disciplina\n");
            Console.WriteLine("2  - Cadastrar Aluno\n");
            Console.WriteLine("3  - Cadastrar Matrícula\n");
            Console.WriteLine("4  - Consultar Dados\n");
            Console.WriteLine("5  - Salvar Disciplinas\n");
            Console.WriteLine("6  - Salvar Alunos\n");
            Console.WriteLine("7  - Salvar Matrículas\n");
            Console.WriteLine("8  - Salvar Todos\n");
            Console.WriteLine("9  - Carregar Disciplinas\n");
            Console.WriteLine("10 - Carregar Alunos\n");
            Console.WriteLine("11 - Carregar Matrículas\n");
            Console.WriteLine("12 - Carregar Todos\n");
            Console.WriteLine("0  - Sair\n");
            Console.Write("\nEscolha uma opção: ");

            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Cadastro.CadastrarDisciplina();
                    break;

                case 2:
                    Cadastro.CadastrarAluno();
                    break;

                case 3:
                    Cadastro.CadastrarMatricula();
                    break;

                case 4:
                    MenuConsulta();
                    break;

                case 5:
                    Cadastro.SalvarDisciplinas();
                    break;

                case 6:
                    Cadastro.SalvarAlunos();
                    break;

                case 7:
                    Cadastro.SalvarMatriculas();
                    break;

                case 8:
                    Cadastro.SalvarTodos();
                    break;

                case 9:
                    Cadastro.CarregarDisciplinas();
                    break;

                case 10:
                    Cadastro.CarregarAlunos();
                    break;

                case 11:
                    Cadastro.CarregarMatriculas();
                    break;

                case 12:
                    Cadastro.CarregarTodos();
                    break;

                case 0:
                    Console.Write("\n💾 Deseja salvar antes de sair? (S/N): ");
                    string resposta = Console.ReadLine().ToUpper();
                    if (resposta == "S")
                    {
                        Cadastro.SalvarTodos();
                    }
                    continuar = false;
                    Console.WriteLine("\n👋 Até logo!");
                    break;

                default:
                    Console.WriteLine("\n❌ Opção inválida!");
                    break;
            }

            if (continuar)
            {
                Console.Write("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }
        }
    }

    static void MenuConsulta()
    {
        Console.Clear();
        Console.WriteLine("\n╔════════════════════════════════════╗");
        Console.WriteLine("║         CONSULTAR DADOS            ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.WriteLine("1 - Listar Disciplinas");
        Console.WriteLine("2 - Listar Alunos");
        Console.WriteLine("3 - Listar Matrículas");
        Console.Write("Escolha: ");

        int opcao = int.Parse(Console.ReadLine());

        if (opcao == 1)
        {
            Disciplina[] disciplinas = Cadastro.ObterDisciplinas();
            Console.WriteLine($"\n╔════════════════════════════════════╗");
            Console.WriteLine($"║  {disciplinas.Length} DISCIPLINAS CADASTRADAS");
            Console.WriteLine($"╚════════════════════════════════════╝");

            for (int i = 0; i < disciplinas.Length; i++)
            {
                Console.WriteLine($"\n{i + 1}. Código: {disciplinas[i].CodDisciplina}");
                Console.WriteLine($"   Nome: {disciplinas[i].NomeDisciplina}");
                Console.WriteLine($"   Nota Mínima: {disciplinas[i].NotaMin}");
                Console.WriteLine("   " + new string('-', 35));
            }
        }
        else if (opcao == 2)
        {
            Alunos[] alunos = Cadastro.ObterAlunos();
            Console.WriteLine($"\n╔════════════════════════════════════╗");
            Console.WriteLine($"║  {alunos.Length} ALUNOS CADASTRADOS");
            Console.WriteLine($"╚════════════════════════════════════╝");

            for (int i = 0; i < alunos.Length; i++)
            {
                Console.WriteLine($"\n{i + 1}. Matrícula: {alunos[i].Matricula}");
                Console.WriteLine($"   Nome: {alunos[i].Nome}");
                Console.WriteLine($"   Idade: {alunos[i].Idade} anos");
                Console.WriteLine("   " + new string('-', 35));
            }
        }
        else if (opcao == 3)
        {
            Matricula[] matriculas = Cadastro.ObterMatriculas();
            Console.WriteLine($"\n╔════════════════════════════════════╗");
            Console.WriteLine($"║  {matriculas.Length} MATRÍCULAS CADASTRADAS");
            Console.WriteLine($"╚════════════════════════════════════╝");

            for (int i = 0; i < matriculas.Length; i++)
            {
                Console.WriteLine($"\n{i + 1}. Matrícula: {matriculas[i].MatriculaAluno}");
                Console.WriteLine($"   Nota 1: {matriculas[i].Nota1}");
                Console.WriteLine($"   Nota 2: {matriculas[i].Nota2}");
                Console.WriteLine($"   Média: {(matriculas[i].Nota1 + matriculas[i].Nota2) / 2.0:F1}");
                Console.WriteLine("   " + new string('-', 35));
            }
        }
    }
}
