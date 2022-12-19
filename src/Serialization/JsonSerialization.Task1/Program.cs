using Shared;
using System;
using System.Runtime.Serialization.Formatters.Binary;
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
                foreach (var employee in deserilizeDepartment.Employees)
                {
                    Console.WriteLine($"Employee {employee.EmpoyeeName} is part of the {deserilizeDepartment.DepartmentName}");
                }
            }
        }
    }
}