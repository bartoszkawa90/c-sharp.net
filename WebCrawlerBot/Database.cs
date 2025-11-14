using LiteDB;

public class User
{
    public int Id { get; set; }  // automatyczny Primary Key
    public string Name { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        using (var db = new LiteDatabase("Filename=mydata.db;"))
        {
            // Pobranie kolekcji (jak tabela w SQL)
            var users = db.GetCollection<User>("users");

            // --- CREATE (INSERT) ---
            var user = new User { Name = "Jan", Age = 30 };
            users.Insert(user);

            // --- READ (SELECT) ---
            var allUsers = users.FindAll();
            foreach (var u in allUsers)
                Console.WriteLine($"{u.Id}: {u.Name}, {u.Age}");

            // --- UPDATE ---
            user.Age = 31;
            users.Update(user);

            // --- DELETE ---
            users.Delete(user.Id);
        }
    }
}