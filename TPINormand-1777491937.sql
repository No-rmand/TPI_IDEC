CREATE TABLE [Client] (
	[Id] int IDENTITY(1,1) NOT NULL UNIQUE,
	[Nom] nvarchar(max) NOT NULL,
	[EMail] nvarchar(max) NOT NULL,
	[NombreEmploye] int NOT NULL,
	[SecteurActiviteId] int NOT NULL,
	PRIMARY KEY ([Id])
);

CREATE TABLE [SecteurActivite] (
	[Id] int IDENTITY(1,1) NOT NULL UNIQUE,
	[Nom] nvarchar(max) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	PRIMARY KEY ([Id])
);

CREATE TABLE [Question] (
	[Id] int IDENTITY(1,1) NOT NULL UNIQUE,
	[Texte] nvarchar(max) NOT NULL,
	[Ordre] int NOT NULL,
	[QuestionId] int NOT NULL,
	[SecteurActiviteId] int,
	PRIMARY KEY ([Id])
);

CREATE TABLE [ApplicationOdoo] (
	[Id] int IDENTITY(1,1) NOT NULL UNIQUE,
	[Nom] nvarchar(max) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[EstEssentiel] bit NOT NULL,
	[EstAdministrative] bit NOT NULL,
	[SecteurActiviteId] int,
	PRIMARY KEY ([Id])
);

CREATE TABLE [Resultat] (
	[Id] int IDENTITY(1,1) NOT NULL UNIQUE,
	[DateGeneration] date NOT NULL,
	[Notes] nvarchar(max),
	[ClientId] int NOT NULL,
	PRIMARY KEY ([Id])
);

CREATE TABLE [QuestionApplication] (
	[Id] int IDENTITY(1,1) NOT NULL UNIQUE,
	[ApplicationOdooId] int NOT NULL,
	[QuestionId] int NOT NULL,
	PRIMARY KEY ([Id])
);

CREATE TABLE [CreationListe] (
	[Id] int IDENTITY(1,1) NOT NULL UNIQUE,
	[ResultatId] int NOT NULL,
	[ApplicationOdooId] int NOT NULL,
	PRIMARY KEY ([Id])
);

CREATE TABLE [Repondre] (
	[Id] int IDENTITY(1,1) NOT NULL UNIQUE,
	[Reponse] bit NOT NULL,
	[ClientId] int NOT NULL,
	[QuestionId] int NOT NULL,
	PRIMARY KEY ([Id])
);

ALTER TABLE [Client] ADD CONSTRAINT [Client_fk4] FOREIGN KEY ([SecteurActiviteId]) REFERENCES [SecteurActivite]([Id]);

ALTER TABLE [Question] ADD CONSTRAINT [Question_fk4] FOREIGN KEY ([SecteurActiviteId]) REFERENCES [SecteurActivite]([Id]);
ALTER TABLE [ApplicationOdoo] ADD CONSTRAINT [ApplicationOdoo_fk5] FOREIGN KEY ([SecteurActiviteId]) REFERENCES [SecteurActivite]([Id]);
ALTER TABLE [Resultat] ADD CONSTRAINT [Resultat_fk3] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id]);
ALTER TABLE [QuestionApplication] ADD CONSTRAINT [QuestionApplication_fk1] FOREIGN KEY ([ApplicationOdooId]) REFERENCES [ApplicationOdoo]([Id]);

ALTER TABLE [QuestionApplication] ADD CONSTRAINT [QuestionApplication_fk2] FOREIGN KEY ([QuestionId]) REFERENCES [Question]([Id]);
ALTER TABLE [CreationListe] ADD CONSTRAINT [CreationListe_fk1] FOREIGN KEY ([ResultatId]) REFERENCES [Resultat]([Id]);

ALTER TABLE [CreationListe] ADD CONSTRAINT [CreationListe_fk2] FOREIGN KEY ([ApplicationOdooId]) REFERENCES [ApplicationOdoo]([Id]);
ALTER TABLE [Repondre] ADD CONSTRAINT [Repondre_fk2] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id]);

ALTER TABLE [Repondre] ADD CONSTRAINT [Repondre_fk3] FOREIGN KEY ([QuestionId]) REFERENCES [Question]([Id]);