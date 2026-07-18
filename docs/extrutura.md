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
├── Aluno.cs                      # Modelo de dados e operações de persistência MySQL
├── AtividadeIA.csproj            # Configuração do projeto .NET 10
└── Program.cs                    # Ponto de entrada do sistema
```

## Descrição dos Componentes Principais

* **[Aluno.cs](file:///c:/Users/felipe.slsilva1/Desktop/conrado/AtividadeIA/AtividadeIA/Aluno.cs)**: Classe que une o modelo de domínio do Aluno (com as propriedades `id`, `nome`, `idade` e `curso`) e as operações de banco de dados (`Cadastrar`, `BuscarPorId` e `BuscarTodos`), seguindo o padrão de projeto *Active Record*.
* **[Program.cs](file:///c:/Users/felipe.slsilva1/Desktop/conrado/AtividadeIA/AtividadeIA/Program.cs)**: Ponto de entrada que executa o fluxo da aplicação, conectando ao banco e invocando os métodos diretamente sobre a classe/objeto `Aluno`.
