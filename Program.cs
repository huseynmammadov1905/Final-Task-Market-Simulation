

using FinalTaskMarketSimulation;
using System;
using System.Reflection.Emit;
using System.Text.Json;
using System.Timers;

public class Program
{
    bool checkWeekFood = true;//heftelik erzaq;
   public bool checkReport = true;//heftelik hesabat;
    bool checkEpidemiya = true;// epidemiya ucun;
    public bool checkNight = true;//gecenin dusmesi
    double rating = 0.01;// alici terevez aldiqda artan rating
    bool checkRottenVegBuy = true;//xarab olmus terevez aldiqda;
    public bool checkMorning = true;//seherin acilmasi;
    int rotten = 0;//  curuk terevezlerin sayi
    int countEmp = 0;//alici sayi
    bool firstVegsBuy = true;// ilk defe magazaya erzaq almaq ucun;
    double cost = 0;
    int emploeeNo = 0;// alici No;
    double xiyarPrice = 1;
    double pomidorPrice = 1.5;
    double kokPrice = 1.5;
    double kartofPrice = 1;
    double badimcanPrice = 2;

    public List<Report> list = new List<Report>();
    Program() { }

    public static int second { get; set; } = 0;
    static public int hour = 10;
    public static int day = 1;
    public static int month = 0;
    public static int year = 0;







    static void Main(string[] args)
    {

        Report report = new Report();

        Market market = new();

        print();

        System.Timers.Timer timer = new();
        timer.Interval = 1000;
        timer.Elapsed += Timer_Elapsed;

        timer.Start();

        Program program = new();

        program.Market(market, report);


        Thread.Sleep(1000000000);
        timer.Stop();


    }

    public void Event(ref Stack<Vegetable> v)
    {
        Random random = new Random();


        foreach (var item in v)
        {
            int rand = random.Next(0, 3);
            item.vegetableSituaion += rand;
        }
    }
    static void print()
    {
        Console.WriteLine($"Day - {day}   Hour - {hour}   Month - {month}  Year-{year}");
    }


    static void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        //her bir saniyeden bir Timwer_Elapsed funksiyasina muraciet edir,saniye (second 10-a catdiqda saat bir vahid artir ve saniye yeniden sifirlanir.
        if (second == 10)
        {
            Console.Clear();
            print();
            hour++;
            if (hour == 24)
            {
                hour = 10;

            }
            second = 0;
        }

