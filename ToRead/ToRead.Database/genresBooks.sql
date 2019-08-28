CREATE TABLE [dbo].[genresBooks]
(
	[BookId] INT NOT NULL FOREIGN KEY REFERENCES books (Id), 
    [GenreId] INT NOT NULL FOREIGN KEY REFERENCES genres (Id),
	PRIMARY KEY (BookId, GenreId)
)
