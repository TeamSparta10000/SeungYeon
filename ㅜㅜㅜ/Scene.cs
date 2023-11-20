using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dungeon_Adventure;

namespace Dungeon_Adventure
{
    public class Scene
    {
        public static void DisplayGameStart()
        {
            Console.Clear();
            Console.WriteLine(" ________      ___  ___      ________      _______       ________      ________      ________           ________      ________      ___      ___  _______       ________       _________    ___  ___      ________      _______      ");
            Console.WriteLine("|\\   ___ \\    |\\  \\|\\  \\    |\\   ____\\    |\\  ___ \\     |\\   __  \\    |\\   __  \\    |\\   ___  \\        |\\   __  \\    |\\   ___ \\    |\\  \\    /  /||\\  ___ \\     |\\   ___  \\    |\\___   ___\\ |\\  \\|\\  \\    |\\   __  \\    |\\  ___ \\     ");
            Console.WriteLine("\\ \\  \\_|\\ \\   \\ \\  \\\\\\  \\   \\ \\  \\___|    \\ \\   __/|    \\ \\  \\|\\  \\   \\ \\  \\|\\  \\   \\ \\  \\\\ \\  \\       \\ \\  \\|\\  \\   \\ \\  \\_|\\ \\   \\ \\  \\  /  / /\\ \\   __/|    \\ \\  \\\\ \\  \\   \\|___ \\  \\_| \\ \\  \\\\\\  \\   \\ \\  \\|\\  \\   \\ \\   __/|    ");
            Console.WriteLine(" \\ \\  \\ \\\\ \\   \\ \\  \\\\\\  \\   \\ \\  \\  ___   \\ \\  \\_|/__   \\ \\   __  \\   \\ \\  \\\\\\  \\   \\ \\  \\\\ \\  \\       \\ \\   __  \\   \\ \\  \\ \\\\ \\   \\ \\  \\/  / /  \\ \\  \\_|/__   \\ \\  \\\\ \\  \\       \\ \\  \\   \\ \\  \\\\\\  \\   \\ \\   _  _\\   \\ \\  \\_|/__  ");
            Console.WriteLine("  \\ \\  \\_\\\\ \\   \\ \\  \\\\\\  \\   \\ \\  \\|\\  \\   \\ \\  \\_|\\ \\   \\ \\  \\ \\  \\   \\ \\  \\\\\\  \\   \\ \\  \\\\ \\  \\       \\ \\  \\ \\  \\   \\ \\  \\_\\\\ \\   \\ \\    / /    \\ \\  \\_|\\ \\   \\ \\  \\\\ \\  \\       \\ \\  \\   \\ \\  \\\\\\  \\   \\ \\  \\\\  \\|   \\ \\  \\_|\\ \\ ");
            Console.WriteLine("   \\ \\_______\\   \\ \\_______\\   \\ \\_______\\   \\ \\_______\\   \\ \\__\\ \\__\\   \\ \\_______\\   \\ \\__\\\\ \\__\\       \\ \\__\\ \\__\\   \\ \\_______\\   \\ \\__/ /      \\ \\_______\\   \\ \\__\\\\ \\__\\       \\ \\__\\   \\ \\_______\\   \\ \\__\\\\ _\\    \\ \\_______\\");
            Console.WriteLine("   \\|_______|    \\|_______|    \\|_______|    \\|_______|    \\|__|\\|__|    \\|_______|    \\|__| \\|__|        \\|__|\\|__|    \\|_______|    \\|__|/        \\|_______|    \\|__| \\|__|        \\|__|    \\|_______|    \\|__|\\|__|    \\|_______|");
            Console.WriteLine("=========================================================================================================================================================================================================================================================");
            Console.WriteLine("                                                                                                               PRESS ANYKEY TO START                                                                                                                     ");
            Console.WriteLine("=========================================================================================================================================================================================================================================================");
            Console.ReadKey();



        }
        public static void DisplayTown()
        {
            Console.Clear();

            Console.WriteLine("초보던전 입구마을에 오신" + (GameData.player.Name) + "님 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전에 점검을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 던전입장");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = Program.CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    Scene.DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    Dungeon(GameData.player);
                    break;
            }
        }
        public static void DisplayMyInfo()
        {
            Console.Clear();

            Program.ShowHighlightText("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Program.PrintTextWithHighlights($"Lv.", GameData.player.Level.ToString("00"));
            Console.WriteLine($"{GameData.player.Name}({GameData.player.Job})");

            int bonusAtk = GameData.GetSumBonusAtk();
            int bonusDef = GameData.GetSumBonusDef();
            int bonusHp = GameData.GetSumBonusHp();
            int bonusMp = GameData.GetSumBonusMp();

            Program.PrintTextWithHighlights($"Atk :", (GameData.player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? string.Format("(+{0})", bonusAtk) : "");
            Program.PrintTextWithHighlights($"Def :", (GameData.player.Def + bonusDef).ToString(), bonusDef > 0 ? string.Format("(+{0})", bonusDef) : "");
            Program.PrintTextWithHighlights($"Hp :", (GameData.player.Hp + bonusHp).ToString(), bonusHp > 0 ? string.Format("(+{0})", bonusHp) : "");
            Program.PrintTextWithHighlights($"Mp :", (GameData.player.Mp + bonusMp).ToString(), bonusMp > 0 ? string.Format("(+{0})", bonusMp) : "");
            Console.WriteLine($"Gold : {GameData.player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = Program.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayTown();
                    break;
            }
        }
        public static void DisplayInventory()
        {
            Console.Clear();

            Program.ShowHighlightText("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                GameData.items[i].PrintItemDescription();
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");

            int input = Program.CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayTown();
                    break;
                case 1:
                    EquipMenu();
                    break;
            }
        }
        public static void EquipMenu()
        {
            Console.Clear();

            Program.ShowHighlightText("인벤토리 - 장착관리");
            Console.WriteLine("보유중인 아이템을 장착하거나 해제할수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                GameData.items[i].PrintItemDescription(true, i + 1);
            }
            Console.WriteLine();
            Console.WriteLine("0.나가기");
            int keyinput = Program.CheckValidInput(0, Item.ItemCnt);
            switch (keyinput)
            {
                case 0:
                    DisplayInventory();
                    break;
                default:
                    Program.ToggleEqupStatus(keyinput - 1);
                    EquipMenu();
                    break;
            }
        }

        enum MonsterType //몬스터 종류 
        {
            greenslime = 0,
            redslime = 1,
            blueslime = 2,
        }

        public struct Monster //몬스터세팅  
        {
            public int hp;
            public int attack;
        }

        public static void RandomMonster(out Monster monster) //out 어캐 쓰는 거야
        {   //랜덤몬스터 
            Random random = new Random();
            int Rmonster = random.Next(0, 2);

            monster = new Monster(); //?? 

            switch (Rmonster)
            {
                case (int)MonsterType.greenslime:
                    Console.WriteLine("그린슬라임 등장");
                    monster.hp = 200;
                    monster.attack = 11;
                    string skilleGreen = "그린 박치기!";
                    break;
                case (int)MonsterType.redslime:
                    Console.WriteLine("레드슬라임 등장");
                    monster.hp = 250;
                    monster.attack = 12;
                    string skilleRed = "레드 파이어붐!";
                    break;
                case (int)MonsterType.blueslime:
                    Console.WriteLine("블루슬라임 등장");
                    monster.hp = 300;
                    monster.attack = 18;
                    string skilleBlue = "블루 물대포!";
                    break;
            }
        }

        static void Dungeon(Character player) //던전씬 
        {
            while (true)
            {

                Monster monster;
                Console.WriteLine("사냥터 입장 완료!");
                RandomMonster(out monster);

                Console.WriteLine("[1] 싸우기");
                Console.WriteLine("[2] 도망가기");




                string input = Console.ReadLine();


                if (input == "1")
                {
                    Fight(player, monster);
                }
                else if (input == "2")
                {
                    //도망 
                    Random rand = new Random();
                    int Rmonster = rand.Next(0, 100);

                    if (Rmonster < 33)
                    {
                        Console.WriteLine("성공적으로 도망쳤어요");
                        Dungeon(player);
                    }
                    else
                    {
                        Fight(player, monster);
                    }
                    break;
                }

            }

        }

        static void Fight(Character player, Monster monster)
        { //ref가 뭔지 알아봐야 할 듯 
            while (true)
            {
                monster.hp -= player.Atk;
                player.Hp -= monster.attack;


                if (monster.hp <= 0)
                {
                    Console.WriteLine("클리어");
                    End();
                    break;
                }
                else if (player.Hp <= 0)
                {
                    Console.WriteLine("죽어버렸숨다");
                    End2();
                    break;
                }
                else if (player.Hp != 0)
                {
                    Console.Clear(); //전 글자 기록들 지워주는 청소도구! 
                    Console.WriteLine("다음 선택은?");
                    Console.WriteLine("[1] 싸우기");
                    Console.WriteLine("[2] 도망가기");

                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("공격을 했다!\n아무키를 입력하세요");
                    Console.WriteLine($"몬스터의 HP 상태: {monster.hp}");

                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("공격을 당했다");
                    Console.WriteLine($"플레이어의 HP 상태: {player.Hp}");


                    Console.WriteLine("[1] 싸우기");
                    Console.WriteLine("[2] 도망가기");




                    string input = Console.ReadLine();

                    if (monster.hp <= 0)
                    {
                        End();
                        break;
                    }
                    else if (input == "2")
                    {
                        //도망 
                        Random rand = new Random();
                        int Rmonster = rand.Next(0, 100);

                        if (Rmonster < 33)
                        {
                            Console.WriteLine("성공적으로 도망쳤어요");
                            Dungeon(player);
                        }
                        else
                        {
                            Fight(player, monster);
                        }

                        break;
                    }
                }

            }

        }


        public static void End() //어쨌든 클리어씬! 
        {
            Console.Clear();

            Console.WriteLine("전투에서 승리하였습니다");
            Console.WriteLine("돌아가려면 0번");

            string input = Console.ReadLine();
            if (input == "0")
            {
                DisplayGameStart();
            }

        }

        public static void End2() //어쨌든 클리어씬! 
        {
            Console.Clear();

            Console.WriteLine("전투에서 패배하였습니다");
            Console.WriteLine("돌아가려면 0번");

            string input = Console.ReadLine();
            if (input == "0")
            {
                DisplayGameStart();
            }

        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


}

