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


namespace VL.Orpheus.Scales
{
    public class Scale
    {
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

            for (int i =0; i < intervals.Length-1; i++)
            {
                // Transpose the current note by the interval
                currentNoteValue += intervals[i];
                
                // Convert the integer value to a Note object
                var nextNote = Orpheus.Notes.NoteUtils.NoteFromIndex(currentNoteValue);
                notes.Add(nextNote);
                Console.WriteLine(nextNote.ToString());
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
                            if (prev.Accent == Accents.SHARP && diff == 2)
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
