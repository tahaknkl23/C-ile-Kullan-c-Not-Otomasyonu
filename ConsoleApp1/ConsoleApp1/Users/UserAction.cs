// UserAction.cs
using ConsoleApp1.Notes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class UserAction : IUserAction
{
    private const string UsersFilePath = @"D:\C#\deneme\users.txt";

    public void AddUser(User user)
    {
        // Kullanıcı eklemek için gereken kod
        List<User> users = GetUserList();
        if (!users.Any(u => u.PhoneNumber == user.PhoneNumber))
        {
            // Parolayı da ekleyin
            user.Password = GetPasswordFromUser();

            users.Add(user);
            WriteUsersToFile(users);
            Console.WriteLine("Kullanıcı başarıyla eklendi.");
        }
        else
        {
            Console.WriteLine("Bu telefon numarası zaten kullanımda. Kullanıcı eklenemedi.");
        }
    }

    static bool IsValidUser(string email, string password)
    {
        List<User> users = new UserAction().GetUserList();
        return users.Any(user => user.Email == email && user.Password == password);
    }

    public List<User> GetUserList()
    {
        // Tüm kullanıcıları getirmek için gereken kod
        try
        {
            List<User> users = new List<User>();
            if (File.Exists(UsersFilePath))
            {
                string[] lines = File.ReadAllLines(UsersFilePath);
                foreach (string line in lines)
                {
                    string[] userInfo = line.Split(' ');
                    User user = new User
                    {
                        Name = userInfo[0],
                        Surname = userInfo[1],
                        Email = userInfo[2],
                        PhoneNumber = userInfo[3],
                        Password = userInfo[4],  // Parolayı ekle
                        IsAdmin = Convert.ToBoolean(userInfo[5])
                    };
                    users.Add(user);
                }
            }
            return users;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kullanıcı listesi alınırken bir hata oluştu: {ex.Message}");
            return new List<User>();
        }
    }

    public void DeleteUser(string phoneNumber)
    {
        List<User> users = GetUserList();
        User userToDelete = users.FirstOrDefault(u => String.Equals(u.PhoneNumber, phoneNumber, StringComparison.OrdinalIgnoreCase));

        if (userToDelete != null)
        {
            users.Remove(userToDelete);
            WriteUsersToFile(users);
            Console.WriteLine("Kullanıcı başarıyla silindi.");
        }
        else
        {
            Console.WriteLine("Belirtilen telefon numarasına sahip kullanıcı bulunamadı.");
        }
    }


    public void SearchUsers()
    {
        Console.Write("Aranacak kelimeyi giriniz: ");
        string filter = Console.ReadLine();

        List<User> filteredUsers = GetUserByFilter(filter);

        if (filteredUsers.Any())
        {
            Console.WriteLine("Arama sonuçları:");
            foreach (var user in filteredUsers)
            {
                Console.WriteLine($"Ad: {user.Name}, Soyad: {user.Surname}, Email: {user.Email}, Telefon: {user.PhoneNumber}, Admin: {user.IsAdmin}");
            }
        }
        else
        {
            Console.WriteLine("Aranan kriterlere uygun kullanıcı bulunamadı.");
        }
    }

    public List<User> GetUserByFilter(string filter)
    {
        // Kullanıcıları filtrelemek için gereken kod
        List<User> users = GetUserList();
        return users.Where(u => u.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                               u.Surname.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                               u.Email.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                               u.PhoneNumber.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    private void WriteUsersToFile(List<User> users)
    {
        // Kullanıcıları dosyaya yazmak için gereken kod
        try
        {
            List<string> lines = users.Select(u => $"{u.Name} {u.Surname} {u.Email} {u.PhoneNumber} {u.Password} {u.IsAdmin}").ToList();
            File.WriteAllLines(UsersFilePath, lines);
            Console.WriteLine("Kullanıcılar dosyaya başarıyla yazıldı.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kullanıcılar dosyaya yazılırken bir hata oluştu: {ex.Message}");
        }
    }

    private string GetPasswordFromUser()
    {
        // Kullanıcıdan parola alın
        Console.Write("Parola: ");
        return Console.ReadLine();
    }
}
