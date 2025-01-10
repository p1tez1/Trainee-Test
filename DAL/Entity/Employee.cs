namespace DAL.Mode
{
    public class Employee
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Married { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
