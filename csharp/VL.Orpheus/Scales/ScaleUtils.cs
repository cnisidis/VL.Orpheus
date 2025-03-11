using Stride.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Lib.Collections;
using static VL.Orpheus.NoteSymbols;

namespace VL.Orpheus.Scales
{


    public static class ScaleUtils
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



    }
}
