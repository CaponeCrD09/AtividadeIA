using MySqlConnector;

namespace AtividadeIA;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("====================================================");
        Console.WriteLine("   Inicializando e verificando o banco de dados...  ");
        Console.WriteLine("====================================================");
        InicializarBancoDeDados();

        bool rodar = true;
        while (rodar)
        {
            Console.WriteLine("\n====================================================");
            Console.WriteLine("               PAINEL GERENCIADOR ESCOLAR           ");
            Console.WriteLine("====================================================");
            Console.WriteLine(" 1. Cadastrar Turma");
            Console.WriteLine(" 2. Cadastrar Aluno");
            Console.WriteLine(" 3. Buscar Turma por ID");
            Console.WriteLine(" 4. Buscar Aluno por ID");
            Console.WriteLine(" 5. Listar todas as Turmas");
            Console.WriteLine(" 6. Listar todos os Alunos");
            Console.WriteLine(" 0. Sair");
            Console.WriteLine("====================================================");
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (opcao)
                {
                    case "1":
                        MenuCadastrarTurma();
                        break;
                    case "2":
                        MenuCadastrarAluno();
                        break;
                    case "3":
                        MenuBuscarTurmaPorId();
                        break;
                    case "4":
                        MenuBuscarAlunoPorId();
                        break;
                    case "5":
                        MenuListarTurmas();
                        break;
                    case "6":
                        MenuListarAlunos();
                        break;
                    case "0":
                        rodar = false;
                        Console.WriteLine("Encerrando o sistema. Até mais!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }

            if (rodar)
            {
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    private static void MenuCadastrarTurma()
    {
        Console.WriteLine(">>> CADASTRAR NOVA TURMA <<<");
        Console.Write("Nome da Turma (ex: Turma A): ");
        string nome = Console.ReadLine() ?? "";
        Console.Write("Período (ex: Matutino, Vespertino, Noturno): ");
        string periodo = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(periodo))
        {
            Console.WriteLine("Erro: Nome e Período são obrigatórios!");
            return;
        }

        var novaTurma = new Turma(0, nome, periodo);
        novaTurma.Cadastrar();
        Console.WriteLine($"Turma cadastrada com sucesso! ID gerado: {novaTurma.id}");
    }

    private static void MenuCadastrarAluno()
    {
        Console.WriteLine(">>> CADASTRAR NOVO ALUNO <<<");
        Console.Write("Nome do Aluno: ");
        string nome = Console.ReadLine() ?? "";
        
        Console.Write("Idade do Aluno: ");
        if (!int.TryParse(Console.ReadLine(), out int idade) || idade <= 0)
        {
            Console.WriteLine("Erro: Idade inválida!");
            return;
        }

        Console.Write("Curso do Aluno: ");
        string curso = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(curso))
        {
            Console.WriteLine("Erro: Nome e Curso são obrigatórios!");
            return;
        }

        // Mostrar turmas disponíveis para vincular
        Console.WriteLine("\n--- Turmas Disponíveis ---");
        var turmas = Turma.BuscarTodas();
        if (turmas.Count == 0)
        {
            Console.WriteLine("Nenhuma turma cadastrada. O aluno será cadastrado sem turma.");
            var alunoSemTurma = new Aluno(0, nome, idade, curso, null);
            alunoSemTurma.Cadastrar();
            Console.WriteLine($"Aluno cadastrado com sucesso (Sem Turma)! ID gerado: {alunoSemTurma.id}");
            return;
        }

        foreach (var t in turmas)
        {
            Console.WriteLine($" ID: {t.id} | {t.nome} ({t.periodo})");
        }
        Console.Write("Selecione o ID da Turma (ou pressione Enter para deixar Sem Turma): ");
        string? inputTurmaId = Console.ReadLine();
        int? turmaId = null;

        if (!string.IsNullOrWhiteSpace(inputTurmaId))
        {
            if (int.TryParse(inputTurmaId, out int parsedId))
            {
                if (turmas.Any(t => t.id == parsedId))
                {
                    turmaId = parsedId;
                }
                else
                {
                    Console.WriteLine("Aviso: ID de turma não encontrado! Cadastrando sem turma...");
                }
            }
            else
            {
                Console.WriteLine("Aviso: ID inválido! Cadastrando sem turma...");
            }
        }

        var novoAluno = new Aluno(0, nome, idade, curso, turmaId);
        novoAluno.Cadastrar();
        Console.WriteLine($"Aluno cadastrado com sucesso! ID gerado: {novoAluno.id}");
    }

    private static void MenuBuscarTurmaPorId()
    {
        Console.WriteLine(">>> BUSCAR TURMA POR ID <<<");
        Console.Write("Digite o ID da Turma: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Erro: ID inválido!");
            return;
        }

        var turma = Turma.BuscarPorId(id);
        if (turma != null)
        {
            Console.WriteLine($"\n[Turma Encontrada]");
            Console.WriteLine($"ID: {turma.id}");
            Console.WriteLine($"Nome: {turma.nome}");
            Console.WriteLine($"Período: {turma.periodo}");
        }
        else
        {
            Console.WriteLine("Turma não encontrada!");
        }
    }

    private static void MenuBuscarAlunoPorId()
    {
        Console.WriteLine(">>> BUSCAR ALUNO POR ID <<<");
        Console.Write("Digite o ID do Aluno: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Erro: ID inválido!");
            return;
        }

        var aluno = Aluno.BuscarPorId(id);
        if (aluno != null)
        {
            Console.WriteLine($"\n[Aluno Encontrado]");
            Console.WriteLine($"ID: {aluno.id}");
            Console.WriteLine($"Nome: {aluno.nome}");
            Console.WriteLine($"Idade: {aluno.idade}");
            Console.WriteLine($"Curso: {aluno.curso}");

            if (aluno.turma_id.HasValue)
            {
                var turma = Turma.BuscarPorId(aluno.turma_id.Value);
                string nomeTurma = turma?.nome ?? "ID " + aluno.turma_id.Value;
                Console.WriteLine($"Turma: {nomeTurma} (ID: {aluno.turma_id})");
            }
            else
            {
                Console.WriteLine("Turma: Sem vínculo");
            }
        }
        else
        {
            Console.WriteLine("Aluno não encontrado!");
        }
    }

    private static void MenuListarTurmas()
    {
        Console.WriteLine(">>> TODAS AS TURMAS CADASTRADAS <<<");
        var turmas = Turma.BuscarTodas();
        if (turmas.Count == 0)
        {
            Console.WriteLine("Nenhuma turma cadastrada no sistema.");
            return;
        }

        Console.WriteLine(string.Format("| {0,-5} | {1,-20} | {2,-15} |", "ID", "Nome da Turma", "Período"));
        Console.WriteLine(new string('-', 50));
        foreach (var t in turmas)
        {
            Console.WriteLine(string.Format("| {0,-5} | {1,-20} | {2,-15} |", t.id, t.nome, t.periodo));
        }
    }

    private static void MenuListarAlunos()
    {
        Console.WriteLine(">>> TODOS OS ALUNOS CADASTRADOS <<<");
        var alunos = Aluno.BuscarTodos();
        if (alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado no sistema.");
            return;
        }

        Console.WriteLine(string.Format("| {0,-5} | {1,-20} | {2,-5} | {3,-25} | {4,-15} |", "ID", "Nome do Aluno", "Idade", "Curso", "Turma"));
        Console.WriteLine(new string('-', 80));

        // Para evitar múltiplas conexões lentas em consultas de turmas no foreach, podemos buscar todas e manter em cache local
        var turmas = Turma.BuscarTodas().ToDictionary(t => t.id, t => t.nome);

        foreach (var a in alunos)
        {
            string turmaNome = "Sem vínculo";
            if (a.turma_id.HasValue && turmas.TryGetValue(a.turma_id.Value, out var nome))
            {
                turmaNome = nome;
            }
            Console.WriteLine(string.Format("| {0,-5} | {1,-20} | {2,-5} | {3,-25} | {4,-15} |", a.id, a.nome, a.idade, a.curso, turmaNome));
        }
    }

    private static void InicializarBancoDeDados()
    {
        string connectionStringSemBanco = "Server=localhost; User ID=root;Password=;Port=3307";
        string connectionStringComBanco = "Server=localhost; User ID=root;Password=;Database=escola;Port=3307";

        try
        {
            using (var connection = new MySqlConnection(connectionStringSemBanco))
            {
                connection.Open();
                using var command = new MySqlCommand("CREATE DATABASE IF NOT EXISTS escola;", connection);
                command.ExecuteNonQuery();
            }

            using (var connection = new MySqlConnection(connectionStringComBanco))
            {
                connection.Open();

                string createTurmas = @"
                    CREATE TABLE IF NOT EXISTS turmas (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        nome VARCHAR(100) NOT NULL,
                        periodo VARCHAR(50) NOT NULL
                    );";
                using (var command = new MySqlCommand(createTurmas, connection))
                {
                    command.ExecuteNonQuery();
                }

                string createAlunos = @"
                    CREATE TABLE IF NOT EXISTS alunos (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        nome VARCHAR(100) NOT NULL,
                        idade INT NOT NULL,
                        curso VARCHAR(100) NOT NULL,
                        turma_id INT,
                        FOREIGN KEY (turma_id) REFERENCES turmas(id) ON DELETE SET NULL
                    );";
                using (var command = new MySqlCommand(createAlunos, connection))
                {
                    command.ExecuteNonQuery();
                }

                try
                {
                    using var command = new MySqlCommand("ALTER TABLE alunos ADD COLUMN idade INT NOT NULL DEFAULT 0;", connection);
                    command.ExecuteNonQuery();
                }
                catch { }

                try
                {
                    using var command = new MySqlCommand("ALTER TABLE alunos ADD COLUMN curso VARCHAR(100) NOT NULL DEFAULT '';", connection);
                    command.ExecuteNonQuery();
                }
                catch { }

                try
                {
                    using var command = new MySqlCommand("ALTER TABLE alunos ADD COLUMN turma_id INT NULL;", connection);
                    command.ExecuteNonQuery();
                }
                catch { }

                try
                {
                    using var command = new MySqlCommand("ALTER TABLE alunos ADD CONSTRAINT fk_alunos_turmas FOREIGN KEY (turma_id) REFERENCES turmas(id) ON DELETE SET NULL;", connection);
                    command.ExecuteNonQuery();
                }
                catch { }
            }
            Console.WriteLine("Banco de dados e tabelas verificados/criados com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Aviso de Inicialização: Não foi possível criar as tabelas automaticamente. Detalhes: {ex.Message}");
        }
    }
}