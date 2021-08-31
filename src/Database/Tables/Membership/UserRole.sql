create table [dbo].[UserRole]
(
    [UserId]          uniqueidentifier    not null,
    [RoleId]          int                 not null,
    [ModifiedDate]    datetime2           not null,

    constraint [PK_UserRole] primary key (
        [UserId],
        [RoleId]
    )
)
go

alter table [dbo].[UserRole]
    add constraint [FK_UserRole_User] foreign key (
        [UserId]
    ) references [User]([Id]) on delete cascade
go

alter table [dbo].[UserRole]
    add constraint [FK_UserRole_Role] foreign key (
        [RoleId]
    ) references [Role]([Id]) on delete cascade
go

alter table [dbo].[UserRole]
    add constraint [DF_UserRole_ModifiedDate] default getdate() for [ModifiedDate]
go