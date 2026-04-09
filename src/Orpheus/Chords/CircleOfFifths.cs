
using VL.Lib.Collections;

namespace VL.Orpheus.Chords
{
    public class CircleOfFifths
    {
        private Notes.Note RootNote;
        private List<Notes.Note> Notes;
        public CircleOfFifths(Notes.Note RootNote)
        {
            if(RootNote == null)
            {
                this.RootNote = new Notes.Note(36);
            }
            else
            {
                this.RootNote = RootNote;
            }

            Notes = new List<Notes.Note>();
            
            var index = RootNote.Index;
            
            for (int i = 0; i<12; i++)
            {
                var _newNote = index;
                if(index - 36 > 12)
                {
                    index = index - 12;
                }

                Notes.Add(new Notes.Note(index));

                index += 5;
            }
        }

        public Spread<Notes.Note> GetNotes()
        {
            return Notes.ToSpread();
        }
    }
}
