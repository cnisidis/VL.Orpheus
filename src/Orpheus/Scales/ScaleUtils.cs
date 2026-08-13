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
        public static Spread<float> MAJOR = new float[] { 2, 2, 1, 2, 2, 2, 1 }.ToSpread();
        public static Spread<float> MINOR = new float[] { 2, 1, 2, 2, 1, 2, 2 }.ToSpread();

        public static Spread<float> IONIAN = new float[] { 2, 2, 1, 2, 2, 2, 1 }.ToSpread();

        public static Spread<float> DORIAN = new float[] { 2, 1, 2, 2, 2, 1, 2 }.ToSpread();

        public static Spread<float> PHRYGIAN = new float[] { 1, 2, 2, 2, 1, 2, 2 }.ToSpread();

        public static Spread<float> LYDIAN = new float[] { 2, 2, 2, 1, 2, 2, 1 }.ToSpread();

        public static Spread<float> MIXOLYDIAN = new float[] { 2, 2, 1, 2, 2, 1, 2 }.ToSpread();

        public static Spread<float> AEOLIAN = new float[] { 2, 1, 2, 2, 1, 2, 2 }.ToSpread();

        public static Spread<float> LOCRIAN = new float[] { 1, 2, 2, 1, 2, 2, 2 }.ToSpread();

        public static Spread<float> ACOUSTIC = new float[] { 2, 2, 2, 1, 2, 1, 2 }.ToSpread();


        public static Spread<float> Hicaz = new float[] { 1.0f, 3.0f, 1.0f, 2.0f, 1.0f, 2.0f, 1.0f }.ToSpread();

        public static Spread<float> Ussak = new float[] { 1.5f, 1.5f, 2.0f, 2.0f, 1.0f, 2.0f, 2.0f }.ToSpread();

        public static Spread<float> Rast = new float[] { 2.0f, 1.5f, 1.5f, 2.0f, 2.0f, 1.5f, 1.5f }.ToSpread();

        public static Spread<float> Saba = new float[] { 1.5f, 1.5f, 2.0f, 1.0f, 1.0f, 2.0f, 2.0f }.ToSpread();

        public static Spread<float> Nihavend = new float[] { 2.0f, 1.0f, 2.0f, 2.0f, 1.0f, 3.0f, 1.0f }.ToSpread();

        public static Spread<float> Kürdi = new float[] { 1.0f, 2.0f, 2.0f, 2.0f, 1.0f, 2.0f, 2.0f }.ToSpread();

        /// <summary>
        /// The Second Mode (Echos B) - Soft Chromatic
        /// </summary>
        public static Spread<float> EchosB = new float[] { 1.16f, 1.84f, 1.0f, 2.0f, 1.16f, 1.84f, 1.0f }.ToSpread();

        /// <summary>
        /// The Fourth Mode (Echos D) - "Legetos"
        /// </summary>
        public static Spread<float> EchosD = new float[] { 1.66f, 1.34f, 1.0f, 2.0f, 1.66f, 1.34f, 1.0f }.ToSpread();
        /// <summary>
        /// The Plagal Fourth Mode (Echos Plagal D) - Hard Diatonic 
        /// </summary>
        public static Spread<float> EchosPlagalD = new float[] { 2.0f, 1.34f, 1.66f, 2.0f, 2.0f, 1.34f, 1.66f }.ToSpread();

        

        // 1. Bilaval (Western Major Scale)
        public static Spread<float> Bilaval = new float[] { 2.0f, 2.0f, 1.0f, 2.0f, 2.0f, 2.0f, 1.0f }.ToSpread();

        // 2. Kalyan (Lydian Mode - Sharp 4th)
        public static Spread<float> Kalyan = new float[] { 2.0f, 2.0f, 2.0f, 1.0f, 2.0f, 2.0f, 1.0f }.ToSpread();

        // 3. Khamaj (Mixolydian Mode - Flat 7th)
        public static Spread<float> Khamaj = new float[] { 2.0f, 2.0f, 1.0f, 2.0f, 2.0f, 1.0f, 2.0f }.ToSpread();

        // 4. Bhairav (Double Harmonic Major - Flat 2nd, Flat 6th)
        public static Spread<float> Bhairav = new float[] { 1.0f, 3.0f, 1.0f, 2.0f, 1.0f, 3.0f, 1.0f }.ToSpread();

        // 5. Bhairavi (Phrygian Mode / Kürdi - All flats)
        public static Spread<float> Bhairavi = new float[] { 1.0f, 2.0f, 2.0f, 2.0f, 1.0f, 2.0f, 2.0f }.ToSpread();

        // 6. Asavari (Aeolian Mode / Natural Minor)
        public static Spread<float> Asavari = new float[] { 2.0f, 1.0f, 2.0f, 2.0f, 1.0f, 2.0f, 2.0f }.ToSpread();

        // 7. Kafi (Dorian Mode)
        public static Spread<float> Kafi = new float[] { 2.0f, 1.0f, 2.0f, 2.0f, 2.0f, 1.0f, 2.0f }.ToSpread();

        // 8. Todi (Very unique: Flat 2, Flat 3, Sharp 4, Flat 6)
        public static Spread<float> Todi = new float[] { 1.0f, 2.0f, 3.0f, 1.0f, 1.0f, 3.0f, 1.0f }.ToSpread();

        // 9. Purvi (Flat 2, Sharp 4, Flat 6)
        public static Spread<float> Purvi = new float[] { 1.0f, 3.0f, 2.0f, 1.0f, 1.0f, 3.0f, 1.0f }.ToSpread();

        // 10. Marwa (Flat 2, Sharp 4, Natural 6)
        public static Spread<float> Marwa = new float[] { 1.0f, 3.0f, 2.0f, 1.0f, 2.0f, 2.0f, 1.0f }.ToSpread();

        // 1. Bhairav (Microtonal)
        // The Flat 2 (Re) is very low, and the Flat 6 (Dha) is also low.
        public static Spread<float> Bhairav_Shruti = new float[] { 0.9f, 2.96f, 1.12f, 2.04f, 0.9f, 2.96f, 1.12f }.ToSpread();

        // 2. Todi (The most microtonal Thaat)
        // Features a "heavy" Komal Re and a very sharp Tivra Ma.
        public static Spread<float> Todi_Shruti = new float[] { 0.9f, 1.91f, 3.01f, 1.18f, 0.9f, 2.96f, 1.12f }.ToSpread();

        // 3. Purvi (Microtonal)
        public static Spread<float> Purvi_Shruti = new float[] { 1.12f, 2.74f, 2.02f, 1.12f, 0.9f, 2.96f, 1.12f }.ToSpread();

    }
}
