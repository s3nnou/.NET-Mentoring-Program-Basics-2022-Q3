CREATE TABLE [dbo].[Employee]
(
    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [AddressId] INT NOT NULL FOREIGN KEY REFERENCES dbo.Address(Id),
    [PersonId] INT NOT NULL FOREIGN KEY REFERENCES dbo.Person(Id),
    [CompanyName] NVARCHAR(20) NOT NULL,
    [Position] NVARCHAR(30) NULL,
    [EmployeeName] NVARCHAR(100) NULL,
)
