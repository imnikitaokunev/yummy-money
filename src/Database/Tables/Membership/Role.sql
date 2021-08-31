create table [dbo].[Role]
(
    [Id]      int             not null    identity, 
    [Name]    nvarchar(32)    not null,

    constraint [PK_Role] primary key (
        [Id]
    )
)
go

alter table [dbo].[Role]
    add constraint [UQ_Role_Name] unique(
        [Name]
    )
go
