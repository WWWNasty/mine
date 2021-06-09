
namespace TG.Exam.OOP
{

    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public override string ToString()
        {
            return $"FirstName: {FirstName}\n LastName: {LastName}\n Salary:{Salary} ";
        }
    }

    public class SalesManager : Employee
    {
        public int BonusPerSale { get; set; }
        public int SalesThisMonth { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}\n BonusPerSale: {BonusPerSale}\n SalesThisMonth: {SalesThisMonth}";
        }
    }

    public class CustomerServiceAgent : Employee
    {
        public int Customers { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}\n Customers: {Customers}";
        }
    }

    public class Dog
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\n Age: {Age}";
        }
    }
}
