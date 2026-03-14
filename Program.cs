// AED-II__Listagem_alunos

//// fazer programa que salva dados de alunos dentro do Vetor, apos apertar salvar, salva em um arquivo .dat
//// todos os dados devem rodar dentro do vetor e serão salvos somente apos salvar
//// as classes devem ser feitas por fora e o vetor deve ser refetente a sua classe
//// funções devem ser executadas conforme escolha do usuario

using System;
using System.IO;

public class Disciplina
{
    public string CodDisciplina { get; set; }
    public string NomeDisciplina { get; set; }
    public int NotaMin { get; set; }
}

public class Matricula
{
    public string MatriculaAluno { get; set; }
    public int Nota1 { get; set; }
    public int Nota2 { get; set; }
}

public class Aluno
{
    public int Matricula { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
}

public static class Cadastro
{
    // Arrays internos
    private static Disciplina[] disciplinas;
    private static Aluno[] alunos;
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
        alunos = new Aluno[maxAlunos];
        matriculas = new Matricula[maxMatriculas];
    }

    // Método auxiliar para leitura segura de inteiros
    private static bool LerInteiro(string mensagem, out int valor)
    {
        Console.Write(mensagem);
        if (!int.TryParse(Console.ReadLine(), out valor))
        {
            Console.WriteLine("Entrada invalida! Digite um numero inteiro.");
            return false;
        }
        return true;
    }

