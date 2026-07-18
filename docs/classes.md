# Classes do Projeto

## 1. Classe Aluno

Representa um aluno no sistema e encapsula as operações de banco de dados (padrão Active Record).

### Propriedades

| Propriedade | Tipo | Descrição |
|---|---|---|
| `id` | `int` | Identificador único do aluno. |
| `nome` | `string` | Nome completo do aluno. |
| `idade` | `int` | Idade do aluno. |
| `curso` | `string` | Curso em que o aluno está matriculado. |
| `turma_id` | `int?` | ID da turma a qual o aluno pertence (opcional/anulável). |

### Construtores

* **`Aluno()`**: Construtor padrão (sem parâmetros).
* **`Aluno(int id, string nome, int idade, string curso, int? turma_id = null)`**: Construtor parametrizado que inicializa todas as propriedades do aluno (incluindo o vínculo com a turma).

### Métodos

* **`void Cadastrar()`**: Insere o aluno atual no banco de dados MySQL e atualiza a propriedade `id`.
* **`static Aluno? BuscarPorId(int id)`**: Busca um aluno pelo ID e retorna um objeto `Aluno` ou `null`.
* **`static List<Aluno> BuscarTodos()`**: Retorna uma lista contendo todos os alunos cadastrados no banco.

---

## 2. Classe Turma

Representa uma turma/classe no sistema e gerencia sua persistência no MySQL.

### Propriedades

| Propriedade | Tipo | Descrição |
|---|---|---|
| `id` | `int` | Identificador único da turma. |
| `nome` | `string` | Nome identificador da turma (ex: "Turma A"). |
| `periodo` | `string` | Período de aulas da turma (ex: "Noturno", "Matutino"). |

### Construtores

* **`Turma()`**: Construtor padrão.
* **`Turma(int id, string nome, string periodo)`**: Construtor parametrizado para instanciar e inicializar todos os dados da turma.

### Métodos

* **`void Cadastrar()`**: Insere a turma atual na base de dados e define a propriedade `id` gerada pelo MySQL.
* **`static Turma? BuscarPorId(int id)`**: Busca e retorna uma turma pelo ID.
* **`static List<Turma> BuscarTodas()`**: Lista e retorna todas as turmas cadastradas na base.
