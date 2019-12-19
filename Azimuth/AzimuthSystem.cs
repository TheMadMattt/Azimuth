using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Azimuth
{
    public class AzimuthSystem
    {
        public float B { get; set; }
        public float Da { get; set; }
        public float Db { get; set; }
        public double teta { get; set; }
        public double tetaPrim { get; set; }
        public int teta1 { get; set; }
        public int teta2 { get; set; }
        public float Dm { get; set; }

        public double r1 { get; set; }
        public double r2 { get; set; }
        public double d1 { get; set; }
        public double d2 { get; set; }
        public double Ds { get; set; }
        public double Qs { get; set; }

        public AzimuthSystem()
        {
            //zainicjalizowanie początkowych wartości
            teta1 = 30;
            teta2 = 150;
            teta = teta1;
        }
    }
}