    // Método auxiliar para leitura segura de strings não vazias
    private static bool LerString(string mensagem, out string valor)
    {
        Console.Write(mensagem);
        valor = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(valor))
        {
            Console.WriteLine("Entrada invalida! O campo nao pode ser vazio.");
            return false;
        }
        return true;
    }

    // ═══════════════════════════════════════════
    // MÉTODOS DE CADASTRO
    // ═══════════════════════════════════════════

    public static void CadastrarDisciplina()
    {
        if (contadorDisciplinas >= disciplinas.Length)
        {
            Console.WriteLine("Limite de disciplinas atingido!");
            return;
        }

        Console.WriteLine("\n=== CADASTRO DE DISCIPLINA ===");

        Disciplina disciplina = new Disciplina();

        if (!LerString("Codigo da Disciplina: ", out string cod)) return;
        disciplina.CodDisciplina = cod;

        if (!LerString("Nome da Disciplina: ", out string nome)) return;
        disciplina.NomeDisciplina = nome;

        if (!LerInteiro("Nota Minima: ", out int notaMin)) return;
        if (notaMin < 0 || notaMin > 100)
        {
            Console.WriteLine("Nota minima deve ser entre 0 e 100.");
            return;
        }
        disciplina.NotaMin = notaMin;

        disciplinas[contadorDisciplinas] = disciplina;
        contadorDisciplinas++;

        Console.WriteLine("Disciplina cadastrada com sucesso!");
    }

    public static void CadastrarAluno()
    {
        if (contadorAlunos >= alunos.Length)
        {
            Console.WriteLine("Limite de alunos atingido!");
            return;
        }

        Console.WriteLine("\n=== CADASTRO DE ALUNO ===");

        Aluno aluno = new Aluno();

        if (!LerInteiro("Matricula: ", out int matricula)) return;
        aluno.Matricula = matricula;

        if (!LerString("Nome: ", out string nome)) return;
        aluno.Nome = nome;

        if (!LerInteiro("Idade: ", out int idade)) return;
        if (idade <= 0 || idade > 150)
        {
            Console.WriteLine("Idade invalida!");
            return;
        }
        aluno.Idade = idade;

        alunos[contadorAlunos] = aluno;
        contadorAlunos++;

        Console.WriteLine("Aluno cadastrado com sucesso!");
    }

    public static void CadastrarMatricula()
    {
        if (contadorMatriculas >= matriculas.Length)
        {
            Console.WriteLine("Limite de matriculas atingido!");
            return;
        }

        Console.WriteLine("\n=== CADASTRO DE MATRICULA ===");

        Matricula matricula = new Matricula();

        if (!LerString("Matricula do Aluno: ", out string matAluno)) return;
        matricula.MatriculaAluno = matAluno;

        if (!LerInteiro("Nota 1: ", out int nota1)) return;
        if (nota1 < 0 || nota1 > 100)
        {
            Console.WriteLine("Nota deve ser entre 0 e 100.");
            return;
        }
        matricula.Nota1 = nota1;

        if (!LerInteiro("Nota 2: ", out int nota2)) return;
        if (nota2 < 0 || nota2 > 100)
        {
            Console.WriteLine("Nota deve ser entre 0 e 100.");
            return;
        }
        matricula.Nota2 = nota2;

        matriculas[contadorMatriculas] = matricula;
        contadorMatriculas++;

        Console.WriteLine("Matricula cadastrada com sucesso!");
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

    public static Aluno[] ObterAlunos()
    {
        Aluno[] resultado = new Aluno[contadorAlunos];
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
                writer.Write(contadorDisciplinas);

                for (int i = 0; i < contadorDisciplinas; i++)
                {
                    writer.Write(disciplinas[i].CodDisciplina);
                    writer.Write(disciplinas[i].NomeDisciplina);
                    writer.Write(disciplinas[i].NotaMin);
                }
            }

            Console.WriteLine($"{contadorDisciplinas} disciplinas salvas em '{ARQUIVO_DISCIPLINAS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar disciplinas: {ex.Message}");
        }
    }

    public static void SalvarAlunos()
    {
        try
        {
            using (FileStream fs = new FileStream(ARQUIVO_ALUNOS, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(contadorAlunos);

                for (int i = 0; i < contadorAlunos; i++)
                {
                    writer.Write(alunos[i].Matricula);
                    writer.Write(alunos[i].Nome);
                    writer.Write(alunos[i].Idade);
                }
            }

            Console.WriteLine($"{contadorAlunos} alunos salvos em '{ARQUIVO_ALUNOS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar alunos: {ex.Message}");
        }
    }

    public static void SalvarMatriculas()
    {
        try
        {
            using (FileStream fs = new FileStream(ARQUIVO_MATRICULAS, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(contadorMatriculas);

                for (int i = 0; i < contadorMatriculas; i++)
                {
                    writer.Write(matriculas[i].MatriculaAluno);
                    writer.Write(matriculas[i].Nota1);
                    writer.Write(matriculas[i].Nota2);
                }
            }

            Console.WriteLine($"{contadorMatriculas} matriculas salvas em '{ARQUIVO_MATRICULAS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar matriculas: {ex.Message}");
        }
    }

    public static void SalvarTodos()
    {
        Console.WriteLine("\nSalvando todos os dados...");
        SalvarDisciplinas();
        SalvarAlunos();
        SalvarMatriculas();
        Console.WriteLine("Todos os dados foram salvos com sucesso!\n");
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
                Console.WriteLine($"Arquivo '{ARQUIVO_DISCIPLINAS}' nao encontrado.");
                return;
            }

            using (FileStream fs = new FileStream(ARQUIVO_DISCIPLINAS, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                contadorDisciplinas = reader.ReadInt32();

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

            Console.WriteLine($"{contadorDisciplinas} disciplinas carregadas de '{ARQUIVO_DISCIPLINAS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar disciplinas: {ex.Message}");
        }
    }

    public static void CarregarAlunos()
    {
        try
        {
            if (!File.Exists(ARQUIVO_ALUNOS))
            {
                Console.WriteLine($"Arquivo '{ARQUIVO_ALUNOS}' nao encontrado.");
                return;
            }

            using (FileStream fs = new FileStream(ARQUIVO_ALUNOS, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                contadorAlunos = reader.ReadInt32();

                for (int i = 0; i < contadorAlunos; i++)
                {
                    alunos[i] = new Aluno
                    {
                        Matricula = reader.ReadInt32(),
                        Nome = reader.ReadString(),
                        Idade = reader.ReadInt32()
                    };
                }
            }

            Console.WriteLine($"{contadorAlunos} alunos carregados de '{ARQUIVO_ALUNOS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar alunos: {ex.Message}");
        }
    }

    public static void CarregarMatriculas()
    {
        try
        {
            if (!File.Exists(ARQUIVO_MATRICULAS))
            {
                Console.WriteLine($"Arquivo '{ARQUIVO_MATRICULAS}' nao encontrado.");
                return;
            }

            using (FileStream fs = new FileStream(ARQUIVO_MATRICULAS, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                contadorMatriculas = reader.ReadInt32();

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

            Console.WriteLine($"{contadorMatriculas} matriculas carregadas de '{ARQUIVO_MATRICULAS}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar matriculas: {ex.Message}");
        }
    }

    public static void CarregarTodos()
    {
        Console.WriteLine("\nCarregando dados salvos...");
        CarregarDisciplinas();
        CarregarAlunos();
        CarregarMatriculas();
        Console.WriteLine("Carregamento concluido!\n");
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
            try
            {
                Console.Clear();
            }
            catch
            {
                // Ignora erro caso o terminal não suporte Clear
            }

            Console.WriteLine("========================================");
            Console.WriteLine("   SISTEMA ACADEMICO - MENU             ");
            Console.WriteLine("========================================");
            Console.WriteLine("1  - Cadastrar Disciplina");
            Console.WriteLine("2  - Cadastrar Aluno");
            Console.WriteLine("3  - Cadastrar Matricula");
            Console.WriteLine("4  - Consultar Dados");
            Console.WriteLine("5  - Salvar Disciplinas");
            Console.WriteLine("6  - Salvar Alunos");
            Console.WriteLine("7  - Salvar Matriculas");
            Console.WriteLine("8  - Salvar Todos");
            Console.WriteLine("9  - Carregar Disciplinas");
            Console.WriteLine("10 - Carregar Alunos");
            Console.WriteLine("11 - Carregar Matriculas");
            Console.WriteLine("12 - Carregar Todos");
            Console.WriteLine("0  - Sair");
            Console.Write("\nEscolha uma opcao: ");

            int opcao;
            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opcao invalida! Digite um numero.");
                Console.Write("\nPressione ENTER para continuar...");
                Console.ReadLine();
                continue;
            }

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
                    Console.Write("\nDeseja salvar antes de sair? (S/N): ");
                    string resposta = Console.ReadLine();
                    if (resposta != null && resposta.Trim().ToUpper() == "S")
                    {
                        Cadastro.SalvarTodos();
                    }
                    continuar = false;
                    Console.WriteLine("\nAte logo!");
                    break;

                default:
                    Console.WriteLine("\nOpcao invalida!");
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
        try
        {
            Console.Clear();
        }
        catch { }

        Console.WriteLine("\n========================================");
        Console.WriteLine("         CONSULTAR DADOS                ");
        Console.WriteLine("========================================");
        Console.WriteLine("1 - Listar Disciplinas");
        Console.WriteLine("2 - Listar Alunos");
        Console.WriteLine("3 - Listar Matriculas");
        Console.Write("Escolha: ");

        int opcao;
        if (!int.TryParse(Console.ReadLine(), out opcao))
        {
            Console.WriteLine("Opcao invalida!");
            return;
        }

        if (opcao == 1)
        {
            Disciplina[] disciplinas = Cadastro.ObterDisciplinas();
            Console.WriteLine($"\n=== {disciplinas.Length} DISCIPLINAS CADASTRADAS ===");

            if (disciplinas.Length == 0)
            {
                Console.WriteLine("Nenhuma disciplina cadastrada.");
            }

            for (int i = 0; i < disciplinas.Length; i++)
            {
                Console.WriteLine($"\n{i + 1}. Codigo: {disciplinas[i].CodDisciplina}");
                Console.WriteLine($"   Nome: {disciplinas[i].NomeDisciplina}");
                Console.WriteLine($"   Nota Minima: {disciplinas[i].NotaMin}");
                Console.WriteLine("   " + new string('-', 35));
            }
        }
        else if (opcao == 2)
        {
            Aluno[] alunos = Cadastro.ObterAlunos();
            Console.WriteLine($"\n=== {alunos.Length} ALUNOS CADASTRADOS ===");

            if (alunos.Length == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado.");
            }

            for (int i = 0; i < alunos.Length; i++)
            {
                Console.WriteLine($"\n{i + 1}. Matricula: {alunos[i].Matricula}");
                Console.WriteLine($"   Nome: {alunos[i].Nome}");
                Console.WriteLine($"   Idade: {alunos[i].Idade} anos");
                Console.WriteLine("   " + new string('-', 35));
            }
        }
        else if (opcao == 3)
        {
            Matricula[] matriculas = Cadastro.ObterMatriculas();
            Console.WriteLine($"\n=== {matriculas.Length} MATRICULAS CADASTRADAS ===");

            if (matriculas.Length == 0)
            {
                Console.WriteLine("Nenhuma matricula cadastrada.");
            }

            for (int i = 0; i < matriculas.Length; i++)
            {
                Console.WriteLine($"\n{i + 1}. Matricula: {matriculas[i].MatriculaAluno}");
                Console.WriteLine($"   Nota 1: {matriculas[i].Nota1}");
                Console.WriteLine($"   Nota 2: {matriculas[i].Nota2}");
                Console.WriteLine($"   Media: {(matriculas[i].Nota1 + matriculas[i].Nota2) / 2.0:F1}");
                Console.WriteLine("   " + new string('-', 35));
            }
        }
        else
        {
            Console.WriteLine("Opcao invalida!");
        }
    }
}
