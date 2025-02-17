CREATE TABLE [Table1] (
	[Column1] int NOT NULL,
	[Column2] nvarchar(100) NOT NULL,
	[Column3] datetime NOT NULL   
);

CREATE TABLE [Table2] (
	[Column1] nvarchar(100) NOT NULL,
	[Column2] int NOT NULL   
);

ALTER TABLE [Table1] ADD PRIMARY KEY ([Column1],[Column2]);

ALTER TABLE [Table2] ADD PRIMARY KEY ([Column1],[Column2]);

ALTER TABLE [Table1] ADD CONSTRAINT [FK] FOREIGN KEY ([Column2], [Column1]) REFERENCES [Table2]([Column1], [Column2]);

CREATE UNIQUE INDEX [UQ__Table1] ON [Table1] ([Column1] Asc);

CREATE UNIQUE INDEX [UQ_AllColumns] ON [Table1] ([Column2] Asc,[Column3] Asc,[Column1] Asc);