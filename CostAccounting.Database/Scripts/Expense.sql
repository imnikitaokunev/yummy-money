﻿insert into [dbo].[Expense] ([CategoryId], [Amount], [Date], [Description])
values
	('500d0491-1ba1-ea11-83ca-54e1ad243737', 5, GETDATE(), N'Шава'),
	('510d0491-1ba1-ea11-83ca-54e1ad243737', 100, GETDATE(), N'Штоники'),
	('520d0491-1ba1-ea11-83ca-54e1ad243737', 7, GETDATE(), N'Гель для душа'),
	('520d0491-1ba1-ea11-83ca-54e1ad243737', 10, GETDATE(), N'Прочее')