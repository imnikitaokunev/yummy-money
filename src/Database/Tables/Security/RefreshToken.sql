create table [dbo].[RefreshToken]
(
	[Id]               uniqueidentifier    not null,
	[JwtId]            nvarchar(128)       not null,
	[CreatedAt]        datetime2           not null,
	[ExpiryDate]       datetime2           not null,
	[IsUsed]           bit                 not null,
	[IsInvalidated]    bit                 not null,
	[UserId]           uniqueidentifier    not null,

	constraint [PK_RefreshToken] primary key (
	    [Id]
    )
)
go

alter table [dbo].[RefreshToken]
    add constraint [FK_RefreshToken_User] foreign key (
	    [UserId]
	) references [dbo].[User]([Id])
go
