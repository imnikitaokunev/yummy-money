create table [dbo].[Category]
(
    [Id]             uniqueidentifier    not null,
    [Name]           nvarchar(32)        not null, 
    [Description]    nvarchar(128)       null,

    constraint [PK_Category] primary key (
        [Id]
    )
)
