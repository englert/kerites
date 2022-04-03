using System; 
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
0 10 P
1 8 K
1 10 :
*/
class Kerites 
{
    public int     oldal_id               {set; get;}
    public int     hossz                  {set; get;}
    public string  szin                   {set; get;}
    public static int paros_hazszam_szamlalo = 0;
    public static  int paratlan_hazszam_szamlalo = -1;
    public int     hazszam                {set; get;}
    public static string   elozo_szin = "";
    public bool    egyforma               {set; get;}
    
    public Kerites(string sor)
    {
        var s = sor.Trim().Split(" ");
        oldal_id = int.Parse(s[0]);
        hossz    = int.Parse(s[1]);
        szin     = s[2];
        egyforma = (elozo_szin == szin) ? true: false;
        elozo_szin = szin;
        
        if (oldal_id == 0)
        {
            paros_hazszam_szamlalo += 2;
            hazszam = paros_hazszam_szamlalo;
        }
        else
        {
            paratlan_hazszam_szamlalo += 2;
            hazszam = paratlan_hazszam_szamlalo;
        }        
    }
}

class Program 
{
    public static void Main(string[] args) 
    {
        var sr = new StreamReader("kerites.txt");
        var lista = new List<Kerites>();
        while(!sr.EndOfStream)
        {
            lista.Add(new Kerites(sr.ReadLine()));
        }
// 2. Írja a képernyőre, hogy hány telket adtak el az utcában!
        Console.WriteLine($"2. feladat");
        Console.WriteLine($"Az eladott telkek száma: {lista.Count}");
        Console.WriteLine();
/* 3. Jelenítse meg a képernyőn, hogy az utolsó eladott telek
a. melyik (páros / páratlan) oldalon talált gazdára!
b. milyen házszámot kapott!
*/
        var utolso_eladott = lista.Last();       
        string oldal = (utolso_eladott.oldal_id == 0) ? "páros" : "páratlan";
        
        Console.WriteLine($"3. feladat");
        Console.WriteLine($"A {oldal} oldalon adták el az utolsó telket.");
        Console.WriteLine($"Az utolsó telek házszáma: {utolso_eladott.hazszam}");
        Console.WriteLine();

        /* 4. Írjon a képernyőre egy házszámot a páratlan oldalról, 
        amely melletti telken ugyanolyan színű a  kerítés! 
        (A hiányzó és a festetlen kerítésnek nincs színe.) 
        Feltételezheti, hogy van ilyen telek, a több ilyen közül elég az egyik ház számát megjeleníteni.
        */
        var paratlan_egyforma =
            (
                from sor in lista
                where sor.oldal_id == 1
                where sor.szin != "#"
                where sor.szin != ":"
                where sor.egyforma
                select sor.hazszam
            ).First();
        
        Console.WriteLine($"4. feladat");
        Console.WriteLine($"A szomszédossal egyezik a kerítés színe: {paratlan_egyforma-2}");
        Console.WriteLine();

        /* 5. Kérje be a felhasználótól egy eladott telek házszámát, 
        majd azt felhasználva oldja meg a következő feladatokat!

        a. Írja ki a házszámhoz tartozó kerítés színét, ha már elkészült és befestették,
        egyébként az állapotát a „#” vagy „:” karakter jelöli!
        */
        
        int hazszam = 83;
        var hazszam_szin = new Dictionary<int, string>();
        foreach (var sor in lista)
        {
            hazszam_szin[sor.hazszam] = sor.szin;
        }
        
        Console.WriteLine($"5. feladat");
        Console.WriteLine($"A kerítés színe / állapota: {hazszam_szin[hazszam]}");

        /*
        5.b. A házszámhoz tartozó kerítést szeretné tulajdonosa be- vagy átfesteni. 
        Olyan színt akar választani, 
        amely különbözik a mellette lévő szomszéd(ok)tól és a jelenlegi színtől is. 
        Adjon meg egy lehetséges színt! 
        A színt a teljes palettából (A–Z) szabadon választhatja meg.
        */
            
        string szinek =  "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        szinek = szinek.Replace(hazszam_szin[hazszam], "");
       
        if (hazszam_szin.ContainsKey(hazszam-2))
        {
            var szin = hazszam_szin[hazszam-2];
            szinek = szinek.Replace(szin, "");
        }
        
        if (hazszam_szin.ContainsKey(hazszam+2))
        {
            var szin = hazszam_szin[hazszam+2];
            szinek = szinek.Replace(szin, "");
        }
        Console.WriteLine($"Egy lehetséges festési szín: {szinek[0]}");

        /*
        6. Jelenítse meg az utcakep.txt fájlban a páratlan oldal utcaképét az alábbi mintának megfelelően!
        KKKKKKKK::::::::::SSSSSSSSSBBBBBBBBFFFFFFFFFKKKKKKKKKKIIIIIIII
        1       3         5        7       9        11        13
        Az első sorban a páratlan oldal jelenjen meg, 
        a megfelelő méternyi szakasz kerítésszínét (vagy állapotát) jelző karakterrel! 
        A második sorban a telek első karaktere alatt kezdődően a házszám álljon!
        
    }
}