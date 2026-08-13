using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Lib.Collections;
using VL.Orpheus.Notes;
using VL.Orpheus;
using Stride.Core.Extensions;
using static VL.Orpheus.Notes.NoteUtils;
using System.Xml.Linq;
using System.Diagnostics;


namespace VL.Orpheus.Scales
{

    
    public class Scale
    
    {

        public static readonly Spread<float> intervals = new float[] { 2, 2, 1, 2, 2, 2, 1 }.ToSpread();
        
        public static readonly Spread<float> NaturalHome = new float[] { 0, 200, 400, 500, 700, 900, 1100 }.ToSpread();


        public struct NoteAnnotation
        {
            public string Letter;
            public int Offset; // Cents difference from "Natural"
            public string FullName;
            public float intervalToNext;
            public float distanceFromRoot;
            public float absoluteCents;
        }

        public static Spread<NoteAnnotation> ResolveScaleAnnotations(Spread<float> actualCents, int rootIndex = 0, float microOffset = 0)
        {
            int[] naturalMajorPattern = { 0, 200, 400, 500, 700, 900, 1100 };
            string[] letters = { "C", "D", "E", "F", "G", "A", "B" };

            List<NoteAnnotation> annotations = new List<NoteAnnotation>();

            for (int i = 0; i < actualCents.Count; i++)
            {
                // 1. Resolve Letter
                int currentLetterIdx = (rootIndex + i) % 7;
                string letter = letters[currentLetterIdx];

                float rootBaseCents = naturalMajorPattern[rootIndex] + microOffset;

                // 2. Calculate "Natural" position for this letter relative to Root
                int home = (naturalMajorPattern[currentLetterIdx] - naturalMajorPattern[rootIndex] + 1200) % 1200;

                // 3. Calculate Offset (Actual position vs Natural position)
                float offset = (actualCents[i] + microOffset) - home;

                // Octave Wrap: ensures -1100 becomes +100 (Sharp) or 1100 becomes -100 (Flat)
                while (offset > 600) offset -= 1200;
                while (offset < -600) offset += 1200;

                int roundedOffset = (int)Math.Round(offset);

                // 4. Intervals
                float intervalToNext = 0;
                if (i < actualCents.Count - 1)
                    intervalToNext = (actualCents[i + 1] - actualCents[i]) / 100f;
                else
                    intervalToNext = (1200 - actualCents[i]) / 100f;

                // 5. Symbol Building
                string symbol = "";
                if (roundedOffset == 0) symbol = "";
                else if (roundedOffset == 100) symbol = "♯";
                else if (roundedOffset == -100) symbol = "♭";
                else if (roundedOffset == 50) symbol = "𝄪";
                else if (roundedOffset == -50) symbol = "𝄳";
                else
                {
                    string dir = roundedOffset > 0 ? "♯" : "♭";
                    symbol = $"({dir}{Math.Abs(roundedOffset)}c)";
                }

                float absoluteCents = rootBaseCents + actualCents[i];

                annotations.Add(new NoteAnnotation
                {
                    Letter = letter,
                    Offset = roundedOffset,
                    intervalToNext = intervalToNext,
                    FullName = letter + symbol,
                    // Absolute position for audio/oscillator use
                    distanceFromRoot = actualCents[i] + microOffset,
                    absoluteCents= absoluteCents
                });
            }

            return annotations.ToSpread();
        }

        public static Spread<float> IntervalsToCents(Spread<float> intervals)
        {
            List<float> cents = new List<float>();
            float currentTotal = 0;

            // Rule: The first note is ALWAYS 0 relative to itself
            cents.Add(currentTotal);

            // Stop at Count - 1 because the 7th interval leads to the octave (1200)
            for (int i = 0; i < intervals.Count - 1; i++)
            {
                currentTotal += intervals[i] * 100f;
                cents.Add(currentTotal);
            }

            return cents.ToSpread();
        }

        public static float CentsToFrequency(float totalCents, float refFrequency = 261.6256f)
        {
            // totalCents is the distance from your reference (e.g., C4)
            // 2.0 ^ (cents / 1200)
            return refFrequency * (float)Math.Pow(2.0, totalCents / 1200.0);
        }


