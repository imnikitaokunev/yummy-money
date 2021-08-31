create table [dbo].[Expense]
(
    [Id]             bigint              not null    identity, 
    [CategoryId]     uniqueidentifier    not null,
    [UserId]         uniqueidentifier    not null,
    [Amount]         money               not null, 
    [Date]           datetime2           not null, 
    [Description]    nvarchar(128)       null,

    constraint [PK_Expense] primary key (
        [Id]
    )
)
go

alter table [dbo].[Expense]
    add constraint [FK_Expense_Category] foreign key (
        [CategoryId]
    ) references [Category]([Id])
go

alter table [dbo].[Expense]
    add constraint [FK_Expense_User] foreign key (
        [UserId]
    ) references [User]([Id])
go
