using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace telefon
{
    class hivas 
    {
        #region adatok
        /// <summary>
        /// Hívás kezdete (string)
        /// </summary>
        public string hKezdet { get; private set; }
        /// <summary>
        /// Hívás kezdete (mp:int)
        /// </summary>
        public int HKezdet { get; set; }
        public int varakozok { get;set; }
        /// <summary>
        /// Hívás vége (string)
        /// </summary>
        public string hVeg { get; private set; }
        /// <summary>
        /// Hívás kezdete (mp:int)
        /// </summary>
        public int HVeg { get; set; }
        public bool felvett { get;set; }
        public int idotartam { get;set; }
        #endregion

        #region konstruktor
        public hivas(string[] sor)
        {
            hKezdet = sor[0] + ":" + sor[1] + ":" + sor[2];
            hVeg = sor[3] + ":" + sor[4] + ":" + sor[5];
        }
        #endregion

    }

    #region órák
    /// <summary>
    /// órák és darabszámuk
    /// </summary>
    class ora
    {
        public int ido { get; set; }
        public int darab { get; set; }
        public ora(int ora,int db)
        {
            ido = ora;
            darab = db;
        }
    }
    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            #region 1.feladat
            Console.WriteLine("Első feladat");
            static int mpbe(int h, int m, int mp)
            {
                return h * 60 * 60 + m * 60 + mp;
            }
            Console.WriteLine("Kész\n");
            #endregion

            #region 2.feladat
            Console.WriteLine("Második feladat");
            StreamReader f = new StreamReader("hivas.txt");

            List<hivas> hivasok = new List<hivas>();

            int a = 0;
            while (!f.EndOfStream)
            {
                string[] darabok = f.ReadLine().Split(' ');
                hivasok.Add(new hivas(darabok));
                hivasok[a].HKezdet = mpbe(int.Parse(darabok[0]), int.Parse(darabok[1]), int.Parse(darabok[2]));
                hivasok[a].HVeg = mpbe(int.Parse(darabok[3]), int.Parse(darabok[4]), int.Parse(darabok[5]));
                hivasok[a].idotartam = hivasok[0].HVeg - hivasok[0].HKezdet;
                a++;
            }

            for (int i = 0; i < hivasok.Count; i++)
            {
                for (int k = 0; k < i; k++)
                {
                    if (hivasok[i].HKezdet < hivasok[k].HVeg)
                    {
                        hivasok[i].varakozok = hivasok[k].HVeg - hivasok[i].HVeg;
                    }
                    else
                    {
                        hivasok[i].varakozok = 0;
                    }

                    if (hivasok[i].HVeg < hivasok[k].HVeg)
                    {
                        hivasok[i].felvett = false;
                    }
                }
                
            }
            Console.WriteLine("Kész\n");
            #endregion

            #region 3.feladat
            Console.WriteLine("Harmadik feladat");

            //lista az óráknak
            List<int> ora = new List<int>();
            //órák + darabszám
            List<ora> orak = new List<ora>();

            //órák kigyűjtése
            for (int i = 0; i < hivasok.Count; i++)
            {
                if (ora.Contains(int.Parse(hivasok[i].hKezdet.Split(':')[0])) == false)
                {
                    ora.Add(int.Parse(hivasok[i].hKezdet.Split(':')[0]));
                }
            }

            //darabszám megszerzése
            for (int i = 0; i < ora.Count; i++)
            {
                int db = 0;
                for (int k = 0; k < hivasok.Count; k++)
                {
                    if (int.Parse(hivasok[k].hKezdet.Split(':')[0]) == ora[i])
                    {
                        db ++;
                    }
                }
                orak.Add(new ora(ora[i], db));
            }

            //eredmény kiírása
            for (int i = 0; i < orak.Count; i++)
            {
                Console.WriteLine($"{orak[i].ido} óra {orak[i].darab} hívás");
            }
            #endregion

            #region 4.feladat
            Console.WriteLine("\nNegyedik feladat");
            int leghosszabb = 0;
            int index = 0;

            for (int i = 0; i < hivasok.Count; i++)
            {
                int kulonbseg = hivasok[i].HVeg - hivasok[i].HKezdet;
                if (kulonbseg > leghosszabb)
                {
                    leghosszabb = kulonbseg;
                    index = i;
                }
            }

            Console.WriteLine($"A legohsszabb ideig vonalban lévő hívás {index}. sorban szerepel, a hívás hossza {leghosszabb+1} másodperc");
            #endregion

            Console.WriteLine("\nÖtödik feldat");

            Console.WriteLine("Adjon megy egy időpontot! (h,m,mp)");
            string[] bekert = Console.ReadLine().Split(' ');
            int bekertI = mpbe(int.Parse(bekert[0]), int.Parse(bekert[1]), int.Parse(bekert[2]));
            bool folyamatban = false;
            int varnak = 0;
            for (int i = 0; i < hivasok.Count; i++)
            {
                
                if (hivasok[i].HKezdet <= bekertI && hivasok[i].HVeg >= bekertI)
                {
                    index = i;
                    folyamatban = true;
                }
            }
            if (folyamatban == true)
            {
                for (int i = 0; i < hivasok.Count; i++)
                {
                    if (hivasok[i].HVeg > hivasok[index].HVeg && hivasok[i].HKezdet < hivasok[index].HVeg)
                    {
                        varnak++;
                    }
                }
                Console.WriteLine($"A várakozók száma: {varnak}, a beszélő a {index+1}");
            }
            else
            {
                Console.WriteLine("Nem volt beszélő");
            }
        }
    }
}
