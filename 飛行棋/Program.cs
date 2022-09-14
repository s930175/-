using System;

namespace 飛行棋
{
    class Program
    {
        //用靜態字段來模擬全局變量
        public static int[] Maps = new int[100];
        //聲明一個靜態數組用來存儲玩家A和玩家B的座標
        static int[] PlayerPos = new int[2];
        //存儲兩個玩家的姓名
        static string[] PlayerNames = new string[2];
        //判斷是否暫停一回合
        static bool[] Flags = new bool[2];//默認情況下都是false(正常)
        static void Main(string[] args)
        {
            GameShow();
            #region 輸入玩家姓名
            Console.WriteLine("請輸入玩家A姓名");
            PlayerNames[0] = Console.ReadLine();
            while (PlayerNames[0] == "")
            {
                Console.WriteLine("玩家A的姓名不能為空，請重新輸入玩家A姓名");
                PlayerNames[0] = Console.ReadLine();
            }

            Console.WriteLine("請輸入玩家B姓名");
            PlayerNames[1] = Console.ReadLine();
            while (PlayerNames[1] == "" || PlayerNames[1] == PlayerNames[0])
            {
                if (PlayerNames[1] == "")
                {
                    Console.WriteLine("玩家B姓名不能為空，請重新輸入玩家B姓名");
                    PlayerNames[1] = Console.ReadLine();       
                }
                else
                {
                    Console.WriteLine("玩家A和B姓名不能相同，請重新輸入玩家B姓名");
                    PlayerNames[1] = Console.ReadLine();
                }
            }
            #endregion 
            //玩家姓名輸入完成後，應該先清屏
            Console.Clear();
            GameShow();
            Console.WriteLine("{0}的士兵用A表示", PlayerNames[0]);
            Console.WriteLine("{0}的士兵用B表示", PlayerNames[1]);
            //在畫地圖前，首先應該先初始化地圖
            InitailMap();
            DrawMap();

            //當玩家A和玩家B皆不再終點時，兩個玩家不停去玩遊戲
            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
            {
                if (Flags[0] == false)
                {
                    PlayGame(0);
                }
                //讓下一回合可以正常玩
                else
                {
                    Flags[0] = false;
                }
                if (PlayerPos[0] >= 99)
                {
                    Console.WriteLine("玩家{0}獲得勝利", PlayerNames[0]);
                    break;
                }
                if (Flags[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flags[1] = false;
                }
                if (PlayerPos[1] >= 99)
                {
                    Console.WriteLine("玩家{0}獲得勝利", PlayerNames[1]);
                    break;
                }
            }

            Win();
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
        /// 初始化地圖1:將數字(0 1 2 3 4 5)存進Maps陣列裡
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
        /// 初始化地圖2:開始將數字轉成圖形
        /// </summary>
        public static void DrawMap()
        {
            Console.WriteLine("圖例:幸運輪盤◎   地雷:☆   暫停:▲   時空隧道:卍");
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

        #region PlayGame()
        /// <summary>
        /// 玩遊戲
        /// </summary>
        public static void PlayGame(int playerNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);//產生1~6隨機數
            Console.WriteLine("{0}按任意鍵開始擲骰子", PlayerNames[playerNumber]);
            //Console.ReadKey()傳參true可以不顯示按下的任意鍵，默認false
            Console.ReadKey(true);
            Console.WriteLine("玩家{0}擲出了{1}", PlayerNames[playerNumber], rNumber);
            PlayerPos[playerNumber] += rNumber;
            ChangePos();
            //Console.ReadKey(true);
            Console.WriteLine("玩家{0}按任意鍵開始行動", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("玩家{0}行動完了", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            //狀況:玩家A踩到玩家B 方塊 幸運輪盤 地雷 暫停 時空隧道
            //玩家A踩到玩家B
            if (PlayerPos[playerNumber] == PlayerPos[1- playerNumber])
            {
                Console.WriteLine("玩家{0}踩到了玩家{1}，玩家{2}退六格", PlayerNames[playerNumber], PlayerNames[1- playerNumber], PlayerNames[1- playerNumber]);
                PlayerPos[1- playerNumber] -= 6;
                ChangePos();
                Console.ReadKey(true);
            }
            else//踩到了關卡
            {
                switch (Maps[PlayerPos[playerNumber]])
                {
                    case 0:
                        Console.WriteLine("玩家{0}踩到了方塊，甚麼事都沒發生", PlayerNames[playerNumber]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("玩家{0}踩到了幸運輪盤，請選擇 1--交換位置  2--轟炸對方", PlayerNames[playerNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("玩家{0}選擇跟玩家{1}交換位置", PlayerNames[playerNumber], PlayerNames[1- playerNumber]);
                                Console.ReadKey(true);
                                int temp = PlayerPos[playerNumber];
                                PlayerPos[playerNumber] = PlayerPos[1- playerNumber];
                                PlayerPos[playerNumber] = temp;
                                Console.WriteLine("交換完成，按任意鍵繼續遊戲");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("玩家{0}選擇轟炸玩家{1},玩家{2}退六格", PlayerNames[playerNumber], PlayerNames[1- playerNumber], PlayerNames[1- playerNumber]);
                                PlayerPos[1 - playerNumber] -= 6;
                                ChangePos();
                                Console.WriteLine("玩家{0}退了六格", PlayerNames[1- playerNumber]);
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("只能輸入1或2  1--交換位置  2--轟炸對方");
                                input = Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩到了地雷，退六格", PlayerNames[playerNumber]);
                        PlayerPos[playerNumber] -= 6;
                        ChangePos();
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Console.WriteLine("玩家{0}踩到了暫停，暫停一回合", PlayerNames[playerNumber]);
                        Flags[playerNumber] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("玩家{0}踩到了時空隧道，前進十格", PlayerNames[playerNumber]);
                        PlayerPos[playerNumber] += 10;
                        ChangePos();
                        Console.ReadKey(true);
                        break;
                }

            }
            //ChangePos();
            Console.Clear();
            DrawMap();
        }
        #endregion

        #region ChangePos()
        /// <summary>
        /// 判斷座標，當玩家座標被改變時調用
        /// </summary>
        public static void ChangePos()
        {
            if (PlayerPos[0] < 0)
            {
                PlayerPos[0] = 0;
            }
            if (PlayerPos[0] > 99)
            {
                PlayerPos[0] = 99;
            }
            if (PlayerPos[1] < 0)
            {
                PlayerPos[1] = 0;
            }
            if (PlayerPos[1] > 99)
            {
                PlayerPos[1] = 99;
            }
        }
        #endregion

        public static void Win()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                    ■                 ■■■");
            Console.WriteLine("■■■■  ■  ■            ■■  ■               ■  ");
            Console.WriteLine("■    ■  ■  ■                  ■           ■  ■");
            Console.WriteLine("■    ■ ■■■■■■       ■■■■■■■     ■  ■");
            Console.WriteLine("■■■■ ■   ■               ■ ■ ■        ■  ■   ");
            Console.WriteLine("■    ■      ■             ■   ■  ■       ■  ■");
            Console.WriteLine("■    ■ ■■■■■■      ■     ■   ■      ■  ■");
            Console.WriteLine("■■■■      ■          ■      ■     ■    ■  ■");
            Console.WriteLine("■    ■      ■                  ■               ■");
            Console.WriteLine("■    ■      ■                  ■               ■");
            Console.WriteLine("■    ■      ■                  ■               ■");
            Console.WriteLine("■   ■■ ■■■■■■■           ■             ■■");
        }
    }
}