        second++;


    }

    public void Serializer(ref List<Report> l)
    {
        var json = JsonSerializer.Serialize(l);
        File.WriteAllText("Reportjson", json);
    }
    public void Market(Market market, Report report)
    {


        int epidemiya = 7;
        while (true)
        {
            Random random = new Random();
        Label:
            //if (checkReport)
            //{
            //    day++;
            //    checkReport= false;
            //}

            if (checkEpidemiya == true && month == epidemiya)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("DIQQET EPIDEMIYA BASH VERDI!!!!!");
                Thread.Sleep(700);
                Console.WriteLine("Epidemiya bash verdiyi ucun muwteri gelmir");
                Thread.Sleep(700);
                Console.WriteLine("Epidemiya muddetini kecmek ucun klaviaturada 'SPACE' duymeye basin ,kecmek istemirsinizse oturun gozlein :)");
                Thread.Sleep(3999);
            Label1:
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Pandemiya davam edir ....");
                Console.ForegroundColor = ConsoleColor.White;
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Spacebar)
                {
                    month++;
                    checkEpidemiya = false;

                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Pandemiya bitdi :)");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(800);
                    goto Label;
                }
                else
                {
                    goto Label1;

                }


            }

            if (hour >= 10 && checkMorning == true)// seher saat 10-da market acilir.
            {
               
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Seher acildi ");
                Thread.Sleep(500);
                Console.WriteLine("Ishciler geldi");
                Thread.Sleep(500);
                Console.WriteLine("Market Acildi");
                Thread.Sleep(500);
                checkMorning = false;
                checkNight = true;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if ((hour > 22 || hour < 10) && checkNight == true)// axwam saat 10-da market baglanir
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Gece oldu");
                Thread.Sleep(500);
                Console.WriteLine("Market baglandi");
                Thread.Sleep(500);
                Console.WriteLine("Ishcilerin evlerine getdiler");
                Thread.Sleep(500);
                Event(ref market.xiyar);
                Event(ref market.kok);
                Event(ref market.pomidor);
                if (market.rating > 5)
                    Event(ref market.kok);
                if (market.rating > 10)
                    Event(ref market.badimcan);
                Console.WriteLine("Terevezler biraz qocaldi");
                checkNight = false;
                Thread.Sleep(3000);
                hour = 10;
                checkMorning = true;
                Console.ForegroundColor = ConsoleColor.White;
                checkWeekFood = true;
                checkReport = true;
                if (day == 30)
                {
                    day = 1;
                    if (month == 12)
                    {
                        month = 0;
                        year++;
                    }
                    else if (month < 12)
                        month++;
                }
                else { day++; }
                goto Label;
            }

            if (day % 7 == 0 && hour >= 20 && checkReport == true)
            {   Console.ForegroundColor= ConsoleColor.Yellow;
                Console.WriteLine("HEFTELIK HESABAT");

                //rating ekrana cixarilir v reporta(json) yazilir
                Console.Write("Market Rating : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(market.rating);
                Console.ForegroundColor = ConsoleColor.White;
                report.marketRating = market.rating;

                //Gelir ekrana cixarilir v reporta(json) yazilir
                Console.Write($"Heftelik gelir : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(market.dayCash);
                Console.ForegroundColor = ConsoleColor.White;
                market.marketMoney += market.dayCash;
                report.weakMoney = market.dayCash;
                market.dayCash = 0;
                Console.Write($"Worker Salary :");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(market.countWorker * 140);// ishciler gunde 20 dollar qazanir (* 140 -> heftelik gelir)
                Console.ForegroundColor = ConsoleColor.White;
                market.marketMoney -= market.countWorker * 140;
                cost += market.countWorker * 140;
                report.cost = cost;

                Console.Write($"Cost :");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(cost);
                Console.ForegroundColor = ConsoleColor.White;
                report.cost = cost;
                Console.Write($"The rest of the money : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(market.marketMoney);
                Console.ForegroundColor = ConsoleColor.White;
                if (market.marketMoney <= -100)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("IFLAS!!!! :(");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                report.AllMoney = market.marketMoney;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Qalan terevezlerin sayi : \n{market.xiyar.Count} xiyar\n{market.pomidor.Count} pomidor\n{market.kok.Count} kok \n{market.kartof.Count} kartof \n{market.badimcan.Count} badimcan ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Atilan terevezler {rotten}");
                report.rotten = rotten;
                report.allVegetableCount = market.xiyar.Count + market.pomidor.Count + market.kok.Count + market.kartof.Count + market.badimcan.Count;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Umumi alici sayi <{countEmp}> nefer");
                Console.ForegroundColor = ConsoleColor.White;
                report.countEmp = countEmp;

                Thread.Sleep( 3000);
                list.Add(report);
                Serializer(ref list);
                checkReport = false;


            }

            if (day % 7 == 0 && checkWeekFood == true || firstVegsBuy == true)
            {
                firstVegsBuy= false;
                if (market.marketMoney < 50)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erzaq almaq ucun yeterli budce yoxdur");
                    Console.WriteLine("IFLAS!!!! :(");
                    Thread.Sleep(4000);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }

                checkWeekFood = false;
                int buyCount = 0;//nece nov terevez alinacaq ,rating artarsa terevezlerin novleri de artacaq
                int Buy = (int)market.marketMoney / 4 * 3;// market her zaman pulunun 3/4 - nu erzaqlara ayirir.
                cost += Buy;
                market.marketMoney -= Buy;
                if (market.rating >= 1 && market.rating <= 5)
                {
                    buyCount = Buy / 3;
                }
                else if (market.rating > 5 && market.rating < 10)
                {
                    market.countWorker += 2;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"Iscilerin sayi artdi :{market.countWorker} + {2} ");
                    Thread.Sleep(1500);
                    buyCount = Buy / 4;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    market.countWorker += 2;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"Iscilerin sayi artdi :{market.countWorker} + {2} ");
                     Thread.Sleep(1500);
                    buyCount = Buy / 5;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // eger Stackda Terevez varsa onlar kohne terevez hesab olunur ve onlar ustden yigilir,yeni gelen terevezler ise altdan yigilir
                // temp adinda bir stack yaradiram ve kohne terevezleri bura yigiram,sonra esas stack'ime yeni terevezleri elave edirem .Sonra ise temp stacindeki normal terevezleri esas stacka elave edirem,curukler ise atilir. 

                if (market.xiyar.Count > 0)
                {

                    Stack<Vegetable> temp = new();
                    for (int i = 0; i < market.xiyar.Count; i++)
                    {
                        temp.Push(market.xiyar.Peek());

                        market.xiyar.Pop();
                    }
                    for (int j = 0; j < buyCount; j++)
                    {
                        Vegetable xiyar = new("xiyar", 1,1);
                        market.xiyar.Push(xiyar);

                    }

                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (temp.Peek().vegetableSituaion <= 8)
                        {
                            market.xiyar.Push(temp.Peek());
                            temp.Pop();
                        }
                        else if (temp.Peek().vegetableSituaion > 8)
                        {
                            rotten++;
                            temp.Pop();
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < buyCount; j++)
                    {
                        Vegetable xiyar = new("xiyar", 1,1);
                        market.xiyar.Push(xiyar);
                    }
                }
                if (market.rating >= 6 && market.rating < 10)
                {
                    if (market.kartof.Count > 0)
                    {
                        Stack<Vegetable> temp = new();
                        for (int i = 0; i < market.kartof.Count; i++)
                        {

                            temp.Push(market.kartof.Peek());
                            market.kartof.Pop();
                        }
                        for (int i = 0; i < buyCount; i++)
                        {
                            Vegetable kartof = new("kartof", 1, 1);
                            market.kartof.Push(kartof);
                        }

                        for (int i = 0; i < temp.Count; i++)
                        {
                            if (temp.Peek().vegetableSituaion <= 8)
                            {
                                market.kartof.Push(temp.Peek());
                                temp.Pop();
                            }
                            else if (temp.Peek().vegetableSituaion > 8)
                            {
                                rotten++;
                                temp.Pop();
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < buyCount; i++)
                        {
                            Vegetable kartof = new("kartof", 1, 1);
                            market.kartof.Push(kartof);
                        }
                    }
                }
                if (market.rating >= 10)
                {
                    if (market.badimcan.Count > 0)
                    {
                        Stack<Vegetable> temp = new();
                        for (int i = 0; i < market.badimcan.Count; i++)
                        {

                            temp.Push(market.badimcan.Peek());
                            market.badimcan.Pop();
                        }
                        for (int i = 0; i < buyCount; i++)
                        {
                            Vegetable badimncan = new("badimcan", 2, 2);
                            market.badimcan.Push(badimncan);
                        }
                        for (int i = 0; i < temp.Count; i++)
                        {
                            if (temp.Peek().vegetableSituaion <= 8)
                            {
                                market.badimcan.Push(temp.Peek());
                                temp.Pop();
                            }
                            else if (temp.Peek().vegetableSituaion > 8)
                            {
                                rotten++;
                                temp.Pop();
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < buyCount; i++)
                        {
                            Vegetable badimncan = new("badimcan", 2, 2);
                            market.badimcan.Push(badimncan);
                        }
                    }
                }
                if (market.pomidor.Count > 0)
                {
                    Stack<Vegetable> temp = new();
                    for (int i = 0; i < market.pomidor.Count; i++)
                    {

                        temp.Push(market.pomidor.Peek());
                        market.pomidor.Pop();
                    }
                    for (int k = 0; k < buyCount; k++)
                    {

                        Vegetable pomidor = new("pomidor", 1.5 ,1);
                        market.pomidor.Push(pomidor);
                    }
                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (temp.Peek().vegetableSituaion <= 8)
                        {
                            market.pomidor.Push(temp.Peek());
                            temp.Pop();
                        }
                        else if (temp.Peek().vegetableSituaion > 8)
                        {
                            rotten++;
                            temp.Pop();
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < buyCount; k++)
                    {
                        Vegetable pomidor = new("pomidor", 1.5,1);
                        market.pomidor.Push(pomidor);
                    }
                }
                if (market.kok.Count > 0)
                {
                    Stack<Vegetable> temp = new();
                    for (int i = 0; i < market.kok.Count; i++)
                    {

                        temp.Push(market.kok.Peek());
                        market.kok.Pop();
                    }
                    for (int k = 0; k < buyCount; k++)
                    {

                        Vegetable kok = new("kok", 1.5,1);
                        market.kok.Push(kok);
                    }
                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (temp.Peek().vegetableSituaion <= 8)
                        {
                            market.kok.Push(temp.Peek());
                            temp.Pop();
                        }
                        else if (temp.Peek().vegetableSituaion > 8)
                        {
                            rotten++;
                            temp.Pop();
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < buyCount; k++)
                    {

                        Vegetable kok = new("kok", 1.5,1);
                        market.kok.Push(kok);
                    }
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Yeni erzaqlar geldi...");
                Thread.Sleep(1000);
                Console.WriteLine("Stendler yeniden duzulur...");
                Thread.Sleep(1000);
                Console.WriteLine("Curuk terevezler atilir...");
                Thread.Sleep(1000);
                Console.WriteLine($"{rotten} curuk terevez atildi");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
            // dukana her saatda bir nece defe  alici gelir;
            Console.WriteLine("Magaza hal-hazirda bowdur,muwteri gozlenilir...");
            Thread.Sleep(4500);
            int employeeCountHour = 0;//saat erzinde gelen alici sayi
            int rand = random.Next(0, 3);//rating awaqi olduqda gelecek alici sayi
            if (market.rating >= 12)
            {
                rating = 0;//rating 12 olsa daha artmasin deye(max rating 12);
            }
            else if (market.rating < 12)
            {
                rating = 0.01;
            }
            if (market.rating <= 3.5)
                employeeCountHour = rand == 0 ? 1 : rand;
            else
                employeeCountHour = (int)market.rating + 1;
            for (int i = 0; i < employeeCountHour; i++)
            {
                Random random1 = new Random();// Burada alicilarin hansi erzaqlari alacaqlari random secilir(ratinge gore erzaq cewidliliyi var)
                int randomVegetable;
                if (market.rating > 10)
                {
                    randomVegetable = random1.Next(1, 6);
                }
                else if (market.rating > 5)
                {
                    randomVegetable = random1.Next(1, 5);
                }
                else
                {
                    randomVegetable = random1.Next(1, 4);
                }
                int randomBuyCount = random1.Next(1, 5);//Bu random ise ona goredir ki, bezi aliciler yalniz bir cewid terevez alacaq,Bezileri ise daha artiq .Nece cewid terevez alacagi burada Random olaraq secilir;
                Employee employee = new Employee();
                employee.id = ++emploeeNo;
                if (randomBuyCount > 2)
                {

                    if (market.rating > 10)
                    {
                        employee.kind = random1.Next(1, 6);
                    }
                    else if (market.rating > 5)
                    {
                        employee.kind = random1.Next(1, 5);
                    }
                    else
                    {
                        employee.kind = random1.Next(1, 4);
                    }

                }
                switch (randomVegetable)
                {
                    case 1:
                        market.employeesXiyar.Enqueue(employee);
                        break;
                    case 2: market.employeesPomidor.Enqueue(employee); break;
                    case 3:
                        market.employeesKok.Enqueue(employee);
                        break;
                    case 4:
                        market.employeesKartof.Enqueue(employee);
                        break;
                    case 5:
                        market.employeesBadimcan.Enqueue(employee);
                        break;
                }

            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{employeeCountHour} sayda alici geldi ");
            countEmp += employeeCountHour;

            Console.ForegroundColor = ConsoleColor.White;

            Thread.Sleep(1000);

            while (market.employeesXiyar.Count != 0 || market.employeesPomidor.Count != 0 || market.employeesKok.Count != 0 || market.employeesKartof.Count != 0 || market.employeesBadimcan.Count != 0)
            {
                Random random2 = new Random();
                int randomBuyVegsCount;//alicinin her hansisa terevezden neche dene alacagini random secir
                if (market.employeesXiyar.Count > 0)
                {
                    randomBuyVegsCount = random2.Next(1, 4);
                    if (market.xiyar.Count < randomBuyVegsCount)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Alici <{market.employeesXiyar.Peek().id}> istediyi erzaqi tapa bilmediyi ucun magazani terk etdi");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(500);
                        market.employeesXiyar.Dequeue();
                    }
                    else
                    {

                        for (int i = 0; i < randomBuyVegsCount; i++)
                        {
                            if (market.xiyar.Peek().vegetableSituaion > 8)
                            {
                                market.xiyar.Pop();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Alicinin aldigi terevez curukdur ve o pulu odemeden magazani terk etdi ,rating dushdu {-0.1}");
                                Thread.Sleep(1000);
                                Console.ForegroundColor = ConsoleColor.White;
                                market.employeesXiyar.Dequeue();
                                checkRottenVegBuy = false;
                                market.rating -= 0.1;
                                break;
                            }
                            else
                            {
                                market.xiyar.Pop();
                                checkRottenVegBuy = true;
                            }

                        }
                        if (checkRottenVegBuy)
                        {
                            market.dayCash += randomBuyVegsCount * (xiyarPrice + 2);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Alici <{market.employeesXiyar.Peek().id}> {randomBuyVegsCount} xiyar aldi");
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write($"Market Rating : {(float)market.rating} +  ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(rating);
                            market.rating += rating;
                            Console.ForegroundColor = ConsoleColor.White;
                            Thread.Sleep(500);
                            if (market.employeesXiyar.Count > 0)
                            {
                                switch (market.employeesXiyar.Peek().kind)
                                {

                                    case 2:
                                        market.employeesPomidor.Enqueue(market.employeesXiyar.Peek());
                                        market.employeesXiyar.Dequeue(); break;
                                    case 3:
                                        market.employeesKok.Enqueue(market.employeesXiyar.Peek());
                                        market.employeesXiyar.Dequeue(); break;
                                    case 4:
                                        market.employeesKartof.Enqueue(market.employeesXiyar.Peek());
                                        market.employeesXiyar.Dequeue(); break;
                                    case 5:
                                        market.employeesBadimcan.Enqueue(market.employeesXiyar.Peek());
                                        market.employeesXiyar.Dequeue();
                                        break;
                                    default:

                                        Console.WriteLine($"Alici <{market.employeesXiyar.Peek().id}> Magazani terk etdi ");
                                        Thread.Sleep(500);
                                        market.employeesXiyar.Dequeue();
                                        break;
                                }
                            }
                        }

                    }

                }

                if (market.employeesPomidor.Count > 0)
                {

                    randomBuyVegsCount = random2.Next(1, 4);
                    if (market.pomidor.Count < randomBuyVegsCount)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Alici <{market.employeesXiyar.Peek().id}> istediyi erzaqi tapa bilmediyi ucun magazani terk etdi");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(500);
                        market.employeesPomidor.Dequeue();
                    }
                    else
                    {
                        for (int i = 0; i < randomBuyVegsCount; i++)
                        {
                            if (market.pomidor.Peek().vegetableSituaion > 8)
                            {
                                market.pomidor.Pop();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Alicinin aldigi terevez curukdur ve o pulu odemeden magazani terk etdi ,rating dushdu {-0.1}");
                                Thread.Sleep(1000);
                                Console.ForegroundColor = ConsoleColor.White;
                                market.employeesPomidor.Dequeue();
                                market.rating -= 0.1;
                                checkRottenVegBuy = false;
                                break;
                            }
                            else
                            {
                                market.pomidor.Pop();
                                checkRottenVegBuy = true;
                            }
                        }
                        if (checkRottenVegBuy)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Alici <{market.employeesPomidor.Peek().id}> {randomBuyVegsCount} pomidor aldi");
                            Console.ForegroundColor = ConsoleColor.White;
                            market.dayCash += randomBuyVegsCount * (pomidorPrice + 2);
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write($"Market Rating : {(float)market.rating} +  ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(rating);
                            market.rating += rating;
                            Console.ForegroundColor = ConsoleColor.White;
                            if (market.employeesPomidor.Count != 0)
                            {
                                switch (market.employeesPomidor.Peek().kind)
                                {

                                    case 1:
                                        market.employeesXiyar.Enqueue(market.employeesPomidor.Peek());
                                        market.employeesPomidor.Dequeue(); break;
                                    case 3:
                                        market.employeesKok.Enqueue(market.employeesPomidor.Peek());
                                        market.employeesPomidor.Dequeue(); break;
                                    case 4:
                                        market.employeesKartof.Enqueue(market.employeesPomidor.Peek());
                                        market.employeesPomidor.Dequeue(); break;
                                    case 5:
                                        market.employeesBadimcan.Enqueue(market.employeesPomidor.Peek());
                                        market.employeesPomidor.Dequeue();
                                        break;
                                    default:
                                        Console.WriteLine($"Alici <{market.employeesPomidor.Peek().id}> Magazani terk etdi ");
                                        Thread.Sleep(500);
                                        market.employeesPomidor.Dequeue();
                                        break;
                                }
                            }
                        }
                    }
                }
                if (market.employeesKok.Count > 0)
                {
                    randomBuyVegsCount = random2.Next(1, 4);
                    if (market.kok.Count < randomBuyVegsCount)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Alici <{market.employeesXiyar.Peek().id}> istediyi erzaqi tapa bilmediyi ucun magazani terk etdi");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(500);
                        market.employeesKok.Dequeue();
                    }
                    else
                    {
                        for (int i = 0; i < randomBuyVegsCount; i++)
                        {
                            if (market.kok.Peek().vegetableSituaion > 8)
                            {
                                market.kok.Pop();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Alicinin aldigi terevez curukdur ve o pulu odemeden magazani terk etdi ,rating dushdu {-0.1}");
                                Thread.Sleep(1000);
                                Console.ForegroundColor = ConsoleColor.White;
                                market.employeesKok.Dequeue();
                                checkRottenVegBuy = false;
                                market.rating -= 0.1;
                                break;
                            }
                            else
                            {
                                market.kok.Pop();
                                checkRottenVegBuy = true;
                            }
                        }
                        if (checkRottenVegBuy)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Alici <{market.employeesKok.Peek().id}> {randomBuyVegsCount} kok aldi");
                            Console.ForegroundColor = ConsoleColor.White;
                            market.dayCash += randomBuyVegsCount * (kokPrice + 2);
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write($"Market Rating : {(float)market.rating} +  ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(rating);
                            market.rating += rating;
                            Console.ForegroundColor = ConsoleColor.White;
                            Thread.Sleep(1000);

                            switch (market.employeesKok.Peek().kind)
                            {
                                case 1:
                                    market.employeesXiyar.Enqueue(market.employeesKok.Peek());
                                    market.employeesKok.Dequeue(); break;
                                case 2:
                                    market.employeesPomidor.Enqueue(market.employeesKok.Peek());
                                    market.employeesKok.Dequeue(); break;

                                case 4:
                                    market.employeesKartof.Enqueue(market.employeesKok.Peek());
                                    market.employeesKok.Dequeue(); break;
                                case 5:
                                    market.employeesBadimcan.Enqueue(market.employeesKok.Peek());
                                    market.employeesKok.Dequeue();
                                    break;
                                default:
                                    Console.WriteLine($"Alici <{market.employeesKok.Peek().id}> Magazani terk etdi ");
                                    Thread.Sleep(500);
                                    market.employeesKok.Dequeue();
                                    break;
                            }
                        }
                    }
                }
                if (market.employeesKartof.Count > 0)
                {
                    randomBuyVegsCount = random2.Next(1, 4);
                    if (market.kartof.Count < randomBuyVegsCount)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Alici <{market.employeesXiyar.Peek().id}> istediyi erzaqi tapa bilmediyi ucun magazani terk etdi");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(500);
                        market.employeesKartof.Dequeue();
                    }
                    else
                    {
                        for (int i = 0; i < randomBuyVegsCount; i++)
                        {
                            if (market.kartof.Peek().vegetableSituaion > 8)
                            {
                                market.kartof.Pop();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Alicinin aldigi terevez curukdur ve o pulu odemeden magazani terk etdi ,rating dushdu {-0.1}");
                                Thread.Sleep(1000);
                                Console.ForegroundColor = ConsoleColor.White;
                                market.employeesKartof.Dequeue();
                                checkRottenVegBuy = false;
                                market.rating -= 0.1;
                                break;
                            }
                            else
                            {
                                market.kartof.Pop();
                                checkRottenVegBuy = true;
                            }
                        }
                        if (checkRottenVegBuy)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Alici <{market.employeesKartof.Peek().id}> {randomBuyVegsCount} kartof aldi");
                            Console.ForegroundColor = ConsoleColor.White;
                            market.dayCash += randomBuyVegsCount * (kartofPrice + 2);
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write($"Market Rating : {(float)market.rating} +  ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(rating);
                            market.rating += rating;
                            Console.ForegroundColor = ConsoleColor.White;
                            switch (market.employeesKartof.Peek().kind)
                            {
                                case 1:
                                    market.employeesXiyar.Enqueue(market.employeesKartof.Peek());
                                    market.employeesKartof.Dequeue(); break;
                                case 2:
                                    market.employeesPomidor.Enqueue(market.employeesKartof.Peek());
                                    market.employeesKartof.Dequeue(); break;
                                case 3:
                                    market.employeesKok.Enqueue(market.employeesKartof.Peek());
                                    market.employeesKartof.Dequeue(); break;

                                case 5:
                                    market.employeesBadimcan.Enqueue(market.employeesKartof.Peek());
                                    market.employeesKartof.Dequeue();
                                    break;
                                default:
                                    Console.WriteLine($"Alici <{market.employeesKartof.Peek().id}> Magazani terk etdi ");
                                    Thread.Sleep(500);
                                    market.employeesKartof.Dequeue();
                                    break;
                            }
                        }
                    }
                }
                if (market.employeesBadimcan.Count > 0)
                {
                    randomBuyVegsCount = random2.Next(1, 4);
                    if (market.badimcan.Count < randomBuyVegsCount)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Alici <{market.employeesXiyar.Peek().id}> istediyi erzaqi tapa bilmediyi ucun magazani terk etdi");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(500);
                        market.employeesBadimcan.Dequeue();
                    }
                    else
                    {
                        for (int i = 0; i < randomBuyVegsCount; i++)
                        {
                            if (market.badimcan.Peek().vegetableSituaion > 8)
                            {
                                market.badimcan.Pop();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Alicinin aldigi terevez curukdur ve o pulu odemeden magazani terk etdi ,rating dushdu {-0.1}");
                                market.rating -= 0.1;
                                Thread.Sleep(1000);
                                Console.ForegroundColor = ConsoleColor.White;
                                market.employeesBadimcan.Dequeue();
                                checkRottenVegBuy = false;
                                break;
                            }
                            else
                            {
                                checkRottenVegBuy = true;
                                market.badimcan.Pop();
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Alici <{market.employeesBadimcan.Peek().id}> {randomBuyVegsCount} badimcan aldi");
                        Console.ForegroundColor = ConsoleColor.White;
                        market.dayCash += randomBuyVegsCount * (badimcanPrice + 2);
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write($"Market Rating : {(float)market.rating} +  ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(rating);
                        market.rating += rating;
                        Console.ForegroundColor = ConsoleColor.White;
                        switch (market.employeesBadimcan.Peek().kind)
                        {
                            case 1:

                                market.employeesXiyar.Enqueue(market.employeesBadimcan.Peek());
                                market.employeesBadimcan.Dequeue();
                                break;
                            case 2:
                                market.employeesPomidor.Enqueue(market.employeesBadimcan.Peek());
                                market.employeesBadimcan.Dequeue(); break;
                            case 3:
                                market.employeesKok.Enqueue(market.employeesBadimcan.Peek());
                                market.employeesBadimcan.Dequeue(); break;
                            case 4:
                                market.employeesKartof.Enqueue(market.employeesBadimcan.Peek());
                                market.employeesBadimcan.Dequeue(); break;

                            default:
                                Console.WriteLine($"Alici <{market.employeesBadimcan.Peek().id}> Magazani terk etdi ");
                                Thread.Sleep(500);
                                market.employeesBadimcan.Dequeue();
                                break;
                        }
                    }
                }

            }
        }

    }
}