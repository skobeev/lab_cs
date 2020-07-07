using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace KurRabV35
{
    class Ferm
    {
        public int fKey { set; get; }
        public string fName { set; get; }
        public string fStreet { set; get; }
        public Ferm(int k, string n, string s)
        {
            fKey = k;
            fName = n;
            fStreet = s;
        }
    }

    class Product
    {
        public int prKey { set; get; }
        public string prName { set; get; }
        public string prQuan { set; get; }
        public double prCount { set; get; }
        public double prPrice { set; get;}
        public Product(int k, string n, string q, double c, double p)
        {
            prKey = k;
            prName = n;
            prQuan = q;
            prCount = c;
            prPrice = p;
        }
    }
    class Potr
    {
        public int potKey { set; get; }
        public string potName { set; get; }
        public int potPrice { set; get; }
        public Potr(int k, string n, int p)
        {
            potKey = k;
            potName = n;
            potPrice = p;
        }
    }

    class Program
    {
        static void Frame()
        {
            Console.WriteLine("ВВЕДИТЕ НОМЕР КОМАНДЫ");
            Console.WriteLine("1 - Продукция, которую производят фермеры области");
            Console.WriteLine("2 - Потребности фермеров в производстве");
            Console.WriteLine("3 - Количество заданной продукции");
            Console.WriteLine("4 - Прибыль фермеров по каждому виду продукции");
            Console.WriteLine("5 - Требуемый кредит");
            Console.WriteLine("6 - Разница между кредитом и прибылью");
            Console.WriteLine("7 - Фермеры, Товары, Цены, Количество");
            Console.WriteLine("8 - Добавить продукт");
            Console.WriteLine("9 - Удалить продукт");
            Console.WriteLine("10 - Выход из программы");
        }
        static void File_Load(string[] path,int n, int m)
        {
            if (n ==0)
            {
                StreamReader File = new StreamReader(path[0], Encoding.Default);
                string str;
                while((str=File.ReadLine())!=null)
                {
                    string[]ms= new string[m];
                    ms = str.Split(';');
                    lst1.Add(new Ferm(Convert.ToInt32(ms[0]), ms[1], ms[2]));

                }
                File.Close();
            }
            if (n==1)
            {
                StreamReader File = new StreamReader(path[n], Encoding.Default);
                string str;
                while ((str = File.ReadLine()) != null)
                {
                    string[] ms = new string[m];
                    ms = str.Split(';');
                    lst2.Add(new Product(Convert.ToInt32(ms[0]), ms[1], ms[2], Convert.ToDouble(ms[3]),Convert.ToDouble(ms[4])));
                }
                File.Close();
            }
            if(n==2)
            {
                StreamReader File = new StreamReader(path[n], Encoding.Default);
                string str;
                while ((str = File.ReadLine()) != null)
                {
                    string[] ms = new string[m];
                    ms = str.Split(';');
                    lst3.Add(new Potr(Convert.ToInt32(ms[0]), ms[1], Convert.ToInt32(ms[2])));
                }
                File.Close();
            }
            
        }
        static List<Ferm> lst1 = new List<Ferm>();
        static List<Product> lst2 = new List<Product>();
        static List<Potr> lst3 = new List<Potr>();
        static string[] path = { "Ferm.txt", "Product.txt", "Potr.txt" };
        static void Main(string[] args)
        {
            File_Load(path, 0, 3);
            File_Load(path, 1, 5);
            File_Load(path, 2, 3);
            bool f = true;
            while(f)
            {
                int n;
                Frame();
                if (!int.TryParse(Console.ReadLine(),out n)||n>10||n<1)
                {
                    Console.WriteLine("Неверная команда. Повторите попытку");
                }
                switch(n)
                {
                    case 1:
                        {
                            //var fermer = lst1.Select(a => a);
                            foreach (Ferm a in lst1)
                            {
                                Console.WriteLine("Продукция фермера " + a.fName);
                                var temp = lst2.Where(b => b.prKey == a.fKey);
                                foreach (var b in temp)
                                {
                                    Console.WriteLine(b.prName);
                                }
                            }          
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case 2:
                        foreach(Ferm a in lst1)
                        {
                            Console.WriteLine("Потребности фермера "+a.fName);
                            var temp = lst3.Where(b => b.potKey == a.fKey);
                            foreach(var b in temp)
                            {
                                Console.WriteLine(b.potName);
                            }
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 3:
                        {
                            Console.Write("Введите искомый продукт: ");
                            string str =" "+Console.ReadLine();
                            var temp = lst2.Where(a => a.prName == str);
                            double sum = 0;
                            foreach(var a in temp)
                            {
                                sum += a.prCount;
                            }
                            Console.WriteLine("Всего производится "+sum);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        
                    case 4:
                        {
                            foreach(Ferm a in lst1)
                            {
                                var temp = lst2.Where(b => b.prKey == a.fKey);
                                Console.WriteLine("Прибыль фермера "+a.fName);
                                foreach(Product b in temp)
                                {
                                    Console.Write((b.prName).PadRight(15));
                                    Console.WriteLine(b.prCount*b.prPrice);
                                }
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        
                    case 5:
                        {
                            foreach(Ferm a in lst1)
                            {
                                Console.WriteLine("Кредит фермера " + a.fName);
                                var temp = lst3.Where(b => b.potKey == a.fKey && b.potName == " Кредит");
                                foreach(var b in temp)
                                Console.WriteLine(b.potPrice);
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case 6:
                        {
                            foreach(Ferm a in lst1)
                            {
                                Console.WriteLine("Фермер "+a.fName);
                                var temp = lst3.Where(b => b.potKey == a.fKey && b.potName == " Кредит");
                                var temp1 = lst2.Where(b => b.prKey == a.fKey);
                                double sum = 0;
                                double kred = 0;
                                foreach (var b in temp1)
                                {
                                sum += b.prCount * b.prPrice;
                                }
                                foreach (var b in temp)
                                {
                                kred += b.potPrice;
                                }
                                Console.WriteLine(sum - kred);
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case 7:
                        {
                            var qr =lst1.Join(lst2,w => w.fKey, b => b.prKey, (w, b) => new { w.fName, b.prName, b.prCount, b.prPrice });
                            Console.Write("Фермер".PadRight(15)+"Товар".PadRight(15)+"Количество".PadRight(15) + "Цена".PadRight(15)+"\n");
                            foreach(var a in qr)
                            {
                                Console.WriteLine(a.fName.PadRight(15) + a.prName.PadRight(15) + Convert.ToString(a.prCount).PadRight(15) + Convert.ToString(a.prPrice).PadRight(15));
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case 8:
                        {
                            int i = 0;
                            Console.WriteLine("Введите номер фермера, которому нужно добавить товар:");
                            foreach(Ferm a in lst1)
                                Console.WriteLine("{0} - {1}",++i,a.fName);
                            if(!int.TryParse(Console.ReadLine(),out i)||i < 0||i>lst1.Count())
                            {
                                Console.WriteLine("Неверная команда");
                                break;
                            }
                            Console.Write("Введите название продукта: ");
                            string name =" "+ Console.ReadLine();
                            Console.Write("Введите качество товара: ");
                            string quan =Console.ReadLine();
                            Console.Write("Введите цену: ");
                            double price = double.Parse(Console.ReadLine());
                            Console.Write("Введите количество: ");
                            double count = double.Parse(Console.ReadLine());
                            lst2.Add(new Product(i, name, quan, count, price));
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case 9:
                        {
                            int i = 0;
                            Console.WriteLine("Введите номер фермера, у котрого нужно удалить товар:");
                            foreach (Ferm a in lst1)
                                Console.WriteLine("{0} - {1}", ++i, a.fName);
                            if (!int.TryParse(Console.ReadLine(), out i) || i < 0 || i > lst1.Count())
                            {
                                Console.WriteLine("Неверная команда");
                                break;
                            }
                            Console.Write("Введите название товара: "  );
                            string name = " "+Console.ReadLine();
                            lst2.RemoveAll(c => c.prKey == i && c.prName == name);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case 10:
                        {
                            f = false;
                            break;
                        }
                           
                }
            }
        }
    }
}
