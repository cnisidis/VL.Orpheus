
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using VL.Lib.Collections;
///For examples, see:
///https://thegraybook.vvvv.org/reference/extending/writing-nodes.html#examples
namespace VL.Orpheus;




public static class NoteSymbols
{
    public enum EnumAccidentals
    {
        NONE = 0,
        FLAT = 1,
        SHARP = 2,
        NATURAL = 3,


    }

    public enum EnumScales
    {
        Chromatic = 0,
        Major = 1,
        Minor = 2,
    }

    public enum EnumScaleType
    {
        natural,
        harmonic,
        melodic

    }

    public enum EnumNotes
    {

        C = 36,
        D = 38,
        E = 40,
        F = 41,
        G = 43,
        A = 45,
        B = 47

    }

    public enum Chromatic
    {
        C = 0,
        Ch,
        D,
        Dh,
        E,
        F,
        Fh,
        G,
        Gh,
        A,
        Ah,
        B
    }

    //https://www.cyberdefinitions.com/music-symbols.html
    public static Dictionary<EnumAccidentals, char> Accidentals = new Dictionary<EnumAccidentals, char>() {
        {EnumAccidentals.NONE, '\u0000' },
        {EnumAccidentals.FLAT, '\u266D' },
        {EnumAccidentals.SHARP, '\u266F' },
        {EnumAccidentals.NATURAL, '\u266E' },

    };

    public static String Format(byte hex)
    {
        return string.Format("{0}", hex);
    }

    public enum HeptatonicsEnum
    {
        Major =0,
        Minor,
        Ionian,
        Dorian,
        Phrygian,
        Lydian,
        Mixolydian,
        Aeolian,
        Locrian, 
        Acoustic
    }

    //whole tone => 1 or t
    //semi tone => 0.5 or s
    public static Dictionary<string, string[]> Scales = new Dictionary<string, string[] >(){
        
        {"Major", new string[]{ "t", "t", "s", "t", "t", "t", "s" } },
        {"Minor", new string[]{ "t", "s", "t", "t", "s", "t", "t" } },
        {"Ionian", new string[]{ "t", "t", "s", "t", "t", "t", "s" } },
        {"Dorian", new string[]{ "t", "s", "t", "t", "t", "s", "t" } },
        {"Phrygian",new string[]{ "s", "t", "t", "t", "s", "t", "t" } },
        {"Lydian", new string[]{ "t", "t", "t", "s", "t", "t", "s" } },
        {"Mixolydian", new string[]{ "t", "t", "s", "t", "t", "s", "t" } },
        
        {"Aeolian",new string[]{ "t", "s", "t", "t", "s", "t", "t" } },
        {"Locrian", new string[]{ "s", "t", "t", "s", "t", "t", "t" } },

        {"Acoustic", new string[]{ "t", "t", "t", "s", "t", "s", "t" } },
    };

    public static Spread<int> HeptatonicScales(int @base, HeptatonicsEnum pattern)
    {
        //string[] scalePattern = { "w","w", "h", "w", "w", "w", "h" };
        string[] scalePattern;
        Scales.TryGetValue(pattern.ToString(), out scalePattern);
        SpreadBuilder<int> scale = new SpreadBuilder<int>();

        var noteNumber = @base;

        var step = 0;
        for (int i=0; i<7; i++)
        {
            if (i > 0)
            {
                var stepType = scalePattern[(i-1) % 7];
                
                if (stepType == "t")
                    step = 2;
                else if (stepType == "s")
                    step = 1;
            }
            else
            {
                step = 0;   
            }
            noteNumber += step;
            scale.Add(noteNumber);
        }
        
        return scale.ToSpread();
    }
}