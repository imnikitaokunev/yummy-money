insert into [dbo].[Expense] ([Id], [CategoryId], [Amount], [Date], [Description])
values
	(1, '500d0491-1ba1-ea11-83ca-54e1ad243737', 5, GETDATE(), N'Шава'),
	(2, '510d0491-1ba1-ea11-83ca-54e1ad243737', 100, GETDATE(), N'Штоники'),
	(3, '520d0491-1ba1-ea11-83ca-54e1ad243737', 7, GETDATE(), N'Гель для душа'),
	(4, '520d0491-1ba1-ea11-83ca-54e1ad243737', 10, GETDATE(), N'Прочее')
