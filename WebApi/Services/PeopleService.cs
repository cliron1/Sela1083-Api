using WebApi.Models;

namespace WebApi.Services;

public interface IPeopleService {
    Person Add(Person model);
    void DeleteOne(int id);
    List<Person> GetAll();
    Person GetOne(int id);
    void Update(int id, Person model);
}

public class PeopleService : IPeopleService {
    private readonly List<Person> data;

    public PeopleService() {
        data = new List<Person> {
            new Person{Id=1, Name="Liron", Pwd="123456"}
        };
    }

    public List<Person> GetAll() {
        return data;
    }
    public Person GetOne(int id) {
        return data.FirstOrDefault(x => x.Id == id);
    }
    public Person Add(Person model) {
        model.Id = 1 + data.Max(x => x.Id);
        data.Add(model);
        return model;
    }
    public void Update(int id, Person model) {

    }
    public void DeleteOne(int id) {
    }
}
