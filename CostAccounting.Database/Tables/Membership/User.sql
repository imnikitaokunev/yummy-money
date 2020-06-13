create table [dbo].[User]
(
	[Id] uniqueidentifier not null constraint [PK_User] primary key,
	[Email] nvarchar(128) not null constraint [UK_User_Email] unique,
	[Username] nvarchar(128) not null constraint [UK_User_Username] unique,
	[PasswordHash] nvarchar(128) not null,
	[PasswordSalt] nvarchar(128) not null,
	[FirstName] nvarchar(128) not null,
	[LastName] nvarchar(128) not null,
	[RegisteredAt] datetime2 not null check([RegisteredAt] between '01/01/2020' and '12/31/2099'),
	[Photo] varbinary(max) null,

	constraint [CK_User_Username] check ([Username] not like '^[a-zA-Z0-9_]*$')
)