        public static void CentsToMidiAndBend(float absoluteCents, out int baseMidiNote, out float bendFraction)
        {
            // 1. Get the float MIDI value (e.g., 61.5 for Uşşak D𝄳)
            float midiFloat = 60 + (absoluteCents / 100f);

            // 2. The Base Note is the floor (e.g., 61)
            baseMidiNote = (int)Math.Floor(midiFloat);

            // 3. The Bend Fraction is the remainder (e.g., 0.5)
            bendFraction = midiFloat - baseMidiNote;
        }
        public static Spread<float> GetNaturalIntervals(Spread<float>? ScaleIntervals, float RootOffset, out float Natural, out float Third, out float Root, int countTones = 7)
        {
            if (ScaleIntervals == null)
                ScaleIntervals = intervals;

            List<float> NaturalDistances = new List<float>();


            float distance = RootOffset;
            NaturalDistances.Add(distance);
            
            float diff = countTones - ScaleIntervals.Count;
            for (int i = 0; i < countTones - 1; i++)
            {
                var idx = i % (countTones - (int)diff);
                float intervalValue = ScaleIntervals[(int)idx] * 100;

                distance += intervalValue;
                NaturalDistances.Add(distance);
            }
            Root = NaturalDistances[0];
            Natural = NaturalDistances[1];
            Third = NaturalDistances[2];
            

            return NaturalDistances.ToSpread();
        }

        
        public static readonly Spread<string> basicTones = new string[] {"C", "D", "E", "F", "G", "A", "B" }.ToSpread();
        public static void ShiftLeft<T>(T[] arr, int shifts)
        {
            Array.Copy(arr, shifts, arr, 0, arr.Length - shifts);
            Array.Clear(arr, arr.Length - shifts, shifts);
        }

        public static void ShiftRight<T>(T[] arr, int shifts)
        {
            Array.Copy(arr, 0, arr, shifts, arr.Length - shifts);
            Array.Clear(arr, 0, shifts);
        }


        private static readonly string[] NoteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        public Note RootNote { get; }
        public string Type { get; }
        public List<Note> Notes { get; }

        public Scale(Note rootNote, Spread<int> Intervals)
        {
            RootNote = rootNote;
            //Type = type;
            Notes = GenerateScale(rootNote, Intervals);
        }

        private List<Note> GenerateScale(Note rootNote, IEnumerable<int> Intervals)
        {
            var intervals = Intervals.ToArray();
            var notes = new List<Note> { rootNote };
            var currentNoteValue = rootNote.Index;

            var rootAccent = rootNote.Accent;

            char[] Names = { 'C', 'D', 'E', 'F', 'G', 'A', 'B' };
            //Console.WriteLine(rootNote.ToString());
            for (int i =0; i < intervals.Length-1; i++)
            {
                // Transpose the current note by the interval
                currentNoteValue += intervals[i];
                
                // Convert the integer value to a Note object
                var nextNote = Orpheus.Notes.NoteUtils.NoteFromIndex(currentNoteValue);
                notes.Add(nextNote);
                //Console.WriteLine(nextNote.ToString());
            }

            //check if all letters are present for instance D#, F3, G3 must turn to D# E#3 F##3

            var BaseNameChar = rootNote.Name.ToString()[0];
            var BaseNameIndex = Names.IndexOf(BaseNameChar);
            

            for (int i=0; i<notes.Count; i++)
            {
                

                if (i>0 && i<7)
                {
                    var curr = notes[i];
                    var prev = notes[i - 1];
                    //var next = notes[i + 1];
                    //next letter must be BaseNameChar - if not force 

                    var currentNameIndex = Names.IndexOf(curr.Name.ToString()[0]);
                    var prevNameIndex = Names.IndexOf(prev.Name.ToString()[0]);
                    BaseNameIndex += 1;
                    var newName = Names[BaseNameIndex % 7];
                    
                    if (curr.Name.ToString()[0] == newName || curr.Name.ToString()[0] == prev.Name.ToString()[0] || Math.Abs(currentNameIndex - prevNameIndex) != 1)
                    {
                        
                        RootNote rnote;
                        Enum.TryParse<RootNote>(newName.ToString(), out rnote);
                        notes[i].Name = rnote;
                        
                        var diff = Math.Abs(curr.Index - prev.Index);
                        
                        if(rootAccent == Accents.SHARP)
                        {
                            if (prev.Accent == Accents.SHARP && curr.Accent==Accents.NATURAL && intervals[i] == 2)
                            {
                                curr.Accent = Accents.DOUBLE_SHARP;
                            }
                            else if (prev.Accent == Accents.DOUBLE_SHARP && curr.Accent == Accents.NATURAL && intervals[i] == 2)
                            {
                                curr.Accent = Accents.DOUBLE_SHARP;
                            }
                            else if (prev.Accent == Accents.SHARP && curr.Accent == Accents.SHARP && intervals[i] == 2)
                            {
                                curr.Accent = Accents.SHARP;
                            }

                        }
                            
                        else if(rootAccent == Accents.FLAT)
                        {
                            {
                                if (prev.Accent == Accents.NATURAL && intervals[i-1]>1)
                                {
                                    curr.Accent = Accents.NATURAL;
                                }
                                if (prev.Accent == Accents.NATURAL && intervals[i] == 2)
                                {
                                    curr.Accent = Accents.FLAT;
                                }
                                else if (curr.Accent == Accents.SHARP) curr.Accent = Accents.FLAT;

                            }
                        }
                            
                    }
                    

                }

            }


            return notes;
        }

        
        private Note GetNoteFromValue(int noteValue)
        {
            // Calculate the note name and octave from the semitone value
            var noteIndex = noteValue % 12;
            var octave = noteValue / 12;
            var noteName = NoteNames[noteIndex];

            return new Note(noteIndex);
        }

        
    }
}
