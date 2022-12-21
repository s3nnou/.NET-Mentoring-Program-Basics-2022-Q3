using Shared;
using System.Text.Json;

namespace JsonSerialization.Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Write();
            Read();
        }

        private static void Write()
        {
            using (FileStream fs = new FileStream("department.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, Constants.PhpDepartment);
                Console.WriteLine("Data is serialized");
            }
        }

        private static void Read()
        {
            using (FileStream fs = new FileStream("department.json", FileMode.OpenOrCreate))
            {
                var deserilizeDepartment = JsonSerializer.Deserialize<Department>(fs);

                if (deserilizeDepartment is null || deserilizeDepartment.DepartmentName is null)
                {
                    Console.WriteLine("Deserialized object is null. Please check department.json file.");
                    return;
                }

                foreach (var employee in deserilizeDepartment.Employees)
                {
                    Console.WriteLine($"Employee {employee.EmpoyeeName} is part of the {deserilizeDepartment.DepartmentName}");
                }
            }
        }
    }
}