using System.Text.Json;
using Projeto_WEBAPI_CalebeBertoluci.Models;

namespace Projeto_WEBAPI_CalebeBertoluci.Context;

public class DataGenerator
{
    private readonly InMemoryContext _inMemoryContext;
    private readonly List<string> _listNames;
    private readonly List<string> _listRoles;

    public DataGenerator(InMemoryContext inMemoryContext)
    {
        _inMemoryContext = inMemoryContext;
        _listNames = new List<string>() {"Calebe", "Ana", "Arthur", "Helena", "Levi"};
        _listRoles = new List<string>() {"Senior", "Pleno", "Junior" };
    }
    public void Generate()
    {
        if (!_inMemoryContext.Movies.Any())
        {
            List<Movies> items;
            using (StreamReader r = new StreamReader("MoviesJSON.json"))
            {
                string json = r.ReadToEnd();
                items = JsonSerializer.Deserialize<List<Movies>>(json);
            }
            _inMemoryContext.Movies.AddRange(items);
            _inMemoryContext.SaveChanges();
        }

        if (!_inMemoryContext.Users.Any())
        {
            List<Users> items = new List<Users>();
            var random = new Random();
            
            for(int i = 0; i < 10; i++)
            {
                var name = $"{_listNames.ElementAt(random.Next(_listNames.Count))} {_listNames.ElementAt(random.Next(_listNames.Count))}";
                var username = name.Replace(" ", "");
                Users user = new Users();
                user.Name = name;
                user.Password = $"{username}{i}";
                user.Username = username;
                user.Role = $"{_listRoles.ElementAt(random.Next(_listRoles.Count))}";
                items.Add(user);
            }
            _inMemoryContext.Users.AddRange(items);
            _inMemoryContext.SaveChanges();
        }
    }
}