using Shared;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinarySerialization.Task1
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
            var formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("department.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Constants.PhpDepartment);

                Console.WriteLine("Data is serialized");
            }
        }

        private static void Read()
        {
            var formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("department.dat", FileMode.OpenOrCreate))
            {
                var deserilizeDepartment = formatter.Deserialize(fs) as Department;

                if (deserilizeDepartment is null || deserilizeDepartment.DepartmentName is null)
                {
                    Console.WriteLine("Deserialized object is null. Please check department.dat file.");
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