# Exemplos de Código

Abaixo estão descritos exemplos práticos de como interagir com o banco de dados diretamente através das classes `Aluno` e `Turma`.

## 1. Conexão

```csharp
using MySqlConnector;
using AtividadeIA;

string connectionString = "Server=localhost; User ID=root;Password=;Database=escola;Port=3307";

// Utilizando a conexão para validar e abrir a comunicação
using var connection = new MySqlConnection(connectionString);
connection.Open();
```

## 2. Fluxo Completo: Cadastrar Turma e Vincular Aluno

```csharp
// 1. Criar e cadastrar uma nova turma
var turma = new Turma(0, "Turma A", "Noturno");
turma.Cadastrar();
Console.WriteLine($"Turma cadastrada com ID: {turma.id}");

// 2. Criar e cadastrar o aluno vinculado a essa turma
var aluno = new Aluno(0, "Ana Souza", 20, "Ciência da Computação", turma.id);
aluno.Cadastrar();
Console.WriteLine($"Aluno cadastrado com ID: {aluno.id} vinculado à turma {aluno.turma_id}");
```

## 3. Buscar Turma por ID (Método Estático)

```csharp
int idBusca = 1;
Turma? turma = Turma.BuscarPorId(idBusca);

if (turma != null)
{
    Console.WriteLine($"Turma: {turma.nome}, Período: {turma.periodo}");
}
else
{
    Console.WriteLine("Turma não encontrada.");
}
```

## 4. Buscar Todas as Turmas (Método Estático)

```csharp
List<Turma> todasAsTurmas = Turma.BuscarTodas();

foreach (var t in todasAsTurmas)
{
    Console.WriteLine($"[ID: {t.id}] {t.nome} - {t.periodo}");
}
```

## 5. Buscar Aluno por ID e Listar Todos os Alunos

```csharp
// Buscar por ID
Aluno? alunoBuscado = Aluno.BuscarPorId(1);
if (alunoBuscado != null)
{
    Console.WriteLine($"Aluno: {alunoBuscado.nome}, Turma ID: {alunoBuscado.turma_id}");
}

// Listar todos
List<Aluno> todosOsAlunos = Aluno.BuscarTodos();
foreach (var a in todosOsAlunos)
{
    Console.WriteLine($"[ID: {a.id}] {a.nome} (Turma ID: {a.turma_id})");
}
```
