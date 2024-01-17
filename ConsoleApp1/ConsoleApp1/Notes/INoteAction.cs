using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Notes
{
    public interface INoteAction
    {
        void AddNote(Note note);
        List<Note> GetNoteList();
        void ListNotes();
        void AddNoteFromUserInput();
    }
}
