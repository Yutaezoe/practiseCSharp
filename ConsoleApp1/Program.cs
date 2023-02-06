using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Program program = new Program();

            program.LinqTest();
        }

        private void LinqTest()
        {

            List<Home> home = new List<Home>()
            {
                new Home(1,"a"),
                new Home(2,"b"),
                new Home(3,"c"),
                new Home(4,"d"),
                new Home(5,"e"),
            };


            Console.WriteLine("検索");
            // 条件検索したいとき
            List<Home> homesFinds = home.Where(x => x.roomId >= 2).ToList();

            foreach (Home eachHome in homesFinds)
            {
                Console.WriteLine(eachHome.roomId);
            }

            Console.WriteLine("複数条件検索");
            List<Home> homesFindMultiConditions = home.Where(x => x.roomId >= 2 & x.name =="b").ToList();

            foreach (Home eachHome in homesFindMultiConditions)
            {
                Console.WriteLine(eachHome.roomId);
            }


            Console.WriteLine("書き換え");
            /// 書き換えて戻り値を得る場合はこんな書き方するらしい。
            List<(int, string)> homesSelect = 
            home.Where(x => x.roomId >= 3).Select(x => (x.roomId = 1 , x.name = "a")).ToList();


            foreach ((int,string) eachHome in homesSelect)
            {
                Console.WriteLine(eachHome.Item1);
                Console.WriteLine(eachHome.Item2);
            }


            // メモリも書き換わることがわかる。ぽいんたみたいなかんじ
            Console.WriteLine("書き換え後のList");
            foreach (Home eachHome in home)
            {
                Console.WriteLine(eachHome.roomId);
                Console.WriteLine(eachHome.name);
            }


            // グループ化
            // ToList()することでListにキャストできるよ

            Console.WriteLine("グループ化");
            List<Home> homeGroup = new List<Home>()
            {
                new Home(1,"a"),
                new Home(5,"b"),
                new Home(3,"c"),
                new Home(1,"d"),
                new Home(3,"e"),
            };


            IEnumerable<IGrouping<int,Home>> homeGroups = homeGroup.GroupBy(x => x.roomId);
            Console.WriteLine(homeGroups.GetType());

            foreach (IEnumerable<Home> homes in homeGroups)
            {
                foreach (Home eachHome in homes)
                {
                    Console.WriteLine(eachHome.roomId);
                    Console.WriteLine(eachHome.name);
                }
            }

            // ならびかえもできるよ
            Console.WriteLine("ならびかえ昇順");
            IEnumerable<Home> orderByHomeTable = homeGroup.OrderBy(x => x.roomId);

            foreach (Home homes in orderByHomeTable)
            {
                Console.WriteLine(homes.roomId);
                Console.WriteLine(homes.name);
            }

            Console.WriteLine("ならびかえ降順");
            IEnumerable<Home> orderByDescendingHomeTable = homeGroup.OrderByDescending(x => x.roomId);

            foreach (Home homes in orderByDescendingHomeTable)
            {
                Console.WriteLine(homes.roomId);
                Console.WriteLine(homes.name);
            }


            Console.ReadLine(); // Enterが押されるまで待機する
        }

    }
}
