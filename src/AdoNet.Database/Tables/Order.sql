CREATE TABLE [dbo].[Order]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [Status] INT NOT NULL,
    [CreatedDate] Date NOT NULL,
    [UpdatedDate] Date NOT NULL,
    [ProductId] INT FOREIGN KEY REFERENCES dbo.Product(Id),
)
