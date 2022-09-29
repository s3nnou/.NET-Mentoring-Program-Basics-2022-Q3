CREATE TRIGGER [EmpoyeeInsert]
    ON Employee
    AFTER INSERT
    AS
    BEGIN
        SET NOCOUNT ON;
        DECLARE @AddressId INT;
        DECLARE @CompanyName NVARCHAR(20);

        SELECT @AddressId = INSERTED.AddressId, @CompanyName = INSERTED.CompanyName      
        FROM INSERTED;

        BEGIN TRAN COMPANY_ADD
            INSERT INTO dbo.Company
            VALUES (@CompanyName, @AddressId)
        COMMIT TRAN COMPANY_ADD
    END