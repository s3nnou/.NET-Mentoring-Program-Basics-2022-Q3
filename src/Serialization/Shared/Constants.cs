namespace Shared
{
    public static class Constants
    {
        public static Department PhpDepartment = new()
        {
            DepartmentName = "PHP Department",
            Employees = new()
            {
                new Employee
                {
                    EmpoyeeName = "Vinnie Robertson",
                },
                new Employee
                {
                    EmpoyeeName = "Alma Summers",
                },
                new Employee
                {
                    EmpoyeeName = "Deborah Simmons",
                },
                new Employee
                {
                    EmpoyeeName = "Eugene Nguyen",
                },
                new Employee
                {
                    EmpoyeeName = "Jayden Escobar",
                },
            }
        };
    }
}
