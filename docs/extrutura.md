# Estrutura do Projeto

Abaixo está descrita a organização de diretórios e arquivos do projeto `AtividadeIA`.

## Estrutura de Diretórios

```text
AtividadeIA/
├── bin/                          # Binários compilados
├── obj/                          # Arquivos intermediários do compilador
├── docs/                         # Documentação do projeto
│   ├── classes.md                # Documentação das classes de negócio
│   ├── codigos.md                # Exemplos de códigos de uso
│   ├── contexto.md               # Contextualização do projeto (vazio)
│   ├── databases.md              # Documentação e DDL do banco de dados
│   └── extrutura.md              # Este arquivo (descrição da estrutura)
├── Aluno.cs                      # Modelo de dados e persistência MySQL do Aluno
├── Turma.cs                      # Modelo de dados e persistência MySQL da Turma
├── AtividadeIA.csproj            # Configuração do projeto .NET 10
└── Program.cs                    # Ponto de entrada do sistema
```

## Descrição dos Componentes Principais

* **[Aluno.cs](file:///c:/Users/felipe.slsilva1/Desktop/conrado/AtividadeIA/AtividadeIA/Aluno.cs)**: Classe que define o modelo do Aluno (com as propriedades `id`, `nome`, `idade`, `curso` e `turma_id`) e seus métodos Active Record (`Cadastrar`, `BuscarPorId` e `BuscarTodos`).
* **[Turma.cs](file:///c:/Users/felipe.slsilva1/Desktop/conrado/AtividadeIA/AtividadeIA/Turma.cs)**: Classe que define a entidade Turma (com propriedades `id`, `nome` e `periodo`) e seus respectivos métodos Active Record de persistência (`Cadastrar`, `BuscarPorId` e `BuscarTodas`).
* **[Program.cs](file:///c:/Users/felipe.slsilva1/Desktop/conrado/AtividadeIA/AtividadeIA/Program.cs)**: Ponto de entrada da aplicação que executa o fluxo demonstrativo do sistema, incluindo inserções, relacionamentos e buscas.
