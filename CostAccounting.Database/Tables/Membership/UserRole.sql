create table [dbo].[UserRole]
(
	[UserId] uniqueidentifier not null constraint [FK_UserRole_User] foreign key
    references [User](Id) on delete cascade,
    [RoleId] int not null constraint [FK_UserRole_Role] foreign key
    references [Role](Id) on delete cascade,
    [ModifiedDate] datetime2 not null default getdate(),

    constraint [PK_UserRole] primary key (
        [UserId],
        [RoleId]
    )
)
