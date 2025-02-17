CREATE TABLE [Table1] (
	[ColumnT11] int NOT NULL, 
	[ColumnT12] int NOT NULL, 
	[ColumnT13] int NULL   
);

CREATE TABLE [Table2] (
	[Column1] int NOT NULL  IDENTITY (1,1), 
	[Column2] int NULL   
);

CREATE TABLE [Table3] (
	[ColumnT31] int NOT NULL, 
	[ColumnT32] int NULL   
);

CREATE TABLE [Table4] (
	[Col1] nvarchar(100) NOT NULL, 
	[Col2] nvarchar(100) NOT NULL, 
	[Col3] int NOT NULL, 
	[Col4] nvarchar(100) NULL, 
	[Col5] nvarchar(100) NULL  
);

CREATE TABLE [Table5] (
	[Col1T5] nvarchar(100) NULL, 
	[Col2T5] nvarchar(100) NULL, 
	[Col3T5] int NULL   
);

CREATE TABLE [Table6] (
	[Column1_6] nvarchar(100) NOT NULL  
);

SET IDENTITY_INSERT [Table2] ON

SET IDENTITY_INSERT [Table2] OFF

ALTER TABLE [Table1] ADD PRIMARY KEY ([ColumnT11]);

ALTER TABLE [Table2] ADD PRIMARY KEY ([Column1]);

ALTER TABLE [Table3] ADD PRIMARY KEY ([ColumnT31]);

ALTER TABLE [Table4] ADD PRIMARY KEY ([Col1],[Col2],[Col3]);

ALTER TABLE [Table6] ADD PRIMARY KEY ([Column1_6]);

ALTER TABLE [Table1] ADD CONSTRAINT [FK_Table1] FOREIGN KEY ([ColumnT12]) REFERENCES [Table2]([Column1]);

ALTER TABLE [Table5] ADD CONSTRAINT [FK_Table4] FOREIGN KEY ([Col1T5], [Col2T5], [Col3T5]) REFERENCES [Table4]([Col1], [Col2], [Col3]);

CREATE UNIQUE INDEX [UQ__Table1] ON [Table1] ([ColumnT11] Asc);

CREATE UNIQUE INDEX [UQ_ColumnT13] ON [Table1] ([ColumnT13] Asc);

CREATE UNIQUE INDEX [UQ__Table2] ON [Table2] ([Column1] Asc);

CREATE UNIQUE INDEX [UQ__Table3] ON [Table3] ([ColumnT31] Asc);

CREATE UNIQUE INDEX [UQ_Col1T5] ON [Table5] ([Col1T5] Asc);