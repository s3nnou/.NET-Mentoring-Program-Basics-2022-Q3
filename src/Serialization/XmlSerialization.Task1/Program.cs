using Shared;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace XmlSerialization.Task1
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
            var xmlSerializer = new XmlSerializer(typeof(Department));

            using (FileStream fs = new FileStream("department.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, Constants.PhpDepartment);
                Console.WriteLine("Data is serialized");
            }
        }

        private static void Read()
        {
            var xmlSerializer = new XmlSerializer(typeof(Department));
            using (FileStream fs = new FileStream("department.xml", FileMode.OpenOrCreate))
            {
                var deserilizeDepartment = xmlSerializer.Deserialize(fs) as Department;
                foreach (var employee in deserilizeDepartment.Employees)
                {
                    Console.WriteLine($"Employee {employee.EmpoyeeName} is part of the {deserilizeDepartment.DepartmentName}");
                }
            }
        }
    }
}