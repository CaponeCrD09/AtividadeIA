using MySqlConnector;

namespace AtividadeIA;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Iniciando a aplicação de alunos...");

        // Definição da string de conexão fornecida
        string connectionString = "Server=localhost; User ID=root;Password=;Database=escola;Port=3307";

        try
        {
            // Criando a conexão utilizando a variável conforme solicitado
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Conexão estabelecida com sucesso!");

            // 1. Cadastrar um aluno (Método de instância)
            Console.WriteLine("\n--- Cadastrando Novo Aluno ---");
            var novoAluno = new Aluno(0, "Ana Souza", 20, "Ciência da Computação");
            novoAluno.Cadastrar();
            Console.WriteLine($"Aluno cadastrado com sucesso! ID gerado no banco: {novoAluno.id}");

            // 2. Buscar aluno por ID (Método estático)
            Console.WriteLine($"\n--- Buscando Aluno por ID: {novoAluno.id} ---");
            var alunoEncontrado = Aluno.BuscarPorId(novoAluno.id);
            if (alunoEncontrado != null)
            {
                Console.WriteLine($"Aluno encontrado -> Nome: {alunoEncontrado.nome}, Idade: {alunoEncontrado.idade}, Curso: {alunoEncontrado.curso}");
            }
            else
            {
                Console.WriteLine($"Aluno com ID {novoAluno.id} não foi encontrado.");
            }

            // 3. Buscar todos os alunos (Método estático)
            Console.WriteLine("\n--- Buscando Todos os Alunos ---");
            var todosAlunos = Aluno.BuscarTodos();
            Console.WriteLine($"Total de alunos no banco: {todosAlunos.Count}");
            foreach (var aluno in todosAlunos)
            {
                Console.WriteLine($"[ID: {aluno.id}] Nome: {aluno.nome} | Idade: {aluno.idade} | Curso: {aluno.curso}");
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"\nErro no banco de dados MySQL: {ex.Message}");
            Console.WriteLine("Certifique-se de que o servidor MySQL está ativo na porta 3307 e que a base de dados 'escola' e a tabela 'alunos' existem.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nOcorreu um erro inesperado: {ex.Message}");
        }
    }
}