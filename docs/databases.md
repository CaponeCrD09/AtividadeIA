# Estrutura do Banco de Dados

O projeto utiliza o banco de dados **MySQL** para persistir as informações dos alunos e de suas turmas.

## Detalhes da Conexão

A conexão com o banco de dados é feita através da seguinte string de conexão utilizando o `MySqlConnector`:

```csharp
using var connection = new MySqlConnection("Server=localhost; User ID=root;Password=;Database=escola;Port=3307");
```

## Script SQL de Criação e Atualização

Para executar a aplicação com sucesso, crie as tabelas conforme o script abaixo:

```sql
-- Criar o banco de dados se não existir
CREATE DATABASE IF NOT EXISTS escola;

-- Selecionar o banco de dados escola
USE escola;

-- Criar a tabela turmas
CREATE TABLE IF NOT EXISTS turmas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    periodo VARCHAR(50) NOT NULL
);

-- Criar a tabela alunos (com relacionamento)
CREATE TABLE IF NOT EXISTS alunos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    idade INT NOT NULL,
    curso VARCHAR(100) NOT NULL,
    turma_id INT,
    FOREIGN KEY (turma_id) REFERENCES turmas(id) ON DELETE SET NULL
);
```

### Script de Alteração (caso as tabelas já existam)

Se você já possuir a tabela `alunos` criada, execute os comandos a seguir para atualizá-la:

```sql
-- Criar a tabela turmas
CREATE TABLE IF NOT EXISTS turmas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    periodo VARCHAR(50) NOT NULL
);

-- Adicionar a coluna de relacionamento na tabela alunos
ALTER TABLE alunos ADD COLUMN IF NOT EXISTS turma_id INT;

-- Adicionar a restrição de chave estrangeira
ALTER TABLE alunos ADD CONSTRAINT fk_alunos_turmas 
    FOREIGN KEY (turma_id) REFERENCES turmas(id) ON DELETE SET NULL;
```
