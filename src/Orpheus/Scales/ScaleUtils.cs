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
        public static Spread<float> MAJOR => new float[] { 2, 2, 1, 2, 2, 2, 1 }.ToSpread();
        public static Spread<float> MINOR => new float[] { 2, 1, 2, 2, 1, 2, 2 }.ToSpread();

        public static Spread<float> IONIAN => new float[] { 2, 2, 1, 2, 2, 2, 1 }.ToSpread();

        public static Spread<float> DORIAN => new float[] { 2, 1, 2, 2, 2, 1, 2 }.ToSpread();

        public static Spread<float> PHRYGIAN => new float[] { 1, 2, 2, 2, 1, 2, 2 }.ToSpread();

        public static Spread<float> LYDIAN => new float[] { 2, 2, 2, 1, 2, 2, 1 }.ToSpread();

        public static Spread<float> MIXOLYDIAN => new float[] { 2, 2, 1, 2, 2, 1, 2 }.ToSpread();

        public static Spread<float> AEOLIAN => new float[] { 2, 1, 2, 2, 1, 2, 2 }.ToSpread();

        public static Spread<float> LOCRIAN => new float[] { 1, 2, 2, 1, 2, 2, 2 }.ToSpread();

        public static Spread<float> ACOUSTIC => new float[] { 2, 2, 2, 1, 2, 1, 2 }.ToSpread();


        public static Spread<float> Hicaz => new float[] { 1.0f, 3.0f, 1.0f, 2.0f, 1.0f, 2.0f, 1.0f }.ToSpread();

        public static Spread<float> Ussak => new float[] { 1.5f, 1.5f, 2.0f, 2.0f, 1.0f, 2.0f, 2.0f }.ToSpread();

        public static Spread<float> Rast => new float[] { 2.0f, 1.5f, 1.5f, 2.0f, 2.0f, 1.5f, 1.5f }.ToSpread();

        public static Spread<float> Saba => new float[] { 1.5f, 1.5f, 2.0f, 1.0f, 1.0f, 2.0f, 2.0f }.ToSpread();

        public static Spread<float> Nihavend => new float[] { 2.0f, 1.0f, 2.0f, 2.0f, 1.0f, 3.0f, 1.0f }.ToSpread();

        public static Spread<float> Kürdi => new float[] { 1.0f, 2.0f, 2.0f, 2.0f, 1.0f, 2.0f, 2.0f }.ToSpread();



    }
}
