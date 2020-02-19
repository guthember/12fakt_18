using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lift
{
  struct Lift
  {
    public int o;
    public int p;
    public int mp;
    public int cs;
    public int ksz;
    public int csz;
  }

  class Program
  {
    static Lift[] liftek;
    static int szintSz;
    static int csapatSz;
    static int igenySz;
    static int kezdes;
    static int melyikCs; //  amti 7. és 8. feladatban figyelünk
    static Lift[] bukovari; // az adott csapat mozgása
    static int mennyi; // mennyi bejegyzés az adott csapatra

    static void Egy()
    {
      Console.WriteLine("1. feladat: beolvasás");

      StreamReader file = new StreamReader("igeny.txt");

      szintSz = Convert.ToInt32(file.ReadLine());
      csapatSz = Convert.ToInt32(file.ReadLine());
      igenySz = Convert.ToInt32(file.ReadLine());

      liftek = new Lift[igenySz];

      for (int i = 0; i < igenySz; i++)
      {
        string[] adatok = file.ReadLine().Split(' ');
        liftek[i].o = Convert.ToInt32(adatok[0]);
        liftek[i].p = Convert.ToInt32(adatok[1]);
        liftek[i].mp = Convert.ToInt32(adatok[2]);
        liftek[i].cs = Convert.ToInt32(adatok[3]);
        liftek[i].ksz = Convert.ToInt32(adatok[4]);
        liftek[i].csz = Convert.ToInt32(adatok[5]);
      }

      file.Close();

    }

    static void Masodik()
    {
      Console.WriteLine("\n2. feladat");
      Console.Write("Kérem a kezdő szintet: ");
      kezdes = Convert.ToInt32(Console.ReadLine());
    }

    static void Harmadik()
    {
      Console.WriteLine("\n3. feladat");
      Console.WriteLine("A lift a {0}. szinten áll az utolsó igény teljesítése után.",
        liftek[igenySz-1].csz);
    }

    static void Negyedik()
    {
      Console.WriteLine("\n4. feladat");
      int min = 100;
      int max = 0;

      for (int i = 0; i < igenySz; i++)
      {
        if (liftek[i].csz > max)
        {
          max = liftek[i].csz;
        }
        if (liftek[i].ksz > max)
        {
          max = liftek[i].ksz;
        }
        if (liftek[i].csz < min)
        {
          min = liftek[i].csz;
        }
        if (liftek[i].ksz < min)
        {
          min = liftek[i].ksz;
        }
      }

      if (min > kezdes) 
      {
        min = kezdes;
      }

      if( max < kezdes)
      {
        max = kezdes;
      }

      Console.WriteLine("Legalacsonybb szint: {0}",min);
      Console.WriteLine("Legmagasabb szint: {0}",max);
    }

    static void Otodik()
    {
      Console.WriteLine("\n5. feladat");
      
      // van benne valami vagy valaki
      int emberrel = 0;
      for (int i = 0; i < igenySz; i++)
      {
        if (liftek[i].ksz < liftek[i].csz)
        {
          emberrel++;
        }
      }

      // üresen fel
      int ures = 0;
      for (int i = 0; i < igenySz - 1; i++)
      {
        if (liftek[i].csz < liftek[i + 1].ksz)
        {
          ures++;
        }
      }

      if (kezdes < liftek[0].ksz)
      {
        ures++;
      }

      Console.WriteLine("Felfelé csapattal: {0}",emberrel);
      Console.WriteLine("Felfelé üresen: {0}",ures);
    }

    static void Hatodik()
    {
      Console.WriteLine("\n6. feladat");
      Console.WriteLine("Nem vették igénybe: ");
      int[] voltak = new int[csapatSz + 1];

      for (int i = 0; i < liftek.Length; i++)
      {
        voltak[liftek[i].cs]++;
      }

      for (int i = 1; i < csapatSz + 1; i++)
      {
        if (voltak[i] == 0)
        {
          Console.Write("{0} ",i);
        }
      }

      Console.WriteLine();
    }

    static void Hetedik()
    {
      Console.WriteLine("\n7. feladat");
      Random vel = new Random();
      melyikCs = vel.Next(1, csapatSz + 1);
      Console.WriteLine("{0}. csapatot figyeljük",melyikCs);


      bukovari = new Lift[igenySz];
      mennyi = 0;

      for (int i = 0; i < igenySz; i++)
      {
        if (liftek[i].cs == melyikCs)
        {
          bukovari[mennyi] = liftek[i];
          mennyi++;
        }
      }

      int hol = 0;
      for (int i = 0; i < mennyi - 1; i++)
      {
        if (bukovari[i+1].ksz != bukovari[i].csz)
        {
          hol = i;
        }
      }

      if (hol == 0)
      {
        Console.WriteLine("Nincs bizonyítható szabálytalanság!");
      }
      else
      {
        Console.WriteLine("{0}. és {1}. emelet között séta.",
          bukovari[hol].csz, bukovari[hol+1].ksz);
      }

    }

    static void Nyolcadik()
    {
      Console.WriteLine("\n8. feladat");

      StreamWriter ki = new StreamWriter("blokkol.txt");

      for (int i = 0; i < mennyi; i++)
      {
        Console.Write("Sikeresség: ");
        string siker = Console.ReadLine();

        Console.Write("Feladatkód: ");
        string kod = Console.ReadLine();

        ki.WriteLine("----");
        ki.WriteLine("Indulási emelet: {0}", bukovari[i].ksz);
        ki.WriteLine("Célemelet: {0}", bukovari[i].csz);
        ki.WriteLine("Feladatkód: {0}", kod);
        ki.WriteLine("Befejezés ideje: {0}:{1}:{2}",
          bukovari[i].o, bukovari[i].p, bukovari[i].mp);
        ki.WriteLine("Sikeresség: {0}",siker);

      }

      ki.Close();
    }

    static void Main(string[] args)
    {
      Egy();
      Masodik();
      Harmadik();
      Negyedik();
      Otodik();
      Hatodik();
      Hetedik();
      Nyolcadik();

      Console.ReadKey();
    }
  }
}
