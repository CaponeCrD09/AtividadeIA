using MySqlConnector;

namespace AtividadeIA;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Inicializando e verificando o banco de dados...");
        InicializarBancoDeDados();

        Console.WriteLine("\nIniciando a aplicação de escola (Alunos e Turmas)...");

        // Definição da string de conexão fornecida
        string connectionString = "Server=localhost; User ID=root;Password=;Database=escola;Port=3307";

        try
        {
            // Criando a conexão utilizando a variável conforme solicitado
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Conexão com o banco de dados estabelecida com sucesso!");

            // 1. Cadastrar uma Turma
            Console.WriteLine("\n--- Cadastrando Nova Turma ---");
            var novaTurma = new Turma(0, "Turma A", "Noturno");
            novaTurma.Cadastrar();
            Console.WriteLine($"Turma cadastrada com sucesso! ID gerado no banco: {novaTurma.id}");

            // 2. Cadastrar um Aluno associado a essa Turma
            Console.WriteLine("\n--- Cadastrando Novo Aluno com Turma ---");
            var novoAluno = new Aluno(0, "Ana Souza", 20, "Ciência da Computação", novaTurma.id);
            novoAluno.Cadastrar();
            Console.WriteLine($"Aluno cadastrado com sucesso! ID gerado no banco: {novoAluno.id} (Vinculado à Turma ID: {novoAluno.turma_id})");

            // 3. Buscar turma por ID
            Console.WriteLine($"\n--- Buscando Turma por ID: {novaTurma.id} ---");
            var turmaEncontrada = Turma.BuscarPorId(novaTurma.id);
            if (turmaEncontrada != null)
            {
                Console.WriteLine($"Turma encontrada -> Nome: {turmaEncontrada.nome}, Período: {turmaEncontrada.periodo}");
            }
            else
            {
                Console.WriteLine("Turma não encontrada.");
            }

            // 4. Buscar aluno por ID
            Console.WriteLine($"\n--- Buscando Aluno por ID: {novoAluno.id} ---");
            var alunoEncontrado = Aluno.BuscarPorId(novoAluno.id);
            if (alunoEncontrado != null)
            {
                Console.WriteLine($"Aluno encontrado -> Nome: {alunoEncontrado.nome}, Curso: {alunoEncontrado.curso}, Turma ID: {alunoEncontrado.turma_id}");
            }
            else
            {
                Console.WriteLine("Aluno não encontrado.");
            }

            // 5. Buscar todas as turmas
            Console.WriteLine("\n--- Buscando Todas as Turmas ---");
            var todasTurmas = Turma.BuscarTodas();
            Console.WriteLine($"Total de turmas no banco: {todasTurmas.Count}");
            foreach (var t in todasTurmas)
            {
                Console.WriteLine($"[ID: {t.id}] Nome: {t.nome} | Período: {t.periodo}");
            }

            // 6. Buscar todos os alunos
            Console.WriteLine("\n--- Buscando Todos os Alunos ---");
            var todosAlunos = Aluno.BuscarTodos();
            Console.WriteLine($"Total de alunos no banco: {todosAlunos.Count}");
            foreach (var a in todosAlunos)
            {
                Console.WriteLine($"[ID: {a.id}] Nome: {a.nome} | Curso: {a.curso} | Turma ID: {a.turma_id}");
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"\nErro no banco de dados MySQL: {ex.Message}");
            Console.WriteLine("Certifique-se de que o servidor MySQL está ativo na porta 3307.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nOcorreu um erro inesperado: {ex.Message}");
        }
    }

    private static void InicializarBancoDeDados()
    {
        string connectionStringSemBanco = "Server=localhost; User ID=root;Password=;Port=3307";
        string connectionStringComBanco = "Server=localhost; User ID=root;Password=;Database=escola;Port=3307";

        try
        {
            // 1. Criar o banco escola caso não exista
            using (var connection = new MySqlConnection(connectionStringSemBanco))
            {
                connection.Open();
                using var command = new MySqlCommand("CREATE DATABASE IF NOT EXISTS escola;", connection);
                command.ExecuteNonQuery();
            }

            // 2. Criar tabelas e colunas
            using (var connection = new MySqlConnection(connectionStringComBanco))
            {
                connection.Open();

                // Criar tabela turmas
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

                // Criar tabela alunos caso não exista
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

                // Tentar alterar a tabela alunos caso ela já existisse sem as novas colunas
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