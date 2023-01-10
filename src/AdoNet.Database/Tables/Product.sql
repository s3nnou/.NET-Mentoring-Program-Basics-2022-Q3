CREATE TABLE [dbo].[Product]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(200) NOT NULL, 
    [Weight] INT NOT NULL, 
    [Height] INT NOT NULL, 
    [Width] INT NOT NULL, 
    [Length] INT NOT NULL,
)
