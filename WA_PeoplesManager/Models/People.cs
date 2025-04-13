namespace WA_PeoplesManager.Models;

public class People
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public List<Job> Jobs { get; set; }
    
    public People()
    {
    }

    public People(int id, string firstName, string lastName, DateTime birthday)
        : this(id, firstName, lastName, birthday, new List<Job>()) { }

    public People(int? id, string firstName, string lastName, DateTime birthday, List<Job> jobs)
    {
        Id = id ?? 0;
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
        Jobs = jobs;
    }
    
    public override string ToString()
    {
        return $"Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, Birthday: {Birthday.ToShortDateString()}, Jobs: [{string.Join(", ", Jobs)}]";
    }
}
