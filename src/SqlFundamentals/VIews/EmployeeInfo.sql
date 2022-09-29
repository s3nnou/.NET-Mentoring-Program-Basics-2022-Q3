CREATE VIEW [dbo].[EmployeeInfo] AS 
SELECT Employee.Id, 
    CASE Employee.EmployeeName WHEN NULL THEN CONCAT(Person.FirstName, ' ', Person.LastName) ELSE Employee.EmployeeName
    END AS EmployeeFullName,
    CONCAT(Address.ZipCode, '_', Address.State, ', ', Address.City, '-', Address.Street) AS EmployeeFullAddress,
    CONCAT(Employee.CompanyName, '(', Employee.Position, ')') AS EmployeeCompanyInfo
    FROM Employee JOIN Person on Employee.Id = Person.Id JOIN Address on Employee.AddressId = Address.Id

