using System.Runtime.Serialization;

namespace BinarySerialization.Task2
{
    [Serializable]
    public class Cat : ISerializable
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Cat()
        {
        }

        protected Cat(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("kittyName");
            Age = info.GetInt32("kittyAge");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("kittyName", Name);
            info.AddValue("kittyAge", Age);
        }
    }
}
