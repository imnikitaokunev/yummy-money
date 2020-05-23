INSERT INTO [dbo].[Expense] ([CategoryId], [Amount], [Date], [Description])
VALUES
	(1, 5, GETDATE(), N'Шава'),
	(2, 100, GETDATE(), N'Штоники'),
	(3, 7, GETDATE(), N'Гель для душа'),
	(3, 10, GETDATE(), N'Прочее')
