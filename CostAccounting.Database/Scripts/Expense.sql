INSERT INTO [dbo].[Expense] ([Id], [CategoryId], [Amount], [Date], [Description])
VALUES
	(1, 1, 5, GETDATE(), N'Шава'),
	(2, 2, 100, GETDATE(), N'Штоники'),
	(3, 3, 7, GETDATE(), N'Гель для душа'),
	(4, 3, 10, GETDATE(), N'Прочее')
