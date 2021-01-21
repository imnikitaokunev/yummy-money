insert into [dbo].[Income] ([CategoryId], [UserId], [Amount], [Date], [Description])
values
	('500d0491-1ba1-ea11-83ca-54e1ad243737','2401d127-bb14-4e59-8fe8-34322355fa37', 5, getdate(), N'Зарплата'),
	('510d0491-1ba1-ea11-83ca-54e1ad243737', '2401d127-bb14-4e59-8fe8-34322355fa37', 100, getdate(), N'Подачки у метро'),
	('520d0491-1ba1-ea11-83ca-54e1ad243737', '2401d128-bb14-4e59-8fe8-34322355fa37', 7, getdate(), N'Мама сккинула'),
	('520d0491-1ba1-ea11-83ca-54e1ad243737', '2401d128-bb14-4e59-8fe8-34322355fa37', 10, getdate(), N'Нашел в зимней куртке')
