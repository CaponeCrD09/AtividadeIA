# Exemplos de Código

Abaixo estão descritos exemplos práticos de como interagir com o banco de dados diretamente através da classe `Aluno`.

## 1. Conexão

```csharp
using MySqlConnector;
using AtividadeIA;

string connectionString = "Server=localhost; User ID=root;Password=;Database=escola;Port=3307";

// Conexão aberta utilizando a variável conforme solicitado
using var connection = new MySqlConnection(connectionString);
connection.Open();
```

## 2. Cadastrar Aluno (Método de Instância)

O método `Cadastrar` insere o próprio objeto instanciado no banco de dados e preenche automaticamente a propriedade `id`.

```csharp
var novoAluno = new Aluno(0, "Ana Souza", 20, "Ciência da Computação");
novoAluno.Cadastrar();

Console.WriteLine($"Aluno cadastrado com ID: {novoAluno.id}");
```

## 3. Buscar Aluno por ID (Método Estático)

O método estático `BuscarPorId` retorna um objeto `Aluno` se encontrado, ou `null` caso contrário.

```csharp
int idBusca = 1;
Aluno? aluno = Aluno.BuscarPorId(idBusca);

if (aluno != null)
{
    Console.WriteLine($"Nome: {aluno.nome}, Curso: {aluno.curso}");
}
else
{
    Console.WriteLine("Aluno não encontrado.");
}
```

## 4. Buscar Todos os Alunos (Método Estático)

O método estático `BuscarTodos` retorna uma lista contendo todos os alunos cadastrados no banco de dados.

```csharp
List<Aluno> todos = Aluno.BuscarTodos();

foreach (var aluno in todos)
{
    Console.WriteLine($"[ID: {aluno.id}] {aluno.nome} - {aluno.curso}");
}
```
