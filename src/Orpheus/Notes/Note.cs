using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VL.Orpheus.Scales.ScaleUtils;
using static VL.Orpheus.Notes.NoteUtils;
using System.Runtime.CompilerServices;
using Stride.Core.Extensions;

namespace VL.Orpheus.Notes
{
    public class Note
    {
        public RootNote Name { get; set; } // e.g., "C", "D#", "Ab"
        public int Octave { get; } // e.g., 4 for middle C
        public double Frequency { get; } // Optional: Calculate frequency
        public Accents Accent { get; set; }

        public int Index { get; }

        
        public Note(RootNote Name, Accents Accent, int Octave)
        {
            this.Name = Name;
            this.Accent = Accent;
            this.Octave = Octave;

            this.Index = CalculateIndex();
            //Frequency = CalculateFrequency(name, octave);

        }


        public Note(int Index)
        {
           var note = NoteFromIndex(Index);

            this.Name = note.Name;
            this.Accent = note.Accent;
            this.Octave = note.Octave;

            this.Index = Index;
        }


        

        private int CalculateIndex()
        {

            var strAccent = FromAccent(Accent);

            var IndexNotesA = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            var IndexNotesB = new[] { "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab", "A", "Bb", "B" };
            var IndexNotesC = new[] { "C", "Db", "D", "Eb", "Fb", "E#", "Gb", "G", "Ab", "A", "Bb", "B" };


            var tempIndex = 0;
            var tmpNoteName = this.Name.ToString() + strAccent;
            
            if (IndexNotesA.Contains(tmpNoteName))
            {
                tempIndex = IndexNotesA.IndexOf(tmpNoteName);
            }
            else if (IndexNotesB.Contains(tmpNoteName))
            {
                tempIndex = IndexNotesB.IndexOf(tmpNoteName);
            }
            else if (IndexNotesC.Contains(tmpNoteName))
            {
                tempIndex = IndexNotesC.IndexOf(tmpNoteName);
            }


            if (this.Accent == Accents.NATURAL)
            {
                
            }

            return tempIndex + (12+Octave*12);
        }

        

        private double CalculateFrequency(string noteName, int octave)
        {
            // Formula: f(n) = 440 * 2^((n - 69)/12) for A4 = 440Hz
            var semitonesFromA4 = GetSemitonesFromA4(noteName, octave);
            return 440 * Math.Pow(2, semitonesFromA4 / 12.0);
        }

        private int GetSemitonesFromA4(string noteName, int octave)
        {
            // Map note names to semitones (C=0, C#=1, ..., B=11)
            var noteIndex = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" }
                .ToList()
                .IndexOf(noteName);
            var a4Index = 9 + 12 * 4; // A4 is the 57th semitone (C0 = 0)
            return noteIndex + 12 * octave - a4Index;
        }

        public override string ToString() => $"{Name}{ToSymbol(Accent)}{Octave}";
    }
}
