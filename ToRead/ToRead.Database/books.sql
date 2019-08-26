CREATE TABLE [dbo].[books]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	[Annotation] NVARCHAR(500) NULL,
	[LocationId] int NULL,
	FOREIGN KEY (LocationId) REFERENCES locations (Id)
)
