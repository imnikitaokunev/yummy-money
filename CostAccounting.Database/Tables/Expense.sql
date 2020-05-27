CREATE TABLE [dbo].[Expense]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [CategoryId] INT NOT NULL CONSTRAINT [FK_Expense_Category] FOREIGN KEY
        REFERENCES [Category]([Id]),
    [Amount] MONEY NOT NULL, 
    [Date] DATETIME2 NOT NULL, 
    [Description] NVARCHAR(128) NULL
)
