create table [dbo].[User]
(
	[Id]              uniqueidentifier    not null,
	[Email]           nvarchar(128)       not null,
	[Username]        nvarchar(128)       not null,
	[PasswordHash]    nvarchar(128)       not null,
	[PasswordSalt]    nvarchar(128)       not null,
	[FirstName]       nvarchar(128)       not null,
	[LastName]        nvarchar(128)       not null,
	[RegisteredAt]    datetime2           not null,
	[Photo]           varbinary(max)      null,

	constraint [PK_User] primary key (
	    [Id]
    ),
)
go

alter table [dbo].[User]
    add constraint [UQ_User_Email] unique (
	    [Email]
	)
go

alter table [dbo].[User]
    add constraint [UQ_User_Username] unique (
	    [Username]
	)
go

alter table [dbo].[User]
    add constraint [CK_User_RegisteredAt] check (
	    [RegisteredAt] between '01/01/2020' and '12/31/2099'
	)
go
