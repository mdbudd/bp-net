
namespace WebApi.Helpers
{
    public class DEmployee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; }
    }
    public class DDepartment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; }
    }
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }
    public class DProduct
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
    }
    public class Order
    {
        public int OrderId { get; set; }
        public string? ProductName { get; set; }
    }
}