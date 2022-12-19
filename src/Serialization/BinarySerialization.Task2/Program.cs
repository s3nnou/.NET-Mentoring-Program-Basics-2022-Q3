
using System.Runtime.Serialization.Formatters.Binary;

namespace BinarySerialization.Task2
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
            var cat = new Cat { Name = "Barsik", Age = 5 };

            using (FileStream fs = new FileStream("cat.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, cat);

                Console.WriteLine("Data is serialized");
            }
        }

        private static void Read()
        {
            var formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("cat.dat", FileMode.OpenOrCreate))
            {
                var deserilizedCat = formatter.Deserialize(fs) as Cat;
                Console.WriteLine($"Cat named {deserilizedCat.Name} with age {deserilizedCat.Age} says meow");
            }
        }
    }
}