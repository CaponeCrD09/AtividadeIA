# Estrutura do Banco de Dados

O projeto utiliza o banco de dados **MySQL** para persistir as informações dos alunos.

## Detalhes da Conexão

A conexão com o banco de dados é feita através da seguinte string de conexão utilizando o `MySqlConnector`:

```csharp
using var connection = new MySqlConnection("Server=localhost; User ID=root;Password=;Database=escola;Port=3307");
```

* **Servidor (Server):** `localhost`
* **Usuário (User ID):** `root`
* **Senha (Password):** *(Em branco)*
* **Banco de Dados (Database):** `escola`
* **Porta (Port):** `3307`

## Script SQL de Criação

Para executar a aplicação com sucesso, crie a base de dados `escola` e a tabela `alunos` utilizando o script abaixo:

```sql
-- Criar o banco de dados se não existir
CREATE DATABASE IF NOT EXISTS escola;

-- Selecionar o banco de dados escola
USE escola;

-- Criar a tabela alunos
CREATE TABLE IF NOT EXISTS alunos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    idade INT NOT NULL,
    curso VARCHAR(100) NOT NULL
);
```
