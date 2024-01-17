using ConsoleApp1.Notes; // INoteAction ve Note türleri bu isim alanında bulunmalı
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class NoteAction : INoteAction
{
    private const string NotesFilePath = @"D:\C#\deneme\notices.txt";
    private bool showAllNotes = false;

    // Kullanıcıdan giriş alarak not ekleyen metod
    public void AddNoteFromUserInput()
    {
        Console.Write("Not yazınız: ");
        string content = Console.ReadLine();

        // Notun tarihini geçerli tarih ve saat olarak ayarla 
        string date = DateTime.Now.ToString();

        // Notu oluştur
        Note newNote = new Note
        {
            Content = content,
            Date = date
        };

        // Notu listeye ekle ve sadece yeni notu görüntüle
        AddNoteAndList(newNote);
    }

    // Parametre olarak alınan notu listeye ekleyen metod
    public void AddNote(Note note)
    {
        List<Note> notes = GetNoteList();
        notes.Add(note);

        // Tüm notları dosyaya yaz
        WriteNotesToFile(notes);
    }

    // Yeni notu ekleyip listeyi görüntüleyen metod
    private void AddNoteAndList(Note note)
    {
        List<Note> notes = GetNoteList();
        notes.Add(note);

        Console.WriteLine($"İçerik: {note.Content}, İşlem Tarihi: {note.Date}");

        // Tüm notları dosyaya yaz
        WriteNotesToFile(notes);

        // Eğer diğer notlar da varsa onları da listeleyelim
        if (notes.Count > 1)
        {
            Console.WriteLine("Diğer Notlar:");
            var lastNote = notes.Last();
            Console.WriteLine($"İçerik: {lastNote.Content}, İşlem Tarihi: {lastNote.Date}");
        }
    }

    // İstenen içerikle eşleşen bir notu silebilen metod
    public void DeleteNote(string content)
    {
        List<Note> notes = GetNoteList();
        Note noteToRemove = notes.Find(n => n.Content.Equals(content, StringComparison.OrdinalIgnoreCase));

        if (noteToRemove != null)
        {
            notes.Remove(noteToRemove);
            WriteNotesToFile(notes);
            Console.WriteLine($"Not '{content}' başarıyla silindi.");
        }
        else
        {
            Console.WriteLine($"Not '{content}' bulunamadı. Silme işlemi başarısız.");
        }
    }

    // Dosyadan notları okuyarak bir liste döndüren metod
    public List<Note> GetNoteList()
    {
        List<Note> notes = new List<Note>();
        if (File.Exists(NotesFilePath))
        {
            string[] lines = File.ReadAllLines(NotesFilePath);
            foreach (string line in lines)
            {
                // Satırın sonundaki boşluktan ayrıştırma yap
                int lastSpaceIndex = line.LastIndexOf(' ');

                // Geçerli bir boşluk konumu ve sonrasındaki kısım varsa devam et
                if (lastSpaceIndex > 0 && lastSpaceIndex < line.Length - 1)
                {
                    // İçeriği ve tarihi al
                    string content = line.Substring(0, lastSpaceIndex);
                    string date = line.Substring(lastSpaceIndex + 1);

                    // Yeni bir Not oluştur
                    Note note = new Note
                    {
                        Content = content,
                        Date = date
                    };

                    // Notu listeye ekle
                    notes.Add(note);
                }
                else
                {
                    Console.WriteLine($"Hatalı formatlı satır: {line}");
                }
            }
        }
        return notes;
    }

    // Notları dosyaya yazan metod
    private void WriteNotesToFile(List<Note> notes)
    {
        try
        {
            // Her notu dosyaya bir satır olarak yaz
            List<string> lines = notes.Select(n => $"{n.Content} {n.Date}").ToList();
            File.WriteAllLines(NotesFilePath, lines);
            Console.WriteLine("Notlar dosyaya başarıyla yazıldı.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata oluştu. Mesaj: {ex.Message}");
        }
    }

    // Tüm notları listeleyen metod
    public void ListNotes()
    {
        List<Note> notes = GetNoteList();

        Console.WriteLine("Tüm Notlar:");

        foreach (var note in notes)
        {
            Console.WriteLine($"İçerik: {note.Content}, İşlem Tarihi: {note.Date}");
        }

        if (notes.Count == 0)
        {
            Console.WriteLine("Henüz not bulunmamaktadır.");
        }
    }
}
