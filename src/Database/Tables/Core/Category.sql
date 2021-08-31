create table [dbo].[Category]
(
    [Id]             uniqueidentifier    not null,
    [Name]           nvarchar(32)        not null, 
    [Description]    nvarchar(128)       null,
    [UserId]         uniqueidentifier    not null,

    constraint [PK_Category] primary key (
        [Id]
    )
)
go

alter table dbo.[Category]
	add constraint [AK_Category_Name] unique
	(
		[Name]
	)
go

alter table [dbo].[Category]
    add constraint [FK_Category_User] foreign key (
        [UserId]
    ) references [User]([Id])
go
