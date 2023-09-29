CREATE DATABASE SenacDB;
GO

USE SenacDB;
GO

CREATE LOGIN L_AULA_APP WITH PASSWORD=N'app123';
GO

CREATE USER L_AULA_APP FROM LOGIN L_AULA_APP;
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON SenacDB.dbo.aluno TO L_AULA_APP;
GO

CREATE TABLE aluno(
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	Nome NVARCHAR(250) NOT NULL,
	Matricula NVARCHAR(100) NOT NULL UNIQUE,
	DataNascimento DATE NOT NULL
);
GO

INSERT INTO aluno(Nome, Matricula, DataNascimento) VALUES ('Viviane', '123456', '2010-09-28');

/*
public Guid Id { get; set; }
public int Matricula { get; set; }
public string Nome { get; set; } = string.Empty;
public IEnumerable<string> Conhecimentos { get; set; } = default!;
*/
CREATE TABLE Professor(

);



select * from aluno