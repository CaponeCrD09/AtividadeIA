using MySqlConnector;

namespace AtividadeIA;

public class Aluno
{
    private static readonly string _connectionString = "Server=localhost; User ID=root;Password=;Database=escola;Port=3307";

    public int id { get; set; }
    public string nome { get; set; } = string.Empty;
    public int idade { get; set; }
    public string curso { get; set; } = string.Empty;
    public int? turma_id { get; set; }

    // Construtor padrão
    public Aluno()
    {
    }

    // Construtor parametrizado (com suporte opcional a turma_id para manter compatibilidade)
    public Aluno(int id, string nome, int idade, string curso, int? turma_id = null)
    {
        this.id = id;
        this.nome = nome;
        this.idade = idade;
        this.curso = curso;
        this.turma_id = turma_id;
    }

    // Função para cadastrar alunos (Active Record)
    public void Cadastrar()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "INSERT INTO alunos (nome, idade, curso, turma_id) VALUES (@nome, @idade, @curso, @turma_id);";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@nome", this.nome);
        command.Parameters.AddWithValue("@idade", this.idade);
        command.Parameters.AddWithValue("@curso", this.curso);
        command.Parameters.AddWithValue("@turma_id", (object?)this.turma_id ?? DBNull.Value);
        command.ExecuteNonQuery();

        // Recuperar o ID gerado automaticamente e atribuir ao próprio objeto
        this.id = (int)command.LastInsertedId;
    }

    // Função para buscar aluno por id
    public static Aluno? BuscarPorId(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT id, nome, idade, curso, turma_id FROM alunos WHERE id = @id;";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Aluno(
                reader.GetInt32("id"),
                reader.GetString("nome"),
                reader.GetInt32("idade"),
                reader.GetString("curso"),
                reader.IsDBNull(reader.GetOrdinal("turma_id")) ? null : reader.GetInt32("turma_id")
            );
        }
        return null;
    }

    // Função para buscar todos os alunos
    public static List<Aluno> BuscarTodos()
    {
        var alunos = new List<Aluno>();
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT id, nome, idade, curso, turma_id FROM alunos;";
        using var command = new MySqlCommand(query, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            alunos.Add(new Aluno(
                reader.GetInt32("id"),
                reader.GetString("nome"),
                reader.GetInt32("idade"),
                reader.GetString("curso"),
                reader.IsDBNull(reader.GetOrdinal("turma_id")) ? null : reader.GetInt32("turma_id")
            ));
        }
        return alunos;
    }
}
