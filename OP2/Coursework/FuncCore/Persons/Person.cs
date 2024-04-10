using System.ComponentModel.DataAnnotations;

namespace FuncCore.Persons;

public class Person
{
    [Key] public long id;
    public string FirstName { get; set; }
    public string SecondName { get; set; }
}
