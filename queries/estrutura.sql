-- #### ESTRUTURA
CREATE DATABASE SenacDB;
GO

USE SenacDB;
GO

CREATE LOGIN L_AULA_APP WITH PASSWORD=N'app123';
GO

CREATE USER L_AULA_APP FROM LOGIN L_AULA_APP;
GO
-- GRANT para DML
GRANT SELECT, INSERT, UPDATE, DELETE ON SenacDB.dbo.aluno TO L_AULA_APP;
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON SenacDB.dbo.NotaAluno TO L_AULA_APP;
GO

GRANT SELECT ON SenacDB.dbo.view_aluno_notas TO L_AULA_APP;
GO

-- DADOS DO ALUNO
CREATE TABLE aluno(
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	Nome NVARCHAR(250) NOT NULL,
	Matricula NVARCHAR(100) NOT NULL UNIQUE,
	DataNascimento DATE NOT NULL
);
GO

CREATE TABLE NotaAluno(
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	AlunoId UNIQUEIDENTIFIER NOT NULL,
	Nota FLOAT NOT NULL,
	FOREIGN KEY(AlunoId) REFERENCES aluno(Id)
);
GO

DROP VIEW view_aluno_notas;
CREATE VIEW view_aluno_notas AS 
SELECT a.Nome, a.Matricula, a.DataNascimento, na.Nota 
FROM aluno a
JOIN NotaAluno na ON a.Id = na.AlunoId;

select * from view_aluno_notas ORDER BY Nome;


INSERT INTO aluno(Nome, Matricula, DataNascimento) VALUES ('Viviane', '123456', '2010-09-28');

-- DADOS DO PROFESSOR
--public Guid Id { get; set; }
--public int Matricula { get; set; }
--public string Nome { get; set; } = string.Empty;
--public IEnumerable<string> Conhecimentos { get; set; } = default!;
DROP TABLE Professor_Conhecimento;
DROP TABLE Professor;
CREATE TABLE Professor(
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	Nome NVARCHAR(250) NOT NULL,
	Matricula INT NOT NULL UNIQUE
);

CREATE TABLE Professor_Conhecimento(
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	Conhecimento NVARCHAR(250) NOT NULL,
	Professor_Id UNIQUEIDENTIFIER NOT NULL,
	FOREIGN KEY(Professor_Id) REFERENCES Professor(Id)
);

CREATE UNIQUE INDEX idx_uniq_conhecimento_professor ON Professor_Conhecimento(Conhecimento, Professor_Id);

GRANT SELECT, INSERT, UPDATE, DELETE ON SenacDB.dbo.Professor TO L_AULA_APP;
GO
GRANT SELECT, INSERT, UPDATE, DELETE ON SenacDB.dbo.Professor_Conhecimento TO L_AULA_APP;
GO

INSERT INTO Professor(Nome, Matricula) OUTPUT Inserted.Id VALUES ('Claudio Rafael', 545612);

INSERT INTO Professor_Conhecimento(Conhecimento, Professor_Id) VALUES ('Java', 'F1BA6C05-BDA7-41DB-AC6C-9256B39052E8');
INSERT INTO Professor_Conhecimento(Conhecimento, Professor_Id) VALUES ('C#', '19E748A2-35AD-4CF0-A759-80D5056A6C6B');
INSERT INTO Professor_Conhecimento(Conhecimento, Professor_Id) VALUES ('Python', '19E748A2-35AD-4CF0-A759-80D5056A6C6B');
INSERT INTO Professor_Conhecimento(Conhecimento, Professor_Id) VALUES ('.net', '19E748A2-35AD-4CF0-A759-80D5056A6C6B');
INSERT INTO Professor_Conhecimento(Conhecimento, Professor_Id) VALUES ('.net Core', '19E748A2-35AD-4CF0-A759-80D5056A6C6B');
INSERT INTO Professor_Conhecimento(Conhecimento, Professor_Id) VALUES ('Kotlin', '19E748A2-35AD-4CF0-A759-80D5056A6C6B');
INSERT INTO Professor_Conhecimento(Conhecimento, Professor_Id) VALUES ('Angular', '19E748A2-35AD-4CF0-A759-80D5056A6C6B');
INSERT INTO Professor_Conhecimento(Conhecimento, Professor_Id) VALUES ('SQL', '19E748A2-35AD-4CF0-A759-80D5056A6C6B');


