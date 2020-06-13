create table [dbo].[Expense]
(
	[Id] bigint not null primary key identity, 
    [CategoryId] uniqueidentifier not null constraint [FK_Expense_Category] foreign key
        references [Category]([Id]),
    [UserId] uniqueidentifier not null constraint [FK_Expense_User] foreign key
        references [User]([Id]),
    [Amount] money not null, 
    [Date] datetime2 not null, 
    [Description] nvarchar(128) null, 
)
