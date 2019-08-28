CREATE TABLE [dbo].[authorsBooks]
(
	[AuthorId] int NOT NULL FOREIGN KEY REFERENCES authors (Id),
	[BookId] int NOT NULL FOREIGN KEY REFERENCES books (Id),
	PRIMARY KEY (AuthorId, BookId)
)
