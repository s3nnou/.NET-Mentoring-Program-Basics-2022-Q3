using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Shared
{
    [Serializable]
    public class Department
    {
        [JsonPropertyOrder(2)]
        [JsonPropertyName("Department")]
        [XmlElement(ElementName = "Department")]
        public string DepartmentName { get; set; }

        [JsonPropertyOrder(1)]
        [JsonPropertyName("DepartmentEmployees")]
        [XmlArray("DepartmentEmployees")]
        public List<Employee> Employees { get; set; }
    }
}