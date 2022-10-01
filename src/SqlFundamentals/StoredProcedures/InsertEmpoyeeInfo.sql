CREATE PROCEDURE [dbo].[InsertEmpoyeeInfo]
    @EmployeeName nvarchar(100) = NULL,
    @FirstName nvarchar(50) = NULL,
    @LastName nvarchar(50) = NULL,
    @CompanyName nvarchar(50),
    @Position nvarchar(50) = NULL,
    @Street nvarchar(50),
    @City nvarchar(20) = NULL,
    @State nvarchar(50) = NULL,
    @ZipCode nvarchar(50) = NULL
AS
    BEGIN
        IF (NULLIF(TRIM(@FirstName), '') IS NULL
        OR NULLIF(TRIM(@LastName), '') IS NULL
        OR NULLIF(TRIM(@EmployeeName), '') IS NULL )
            BEGIN
                raiserror('Error while inserting FirstName, LastName, EmployeeName', 18, 1);
                return -1;
            END
        ELSE
        BEGIN
            BEGIN TRAN EMPLOYEE_ADD
                DECLARE @a INT, @b INT;
                INSERT INTO [dbo].[Person]
                VALUES (@FirstName, @LastName)
                SET @a = @@IDENTITY;

                INSERT INTO [dbo].[Address]
                VALUES (@Street, @City, @State, @ZipCode)
                SET @b = @@IDENTITY;

                INSERT INTO dbo.Employee
                VALUES (@b, @a, LEFT(@CompanyName, 20), @Position, @EmployeeName);
            COMMIT TRAN EMPLOYEE_ADD
        END
    END