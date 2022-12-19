using Shared;

namespace DeepCopy.Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var netDepartment = new Department()
            {
                DepartmentName = "NET Department",
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

            var deepClonedNetDepartment = netDepartment.DeepClone();
            var anotherNetDepartment = netDepartment;
            anotherNetDepartment.DepartmentName = "...";
            deepClonedNetDepartment.DepartmentName = "Department";

            Console.WriteLine($"{deepClonedNetDepartment.DepartmentName}");
            Console.WriteLine($"{anotherNetDepartment.DepartmentName}");
            Console.WriteLine($"{netDepartment.DepartmentName}");
        }
    }
}