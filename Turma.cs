using MySqlConnector;

namespace AtividadeIA;

public class Turma
{
    private static readonly string _connectionString = "Server=localhost; User ID=root;Password=;Database=escola;Port=3307";

    public int id { get; set; }
    public string nome { get; set; } = string.Empty;
    public string periodo { get; set; } = string.Empty;

    // Construtor padrão
    public Turma()
    {
    }

    // Construtor parametrizado
    public Turma(int id, string nome, string periodo)
    {
        this.id = id;
        this.nome = nome;
        this.periodo = periodo;
    }

    // Função para cadastrar a turma (Active Record)
    public void Cadastrar()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "INSERT INTO turmas (nome, periodo) VALUES (@nome, @periodo);";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@nome", this.nome);
        command.Parameters.AddWithValue("@periodo", this.periodo);
        command.ExecuteNonQuery();

        // Recuperar o ID gerado automaticamente e atribuir ao próprio objeto
        this.id = (int)command.LastInsertedId;
    }

    // Função para buscar turma por id
    public static Turma? BuscarPorId(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT id, nome, periodo FROM turmas WHERE id = @id;";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Turma(
                reader.GetInt32("id"),
                reader.GetString("nome"),
                reader.GetString("periodo")
            );
        }
        return null;
    }

    // Função para buscar todas as turmas
    public static List<Turma> BuscarTodas()
    {
        var turmas = new List<Turma>();
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT id, nome, periodo FROM turmas;";
        using var command = new MySqlCommand(query, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            turmas.Add(new Turma(
                reader.GetInt32("id"),
                reader.GetString("nome"),
                reader.GetString("periodo")
            ));
        }
        return turmas;
    }
}
