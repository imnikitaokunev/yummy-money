create table [dbo].[Category]
(
	[Id] uniqueidentifier constraint [PK_Category] primary key, 
    [Name] nvarchar(32) NOT NULL, 
    [Description] nvarchar(128) NULL
)
