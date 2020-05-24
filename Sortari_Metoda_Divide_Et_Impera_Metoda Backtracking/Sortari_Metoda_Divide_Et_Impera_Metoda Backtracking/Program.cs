using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortari_Metoda_Divide_Et_Impera_Metoda_Backtracking
{
    class Program
    {
        #region Sortari
//        O metodă de sortare mai eficientă şi foarte des folosită este bubble sort sau sortarea prin
//metoda bulelor.Această metodă presupune parcurgerea vectorului şi compararea elementelor
//alăturate (a[i] şi a[i+1]); dacă ele nu respectă ordinea dorită, îşi vor interschimba valorile.Vectorul va fi
//parcurs de atâtea ori, până când va fi sortat, adică nu se va mai efectua nici o interschimbare de
//valori.În acest scop va fi folosită o variabilă de tip întreg, care să „ţină minte” dacă s-au mai efectuat
//interschimbări.Secvenţa de algoritm corespunzătoare este:
        static void BubbleSort(int[] v)
        {
            int k = 0;
            bool ok;
            do
            {
                ok = false;
                for (int i = 0; i < v.Length - 1 - k; i++)
                    if (v[i] > v[i + 1])
                    {
                        Swap(ref v[i], ref v[i + 1]);
                        ok = true;
                    }
                k++;
            } while (ok);
        }


//        Algoritmul Selection sort constă în alegerea celui mai mic element dintr-un vector şi aşezarea lui pe prima
//poziţie, repetată pentru şiruri din ce în ce mai scurte.Metoda necesită un timp de lucru care depinde
//de numărul de elemente din vector, iar algoritmul metodei se reprezintă prin structuri repetitive cu
//număr cunoscut de paşi.
//În cazul unui vector sortat crescător, primul element (cu indice 1) este cel mai mic dintre cele cu indici
//de la 1 la n, cel de-al doilea este cel mai mic dintre cele cu indici de la 2 la n ş.a.m.d.
//Să considerăm un vector în care elementele cu indici de la 1 la i-1 sunt deja sortate.Pentru a
//continua procesul de sortare, dintre elementele rămase(cu indici de la i până la n) trebuie găsit cel
//mai mic(cu indice isel) şi adus în poziţia i.
        static void SelectionSort(int[] v)
        {
            for (int j = 0; j < v.Length; j++)
            {
                int poz = j;
                for (int i = j + 1; i < v.Length; i++)
                    if (v[i] < v[poz])
                        poz = i;
                Swap(ref v[j], ref v[poz]);
            }
        }




//        În cazul sortării prin inserţie se consideră că vectorul este sortat pană la o anumită poziţie şi se
//încearcă inserarea următorului element pe poziţia potrivită.Dacă vectorul ar avea un singur element,
//el ar fi deja sortat; în cazul unui vector cu 2 elemente, care nu respectă relaţia de ordine, este
//suficientă inversarea acestora.Să presupunem că vectorul are mai mult de 2 elemente şi că am
//sortat deja, în ordine crescătoare, primele i-1 elemente.Pentru a extinde sortarea la i elemente este
//necesară deplasarea la dreapta, cu o poziţie, a tuturor elementelor mai mari decât cel cu indicele i,
//urmată de inserarea sa în poziţia corespunzătoare.Aceasta presupune compararea elementului 3
//inserat cu cele deja sortate, situate la stânga sa.Dacă această operaţie se efectuează de la dreapta
//spre stânga, atunci ea poate fi combinată cu cea de deplasare.

        static void InsertionSort(int[] v)
        {
            for (int j = 1; j < v.Length; j++)
                for (int i = j; i > 0; i--)
                    if (v[i] < v[i - 1])
                        Swap(ref v[i], ref v[i - 1]);
        }
//        În cazul  metode Merge sort vectorul de sortat este divizat în subvectori, prin înjumătăţiri succesive, cât
//timp lungimea acestora este > 2. Evident, un subvector de lungime 1 este sortat, iar un subvector de
//lungime 2 necesită cel mult interschimbarea celor doua valori.Monotoniile din subvectorii sortaţi sunt
//interclasate succesiv, în ordinea inversă divizării, obţinând în final vectorul sortat.Deoarece
//interclasarea necesită un vector auxiliar, sortarea propriu-zisă va fi precedată de alocarea unei zone
//tampon de aceeaşi lungime cu vectorul de sortat.
//Pentru a evita copierea rezultatului unei interclasari din vectorul auxiliar în cel de sortat şi a reduce
//astfel numărul de operaţii, vectorul iniţial şi cel auxiliar pot fi utilizaţi alternativ ca sursă şi, respectiv,
//rezultat al operaţiei de interclasare.
        static void MergeSort(int[] v, int st, int dr)
        {
            if (st < dr)
            {
                int m = (st + dr) / 2;
                MergeSort(v, st, m);
                MergeSort(v, m + 1, dr);
                Interclasare(v, st, m, dr);
            }
        }

        //Fiind dați doi vectori sortați, prin interclasarea lor se înțelege construirea unui
        //al treilea vector sortat care să conțină toate elementele acestora.
        static void Interclasare(int[] v, int st, int m, int dr)
        {
            int[] _v = new int[dr - st + 2];
            int i = st, j = m + 1, k = 0;
            while (i <= m && j <= dr)
            {
                if (v[i] <= v[j])
                    _v[++k] = v[i++];
                else
                    _v[++k] = v[j++];
            }

            while (i <= m)
                _v[++k] = v[i++];
            while (j <= dr)
                _v[++k] = v[j++];
            for (i = 1; i <= k; ++i)
                v[st + i - 1] = _v[i];
        }



        static Random random = new Random();

        static int Partitie(int[] v, int st, int dr)
        {
            int poz = st + random.Next(0, dr - st + 1); //Se ia pozitia pivot la intamplare
            int tmp = v[poz];
            v[poz] = v[st];
            v[st] = tmp;
            int V = v[st];
            --st; ++dr;
            //Se reorganizeaza lista in functie de pivot a.i cele mai mici fata de pivot sunt inainte pivotului, iar cele
            //mai mari decat pivotul sa fie in partea dreapta.
            while (st < dr)
            {
                do
                    --dr;
                while (st < dr && v[dr] > V);
                do
                    ++st;
                while (st < dr && v[st] < V);
                if (st < dr)
                {
                    int aux = v[st];
                    v[st] = v[dr];
                    v[dr] = aux;
                }
            }
            return dr;
        }
 //     Metoda QuickSort presupune gasirea pozitiei finale pe care o ocupa elemenetul de pe prima  pozitie comparandu-l cu  elementele din  cealalta partitie  a tabelului, acest algoritm realizandu-se pana cand partitia are 1 element.
 //     Potrivit algoritmului, fiecare element este comparat cu pivoul, adicã operatiunea este de O(N),  tabela este divizatã în douã pãrti, fiecare parte este divizatã iarãsi în douã.Dacã  fiecare parte  este împãrtitã  aproximativ în  jumãtate, va rezulta  log2N împãrtiri. Deci timpul de executie al Quicksortului în caz mediu este de O(N log2  N), iar în caz nefavorabil O(N2) .
 //     Quicksort este o metodã bunã în caz general, dar nu si în caz nefavorabil când este preferabil  folosirea a 3 indicii de impartire.Randomizarea este o idee importantã si folositoare, o unealtã generalã pentru a îmbunãtãti algoritmul.Quicksort este sensibil la ordinea datelor de intrare.Nu este o metodã stabilã.
 //     Dezavantajul algoritmului este cã, e recursiv. Necesitã în jur de N 2 de operatii în caz nefavorabil.Este fragil, o simplã gresealã în implementare poate cauza o executare gresitã pentru anumite fisiere.
        static void Quicksort(ref int[] v, int st, int dr)
        {
            while (st < dr)
            {
                int P = Partitie(v, st, dr);
                if (P - st < dr - P - 1)
                {
                    Quicksort(ref v, st, P);
                    st = P + 1;
                }
                else
                {
                    Quicksort(ref v, P + 1, dr);
                    dr = P;
                }
            }
        }




        //Cu metoda Swap schimbăm două elemente dintr-un vector
        static void Swap(ref int a, ref int b)
        {
            int aux = a;
            a = b;
            b = aux;
        }
        #endregion

        #region Backtracking
        //Metoda backtracking poate fi folosită în rezolvarea a diverse probleme.
        //Este o metodă lentă, dar de multe ori este singura pe care o avem la dispoziție

//        Metoda backtracking poate fi aplicată în rezolvarea problemelor care respectă următoarele condiții:

//soluția poate fi reprezentată printr-un tablou x[]=(x[1], x[2], ..., x[n]), fiecare element x[i] aparținând unei mulțimi cunoscute Ai;
//        fiecare mulțime Ai este finită, iar elementele ei se află într-o relație de ordine precizată – de multe ori cele n mulțimi sunt identice;
//        se cer toate soluțiile problemei sau se cere o anumită soluție care nu poate fi determinată într-un alt mod(de regulă mai rapid).
//Algoritmul de tip backtracking construiește vectorul x[] (numit vector soluție) astfel:

//Fiecare pas k, începând de la pasul 1, se prelucrează elementul curent x[k] al vectorului soluție:

//x[k] primește pe rând valori din mulțimea corespunzătoare Ak;
//la fiecare pas se verifică dacă configurația curentă a vectorului soluție poate duce la o soluție finală – dacă valoarea lui x[k] este corectă în raport cu x[1], x[2], … x[k - 1]:
//dacă valoarea nu este corectă, elementul curent X[k] primește următoarea valoare din Ak sau revenim la elementul anterior x[k - 1], dacă X[k] a primit toate valorile din Ak – pas înapoi;
//        dacă valoarea lui x[k] este corectă(avem o soluție parțială), se verifică existența unei soluții finale a problemei:
//dacă configurația curentă a vectorului soluție x reprezintă soluție finală(de regulă) o afișăm;
//        dacă nu am identificat o soluție finală trecem la următorul element, x[k + 1], și reluăm procesul pentru acest element – pas înainte.
//          Pe măsură ce se construiește, vectorul soluție x[] reprezită o soluție parțială a problemei.Când vectorul soluție este complet construit, avem o soluție finală a problemei.

// O problemă clasică de bactracking este problema permutărilor:
        static void Permutari(int k, int n, int[] sol, bool[] b)
        {
            if (k >= n)
            {
                for (int i = 0; i < n; i++)
                    Console.Write(sol[i] + " ");
                Console.WriteLine();
            }
            else
                for (int i = 0; i < n; i++)
                    if (!b[i])
                    {
                        b[i] = true;
                        sol[k] = i + 1; //pentru a incepe de la 1, nu de la 0
                        Permutari(k + 1, n, sol, b);
                        b[i] = false;
                    }
        }
        #endregion

        #region DivideEtImpera
        //Divide et impera este o clasă de algoritmi care funcționează pe baza tacticii divide et impera.

//        Divide et impera se bazează pe principiul descompunerii problemei în două sau mai multe subprobleme(mai ușoare), care se rezolvă, iar soluția pentru problema inițială se obține combinând soluțiile subproblemelor.De multe ori, subproblemele sunt de același tip și pentru fiecare din ele se poate aplica aceeași tactică a descompunerii în (alte) subprobleme, până când (în urma descompunerilor repetate) se ajunge la probleme care admit rezolvare imediată.

//Nu toate problemele pot fi rezolvate prin utilizarea acestei tehnici.Se poate afirma că numărul celor rezolvabile prin "divide et impera" este relativ mic, tocmai datorită cerinței ca problema să admită o descompunere repetată.


//Divide et impera este o tehnică ce admite o implementare recursivă.Principiul general prin care se elaborează algoritmi recursivi este: "ce se întâmplă la un nivel, se întâmplă la orice nivel" (având grijă să asigurăm condițiile de terminare). Așadar, un algoritm prin divide et impera se elaborează astfel: la un anumit nivel avem două posibilități:

//s-a ajuns la o problemă care admite o rezolvare imediată(condiția de terminare), caz în care se rezolvă și se revine din apel;
//        nu s-a ajuns în situația de la punctul 1, caz în care problema curentă este descompusă în(două sau mai multe) subprobleme, pentru fiecare din ele urmează un apel recursiv al funcției, după care combinarea rezultatelor are loc fie pentru fiecare subproblemă, fie la final, înaintea revenirii din apel.

        // În aplicație exemplu pentru metoda divide et impera este căutarea binară.
        static bool BinarySearch(int[] v, int x, int st, int dr)
        {
            if (st <= dr)
            {
                int m = (st + dr) / 2;
                if (v[m] == x) return true;
                if (x > v[m])
                    return BinarySearch(v, x, m + 1, dr);
                else
                    return BinarySearch(v, x, st, m - 1);
            }
            return false;
        }

        #endregion
        static void Main(string[] args)
        {
            Console.WriteLine("Bubble Sort");
            int[] vector = new int[] { 2, 5, 3, 6, 1, 7, 9, 4, 8 };//Initializez vectorul
            Console.Write("vectorul initial:");
            Afisare(vector);//Afisez vectorul initial
            BubbleSort(vector);//Apelez metoda pentru sortare
            Console.Write("vectorul sortat:");
            Afisare(vector);//Afisez vectorul sortat
            Console.WriteLine();


            Console.WriteLine("Insertion Sort");
            vector = new int[] { 2, 5, 3, 6, 1, 7, 9, 4, 8 };//Resetăm vectorul
            Console.Write("vectorul initial:");
            Afisare(vector);//Afisez vectorul initial
            InsertionSort(vector);//Apelez metoda pentru sortare
            Console.Write("vectorul sortat:");
            Afisare(vector);//Afisez vectorul sortat
            Console.WriteLine();


            Console.WriteLine("Selection Sort");
            vector = new int[] { 2, 5, 3, 6, 1, 7, 9, 4, 8 };//Resetăm vectorul
            Console.Write("vectorul initial:");
            Afisare(vector);//Afisez vectorul initial
            SelectionSort(vector);//Apelez metoda pentru sortare
            Console.Write("vectorul sortat:");
            Afisare(vector);//Afisez vectorul sortat
            Console.WriteLine();


            Console.WriteLine("Merge Sort");
            vector = new int[] { 2, 5, 3, 6, 1, 7, 9, 4, 8 };//Resetăm vectorul
            Console.Write("vectorul initial:");
            Afisare(vector);//Afisez vectorul initial
            MergeSort(vector,0,vector.Length-1);//Apelez metoda pentru sortare
            Console.Write("vectorul sortat:");
            Afisare(vector);//Afisez vectorul sortat
            Console.WriteLine();

            Console.WriteLine("Quick Sort");
            vector = new int[] { 2, 5, 3, 6, 1, 7, 9, 4, 8 };//Resetăm vectorul
            Console.Write("vectorul initial:");
            Afisare(vector);//Afisez vectorul initial
            Quicksort(ref vector,0,vector.Length-1);//Apelez metoda pentru sortare
            Console.Write("vectorul sortat:");
            Afisare(vector);//Afisez vectorul sortat
            Console.WriteLine();

            Console.WriteLine("Cautarea binară");
            vector = new int[] { 2, 5, 3, 6, 1, 7, 9, 4, 8 };//Resetăm vectorul
            int x = 4;
            if(BinarySearch(vector, x, 0, vector.Length)==true)// Verific dacă numarul x se află în vector
            {
                Console.WriteLine("Numărul {0} se află în vector.", x);//afisez rezultatul
            }
            else
            {
                Console.WriteLine("Numărul {0} nu se află în vector.", x);// afisez rezultatul
            }
            Console.WriteLine();

            Console.WriteLine("Permutări (3)");
            int n = 3;
            int[] sol = new int[n];
            bool[] b = new bool[n];
            Permutari(0, n, sol,b);// apelez metoda pentru afisarea permutarilor
            Console.WriteLine();

            Console.ReadKey();
        }
        //metoda pentru afisarea unui vector
        static void Afisare(int[] vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                Console.Write(vector[i]+", ");
            }
            Console.WriteLine();
        }
    }
}
