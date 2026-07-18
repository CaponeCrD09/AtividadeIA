# Classes do Projeto

## Classe Aluno

Representa um aluno no sistema e encapsula as operações de banco de dados (padrão Active Record).

### Propriedades

| Propriedade | Tipo | Descrição |
|---|---|---|
| `id` | `int` | Identificador único do aluno. |
| `nome` | `string` | Nome completo do aluno. |
| `idade` | `int` | Idade do aluno. |
| `curso` | `string` | Curso em que o aluno está matriculado. |

### Construtores

* **`Aluno()`**: Construtor padrão (sem parâmetros).
* **`Aluno(int id, string nome, int idade, string curso)`**: Construtor parametrizado que inicializa todas as propriedades do aluno.

### Métodos

* **`void Cadastrar()`**: Insere o aluno atual no banco de dados MySQL e atualiza a propriedade `id` com o valor gerado.
* **`static Aluno? BuscarPorId(int id)`**: Busca um aluno pelo ID e retorna um objeto `Aluno` ou `null`.
* **`static List<Aluno> BuscarTodos()`**: Retorna uma lista contendo todos os alunos cadastrados no banco.
