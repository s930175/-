using System;

namespace 飛行棋
{
    class Program
    {
        //用靜態字段來模擬全局變量
        public static int[] Maps = new int[100];
        //聲明一個靜態數組用來存儲玩家A和玩家B的座標
        static int[] PlayerPos = new int[2];
        static void Main(string[] args)
        {
            GameShow();
            InitailMap();
            DrawMap();
            Console.ReadKey();
        }

        #region GameShow()
        /// <summary>
        /// 畫遊戲頭
        /// </summary>
        public static void GameShow()
        {
            //Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("********************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("********************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("********************");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("***飛行棋遊戲v1.0***");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("********************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("********************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("********************");
        }
        #endregion

        #region InitailMap()
        /// <summary>
        /// 初始化地圖:將數字(0 1 2 3 4 5)轉化為圖形
        /// </summary>
        public static void InitailMap()
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//幸運輪盤◎
            for (int i = 0; i < luckyturn.Length; i++)
            {
                //將luckyturn的值轉成Map的index,再將其賦值為1
                int index = luckyturn[i];
                Maps[index] = 1;
            }
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷☆
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }
            int[] pause = { 9,27,60,93 };//暫停▲
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }
            int[] timeTunnel = { 20,25,45,63,72,88,90 };//時空隧道卍
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }
        }
        #endregion

        #region DrawMap()
        /// <summary>
        /// 初始化地圖:開始將數字轉成圖形
        /// </summary>
        public static void DrawMap()
        {
            #region 第一橫行
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            //畫完第一橫行後換行
            Console.WriteLine();

            #region 第一豎列
            for (int i = 30; i < 35; i++)
            {
                //畫空格
                for (int j = 0; j < 29; j++)
                {
                    Console.Write("　");
                }
                //畫豎列
                Console.Write(DrawStringMap(i));
                Console.WriteLine();
                
            }
            #endregion
            Console.WriteLine();

            #region 第二橫行
            for (int i = 64; i >= 35; i--)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            Console.WriteLine();

            #region 第二豎列
            for (int i = 65; i <= 69; i++)
            {
                Console.WriteLine(DrawStringMap(i));
            }
            #endregion
            Console.WriteLine();

            #region 第三橫行
            for (int i = 70; i <= 99; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            Console.WriteLine();
        }
        #endregion


        #region DrawStringMap()
        /// <summary>
        /// 畫圖
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string DrawStringMap(int i)
        {
            string str = "";
            //玩家A和玩家B的座標相同，並且都在地圖上(在第一行上)，則畫<>
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[1] == i)
            {
                str="<>";
            }
            else if (PlayerPos[0] == i)
            {
                //注意A和B為全形
                str="Ａ";
            }
            else if (PlayerPos[1] == i)
            {
                str = "Ｂ";
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        str = "□" ;
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        str = "◎";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "☆";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "卍";
                        break;
                }
            }
            return str;
        }
        #endregion
    }
}
