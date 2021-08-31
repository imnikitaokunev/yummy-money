create table [dbo].[Income]
(
    [Id]             bigint              not null    identity, 
    [CategoryId]     uniqueidentifier    not null,
    [UserId]         uniqueidentifier    not null,
    [Amount]         money               not null, 
    [Date]           datetime2           not null, 
    [Description]    nvarchar(128)       null,

    constraint [PK_Income] primary key (
        [Id]
    )
)
go

alter table [dbo].[Income]
    add constraint [FK_Income_Category] foreign key (
        [CategoryId]
    ) references [Category]([Id])
go

alter table [dbo].[Income]
    add constraint [FK_Income_User] foreign key (
        [UserId]
    ) references [User]([Id])
go
