using ConsoleApp1.Notes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program
{
    static void Main()
    {
        bool exit = false;

        do
        {
            Console.WriteLine("1. Admin Giriş");
            Console.WriteLine("2. Kullanıcı Giriş");
            Console.WriteLine("3. Çıkış");

            int userType = Convert.ToInt32(Console.ReadLine());

            switch (userType)
            {
                case 1:
                    AdminLogin();
                    break;

                case 2:
                    UserLogin();
                    break;

                case 3:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Geçersiz seçim!");
                    break;
            }

        } while (!exit);
    }

    static void AdminLogin()
    {
        Console.Write("Mail giriniz: ");
        string email = Console.ReadLine();

        Console.Write("Parola giriniz: ");
        string password = Console.ReadLine();

        if (email == "a" && password == "1")
        {
            AdminMenu(new UserAction());
        }
        else
        {
            Console.WriteLine("Admin girişi başarısız. Geçersiz mail veya parola.");
        }
    }

    static void UserLogin()
    {
        Console.Write("Mail giriniz: ");
        string email = Console.ReadLine();

        Console.Write("Parola giriniz: ");
        string password = Console.ReadLine();

        if (IsValidUser(email, password))
        {
            UserMenu(new NoteAction());
        }
        else
        {
            Console.WriteLine("Kullanıcı girişi başarısız. Geçersiz mail veya parola.");
        }
    }

    static bool IsValidUser(string email, string password)
    {
        List<User> users = new UserAction().GetUserList();
        return users.Any(user => user.Email == email && user.Password == password);
    }

    static void AdminMenu(IUserAction userAction)
    {
        bool exitAdminMenu = false;

        do
        {
            Console.WriteLine("1. Kullanıcı Ekle");
            Console.WriteLine("2. Kullanıcı Ara");
            Console.WriteLine("3. Kullanıcı Sil");
            Console.WriteLine("4. Çıkış");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Kullanıcı Ekleme İşlemi:");
                    AddUser(userAction);
                    break;

                case 2:
                    Console.WriteLine("Kullanıcı Ara İşlemi:");
                    SearchUsers(userAction);
                    break;

                case 3:
                    Console.WriteLine("Kullanıcı Silme İşlemi:");
                    DeleteUser(userAction);
                    break;

                case 4:
                    exitAdminMenu = true;
                    break;

                default:
                    Console.WriteLine("Geçersiz seçim!");
                    break;
            }

        } while (!exitAdminMenu);
    }

    static void AddUser(IUserAction userAction)
    {
        Console.Write("İsim: ");
        string name = Console.ReadLine();

        Console.Write("Soyisim: ");
        string surname = Console.ReadLine();

        Console.Write("Telefon: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Parola: ");
        string password = Console.ReadLine();

        Console.Write("Admin mi? (true/false): ");
        bool isAdmin = Convert.ToBoolean(Console.ReadLine());

        User user = new User
        {
            Name = name,
            Surname = surname,
            PhoneNumber = phoneNumber,
            Email = email,
            Password = password,
            IsAdmin = isAdmin
        };

        userAction.AddUser(user);
    }

    static void DeleteUser(IUserAction userAction)
    {
        Console.Write("Silmek istediğiniz kullanıcının telefon numarasını giriniz: ");
        string phoneNumberToDelete = Console.ReadLine();

        userAction.DeleteUser(phoneNumberToDelete);
    }

    static void SearchUsers(IUserAction userAction)
    {
        Console.Write("Aranacak kelimeyi giriniz: ");
        string filter = Console.ReadLine();

        List<User> filteredUsers = userAction.GetUserByFilter(filter);

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

    static void UserMenu(INoteAction noteAction)
    {
        bool exitUserMenu = false;

        do
        {
            Console.WriteLine("1. Not Ekle");
            Console.WriteLine("2. Notları Listele");
            Console.WriteLine("3. Çıkış");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Not Ekleme İşlemi:");
                    noteAction.AddNoteFromUserInput();
                    break;

                case 2:
                    Console.WriteLine("Notları Listeleme İşlemi:");
                    noteAction.ListNotes();
                    break;

                case 3:
                    exitUserMenu = true;
                    break;

                default:
                    Console.WriteLine("Geçersiz seçim!");
                    break;
            }

        } while (!exitUserMenu);
    }
}
