using Stride.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Lib.Collections;
using static VL.Orpheus.NoteSymbols;

namespace VL.Orpheus
{
    public static class Scales
    {
        public static Spread<int> MAJOR => new int[] { 2, 2, 1, 2, 2, 2, 1 }.ToSpread();
        public static Spread<int> MINOR => new int[] { 2, 1, 2, 2, 1, 2, 2 }.ToSpread();

        public static Spread<int> IONIAN => new int[] { 2, 2, 1, 2, 2, 2, 1 }.ToSpread();

        public static Spread<int> DORIAN => new int[] { 2, 1, 2, 2, 2, 1, 2 }.ToSpread();

        public static Spread<int> PHRYGIAN => new int[] { 1, 2, 2, 2, 1, 2, 2 }.ToSpread();

        public static Spread<int> LYDIAN => new int[] { 2, 2, 2, 1, 2, 2, 1 }.ToSpread();

        public static Spread<int> MIXOLYDIAN => new int[] { 2, 2, 1, 2, 2, 1, 2 }.ToSpread();

        public static Spread<int> AEOLIAN => new int[] { 2, 1, 2, 2, 1, 2, 2 }.ToSpread();

        public static Spread<int> LOCRIAN => new int[] { 1, 2, 2, 1, 2, 2, 2 }.ToSpread();

        public static Spread<int> ACOUSTIC => new int[] { 2, 2, 2, 1, 2, 1, 2 }.ToSpread();


        public static string FLAT => '\u266D'.ToString();
        public static string SHARP => '\u266F'.ToString();

        public static string NATURAL => '\u266E'.ToString();

        public static Spread<string> MAJOR_ANNOT => new string[] { "C", "C\u266F", "D", "D\u266F", "E", "F", "F\u266F", "G", "G\u266F", "A", "A\u266F", "B" }.ToSpread();

        public static Spread<string> MINOR_ANNOT => new string[] { "C", "D\u266D", "D", "E\u266D", "E", "F", "G\u266D", "G", "A\u266D", "A", "B\u266D", "B" }.ToSpread();


        public static Spread<string> FLAT_ANNOT => new string[] { "C♭", "D♭", "E♭", "F♭", "G♭", "A♭", "B♭" }.ToSpread();

        //one of each note letter.
        public static Spread<int> CalcHeptatonicScale(IEnumerable<int>? IntervalsPattern, int BaseNote = 36, int Count = 1)
        {
            var iterations = Count * 7;
            List<int> Scale = new List<int>();
            
            //var baseNoteAnnot = SetBaseNote((EnumNotes)BaseNote, EnumAccidentals.NONE, 0);

            if (!IntervalsPattern.IsNullOrEmpty())
            {
                var intervals = IntervalsPattern.ToSpread();
                

                Scale.Add(BaseNote);
                var note = BaseNote;
                for (int i = 0; i < iterations - 1; i++)
                {
                    var interval = intervals[i%7];



                    note += interval;
                    Scale.Add(note);
                }
            }
           
            
            
            return Scale.ToSpread();
        }

        public static int SetBaseNote(EnumNotes Notes, EnumAccidentals Accidental, int Octave = 0)
        {

            int newBase = 12 * Octave + (int)Notes;
            var acc = 0;
            switch (Accidental)
            {
                case EnumAccidentals.SHARP:
                    newBase += 1;
                    break;

                case EnumAccidentals.FLAT:
                    newBase -= 1;
                    break;

                case EnumAccidentals.NATURAL:
                    newBase = newBase;
                    break;
                case EnumAccidentals.NONE:
                    newBase = newBase;
                    break;
            }
                

            return newBase;
        }

        
    }
}
