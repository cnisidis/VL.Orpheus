using Stride.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VL.Orpheus.Notes
{
    public static class NoteUtils
    {
        public static string FLAT => '\u266D'.ToString();
        public static string SHARP => '\u266F'.ToString();

        public static string DOUBLE_SHARP => '\u266F'+'\u266F'.ToString();

        public static string DOUBLE_FLAT => '\u266D' + '\u266D'.ToString();

        public static string NATURAL => "";// '\u266E'.ToString();

        public static Note NoteFromIndex(int Index)
        {

            var IndexNotesA = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

            int octave = (int)Math.Ceiling((float)(Index / 12 - 1));
            int tmpIndex = Index % 12;
            
            var name = IndexNotesA[tmpIndex];
            //Console.WriteLine("{0:G} : {1:G}",tmpIndex, name);
            Accents accent = Accents.NATURAL;
            
            if(name.Contains("#"))
            {
                accent = Accents.SHARP;
                name = name[0].ToString();
            }
            else
            {
                accent = Accents.NATURAL;
            }

            RootNote rootNote;
            Enum.TryParse<RootNote>(name, out rootNote);
            return new Note(rootNote, accent, octave);
        }

        public enum Accents
        {
            NATURAL,
            SHARP,
            FLAT ,
            DOUBLE_FLAT,
            DOUBLE_SHARP
        }

        public enum RootNote
        {
            C, D, E, F, G, A, B

        }

        public static string FromAccent(Accents accent)
        {
            switch (accent)
            {
                case Accents.NATURAL:
                    return "";
                    break;

                case Accents.FLAT:
                    return "b";
                    break;

                case Accents.SHARP:
                    return "#";
                    break;

                


            }

            return NATURAL;
        }

        public static string ToSymbol(Accents accent)
        {
            switch(accent)
            {
                case Accents.NATURAL:
                    return NATURAL;
                    break;

                case Accents.FLAT:
                    return FLAT;
                    break;

                case Accents.SHARP:
                    return SHARP;
                    break;

                case Accents.DOUBLE_SHARP:
                    return DOUBLE_SHARP;
                    break;

                case Accents.DOUBLE_FLAT:

                    return DOUBLE_FLAT;
                    break;

            }

            return NATURAL;
        }
    }
}
