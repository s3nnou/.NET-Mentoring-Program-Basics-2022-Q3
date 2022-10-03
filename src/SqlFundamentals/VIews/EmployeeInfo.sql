CREATE VIEW [dbo].[EmployeeInfo] AS 
    SELECT TOP 100 Employee.Id, 
    ISNULL(Employee.EmployeeName, CONCAT(Person.FirstName, ' ', Person.LastName)) AS EmployeeFullName,
    CONCAT(Address.ZipCode, '_', Address.State, ', ', Address.City, '-', Address.Street) AS EmployeeFullAddress,
    CONCAT(Employee.CompanyName, '(', Employee.Position, ')') AS EmployeeCompanyInfo
    FROM Employee JOIN Person on Employee.Id = Person.Id JOIN Address on Employee.AddressId = Address.Id
    ORDER BY Employee.CompanyName, Address.City;

