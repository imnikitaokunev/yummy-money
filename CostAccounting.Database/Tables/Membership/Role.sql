create table [dbo].[Role]
(
	[Id] int identity not null constraint [PK_Role] primary key, 
    [Name] nvarchar(32) not null constraint [UK_Role_Name] unique
)
