using WA_PeoplesManager.Models;

namespace WA_PeoplesManager.Services;

public class PeopleService
{
    private static List<People> PeopleList { get; set; }
    private static int _lastIdAvailable;

    static PeopleService()
    {
        PeopleList = new List<People>();
        _lastIdAvailable = 0;
    }

    public void CreatePeople(People people)
    {
        people.Id = _lastIdAvailable++;
        PeopleList.Add(people);
        _lastIdAvailable = Math.Max(_lastIdAvailable, people.Id + 1);
    }

    public List<People> GetAllPeople() => PeopleList;

    public People GetPeopleById(int id) => PeopleList.Find(p => p.Id == id)
                                            ?? throw new KeyNotFoundException();

    public bool UpdatePeople(int id, People updatedPeople)
    {
        var index = PeopleList.FindIndex(p => p.Id == id);
        if (index == -1) return false;

        PeopleList[index] = updatedPeople;
        return true;
    }

    public bool DeletePeople(int id)
    {
        var people = GetPeopleById(id);
        if (people == null) return false;

        PeopleList.Remove(people);
        return true;
    }

    public bool AddJobToPeople(int peopleId, Job job)
    {
        var people = GetPeopleById(peopleId);
        if (people == null) return false;

        people.Jobs.Add(job);
        return true;
    }

    public bool AddJobsToPeople(int peopleId, List<Job> jobs)
    {
        var people = GetPeopleById(peopleId);
        if (people == null) return false;

        people.Jobs.AddRange(jobs);
        return true;
    }

    public bool RemoveJobFromPeople(int peopleId, Job job)
    {
        var people = GetPeopleById(peopleId);
        if (people == null) return false;

        return people.Jobs.Remove(job);
    }

    public List<Job>? GetJobsByPeopleId(int peopleId)
    {
        var people = GetPeopleById(peopleId);
        return people?.Jobs;
    }

    public List<People> GetPeopleByCompanyName(string companyName)
    {
        return PeopleList.Where(p => p.Jobs.Any(j => j.CompanyName == companyName)).ToList();
    }

    public List<Job>? GetJobsByDateRange(int peopleId, DateTime startDate, DateTime endDate)
    {
        var people = GetPeopleById(peopleId);
        if (people == null) return null;

        return people.Jobs
            .Where(job => job.StartDate <= endDate && (job.EndDate == null || job.EndDate >= startDate))
            .ToList();
    }
}