--UPSERT
DECLARE 
	@conhecimento NVARCHAR(250), 
	@professor_id UNIQUEIDENTIFIER

IF((select count(*) from Professor_Conhecimento where lower(Conhecimento) = lower(@conhecimento) AND Professor_Id = @professor_id) >= 1)
	BEGIN
		UPDATE Professor_Conhecimento
		SET
			Conhecimento = @conhecimento
		WHERE
			LOWER(Conhecimento) = LOWER(@conhecimento) 
		AND Professor_Id = @professor_id
	END
ELSE
	BEGIN
		INSERT INTO Professor_Conhecimento(Conhecimento, Professor_Id) 
		VALUES (@conhecimento, @professor_id);
	END;

INSERT INTO Professor_Conhecimento(Conhecimento, Professor_Id) 
VALUES ('Java', '19E748A2-35AD-4CF0-A759-80D5056A6C6B');

CREATE VIEW view_professor_conhecimento AS
select p.Id, p.Nome, p.Matricula, pc.Conhecimento from Professor AS p
join Professor_Conhecimento AS pc ON p.Id = pc.Professor_Id;

GRANT SELECT ON SenacDB.dbo.view_professor_conhecimento TO L_AULA_APP;
GO

-- levantamento de informações da estrutura da base
-- #case insensitive
select SERVERPROPERTY('COLLATION');
select collation_name from sys.databases where name = 'SenacDB';
execute sp_helpsort;

-- modificação do case sensitive/insensitive (SQL SERVER)
alter database SenacDB set single_user with rollback immediate;
go
alter database SenacDB collate SQL_Latin1_General_CP1_CS_AS;
go
alter database SenacDB set multi_user;
go

-- #### TESTES
select * from aluno;

select CURRENT_TIMESTAMP; -- Datetime
SELECT CONVERT (date, CURRENT_TIMESTAMP); -- DateOnly
SELECT CONVERT (time, CURRENT_TIMESTAMP); -- TimeOnly

select * from aluno;
select Nome, Matricula from aluno;

INSERT INTO NotaAluno(AlunoId, Nota) VALUES ('2b7c6035-23dc-4834-a66c-0798bd17453f', 10.0);
INSERT INTO NotaAluno(AlunoId, Nota) VALUES ('2b7c6035-23dc-4834-a66c-0798bd17453f', 6.7);
INSERT INTO NotaAluno(AlunoId, Nota) VALUES ('2b7c6035-23dc-4834-a66c-0798bd17453f', 7.2);

select * from NotaAluno where AlunoId = '2b7c6035-23dc-4834-a66c-0798bd17453f'



INSERT INTO NotaAluno(AlunoId, Nota) VALUES ("2B7C6035-23DC-4834-A66C-0798BD17453F", 10.0),
											("2B7C6035-23DC-4834-A66C-0798BD17453F", 9.0),
											("2B7C6035-23DC-4834-A66C-0798BD17453F", 8.9);


SELECT * FROM NotaAluno WHERE AlunoId = (SELECT Id FROM aluno WHERE Matricula = '123456')


MERGE INTO Professor AS target
USING (VALUES ('Luis', 98745)) AS source (Nome, Matricula)
ON (target.Matricula = source.Matricula)
WHEN MATCHED THEN
	UPDATE SET Nome = target.Nome
WHEN NOT MATCHED THEN
	INSERT (Nome, Matricula) VALUES (source.Nome, source.Matricula)
OUTPUT inserted.Id;


MERGE INTO Professor_Conhecimento AS target
USING (VALUES ('PHP', '0ACAC32F-A669-48CB-9C48-EA8C2F012088')) AS source (Conhecimento, Professor_Id)
ON ((target.Conhecimento = source.Conhecimento) AND (target.Professor_Id = source.Professor_Id))
WHEN MATCHED THEN
	UPDATE SET Conhecimento = target.Conhecimento
WHEN NOT MATCHED THEN
	INSERT (Conhecimento, Professor_Id) VALUES (source.Conhecimento, source.Professor_Id);