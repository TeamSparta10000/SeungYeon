using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            DisplayTown();
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
            Console.WriteLine("4. 체력회복");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = Program.CheckValidInput(1, 4);
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
                case 4:
                    Recovery(GameData.player);
                    break;
                default: Console.WriteLine("잘못 입력하셨어여");
                    DisplayTown();
                    break;
            }
        }

        public static void Recovery(Character player)
        {
            Console.Clear();

            if (player.Hp < 100)
            {
                player.Hp += 20;
                Console.WriteLine($"체력이 20 회복되었습니다.\n현재 체력: {player.Hp}");
            }
            else if (player.Hp >= 100)
            {
                Console.WriteLine("체력이 충분합니다");
            }
            Console.WriteLine("0: 돌아가기, 1: 상태보기");

            int input = Program.CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayTown();
                    break;
                case 1:
                    DisplayMyInfo();
                    break;
                default:
                    Console.WriteLine("잘못 입력하셨어여");
                    Recovery(GameData.player);
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
                default:
                    Console.WriteLine("잘못 입력하셨어여");
                    DisplayMyInfo();
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
                default:
                    Console.WriteLine("잘못 입력하셨어여");
                    DisplayInventory();
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

        public static void RandomMonster(out Monster monster)
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
            Console.Clear();
            while (true)
            {

                Monster monster;
                Console.WriteLine("사냥터 입장 완료!");
                RandomMonster(out monster);

                Console.WriteLine("[1] 기본공격");
                Console.WriteLine("[2] 도망가기");
                Console.WriteLine("[3] 스킬선택");




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
                else if (input == "3")
                {

                    Skille(player, monster);
                }

            }


        }
        static void Skille(Character player, Monster monster) //직업별 스킬구현
        {

            if (player.Job == "Knight") //기사 직업 스킬 구현 
            {
                Console.WriteLine("스킬: 쫓아가서 쥐어 박기! - MP 10 고정피해 40");
                Console.WriteLine("스킬: 세트 궁극기! 돌려 내려찍기! - MP 15 고정피해 60");
                Console.WriteLine("돌아가기: 0, 스킬: 1, 스킬: 2");

                int input3 = Program.CheckValidInput(0, 2); //?

                if (input3 == 0)
                {
                    if (player.Mp >= 15)
                    {
                        Console.Clear();
                        Console.WriteLine("돌아가기 \n 아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                }
                else if (input3 == 1)
                {
                    if (player.Mp >= 15)
                    {
                        Console.Clear();
                        Console.WriteLine($"쥐어 박았다\n상대 HP{monster.hp}\n플레이어 HP{player.Hp} MP{player.Mp}");
                        player.Mp -= 10;
                        monster.hp -= 40;

                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("마나부족");
                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                }
                else if (input3 == 2)
                {
                    if (player.Mp >= 15)
                    {
                        Console.Clear();
                        Console.WriteLine($"내려찍었다\n상대 HP{monster.hp}\n플레이어 HP{player.Hp} MP{player.Mp}");
                        player.Mp -= 15;
                        monster.hp -= 60;

                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("마나부족");
                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                }
            }
            else if (player.Job == "Mage") //마법사 직업 스킬 구현 
            {
                Console.WriteLine("스킬: 마법으로 불 붙이기! - MP 10 고정피해 50");
                Console.WriteLine("스킬: 물대포 발사하기! - MP 15 고정피해 50");
                Console.WriteLine("돌아가기: 0, 스킬: 1, 스킬: 2");

                int input4 = Program.CheckValidInput(0, 2);

                if (input4 == 0)
                {
                    if (player.Mp >= 15)
                    {
                        Console.Clear();
                        Console.WriteLine("돌아가기 \n 아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                }
                else if (input4 == 1)
                {
                    if (player.Mp >= 15)
                    {
                        Console.Clear();
                        Console.WriteLine($"순식간에 타올랐다\n상대 HP{monster.hp}\n플레이어 HP{player.Hp} MP{player.Mp}");
                        player.Mp -= 10;
                        monster.hp -= 40;

                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("마나부족");
                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                }
                else if (input4 == 2)
                {
                    if (player.Mp >= 15)
                    {
                        Console.Clear();
                        Console.WriteLine($"적은 물에 허우적 거렸다.\n상대 HP{monster.hp}\n플레이어 HP{player.Hp} MP{player.Mp}");
                        player.Mp -= 15;
                        monster.hp -= 60;

                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("마나부족");
                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                }   
            }
            else if (player.Job == "Archer") //아처 직업 스킬 구현 
            {

                Console.WriteLine("스킬: 활 오지게 날리기! - MP 10 고정피해 50");
                Console.WriteLine("스킬: 불화살 발사하기! - MP 15 고정피해 50");
                Console.WriteLine("돌아가기: 0, 스킬: 1, 스킬: 2");

                int input4 = Program.CheckValidInput(0, 2);

                if (input4 == 0)
                {                        
                        Console.Clear();
                        Console.WriteLine("돌아가기 \n 아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster); 
                }
                else if (input4 == 1)
                {
                    if (player.Mp >= 15)
                    {
                        Console.Clear();
                        Console.WriteLine($"순식간에 타올랐다\n상대 HP{monster.hp}\n플레이어 HP{player.Hp} MP{player.Mp}");
                        player.Mp -= 15;
                        monster.hp -= 40;

                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("마나부족");
                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                }
                else if (input4 == 2)
                {
                    if (player.Mp >= 15)
                    {
                        Console.Clear();
                        Console.WriteLine($"적은 물에 허우적 거렸다.\n상대 HP{monster.hp}\n플레이어 HP{player.Hp} MP{player.Mp}");
                        player.Mp -= 15;
                        monster.hp -= 60;

                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("마나부족");
                        Console.WriteLine("아무키나 눌러주세요");
                        Console.ReadLine();
                        Fight(player, monster);
                    }
                }


            }




        }

            public static void Fight(Character player, Monster monster)
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
                        Console.WriteLine("[3] 스킬선택");


                        string input = Console.ReadLine();

                        if (input == "1")
                        {
                            Console.Clear();
                            Console.WriteLine("공격을 했다!\n아무키를 입력하세요");
                            Console.WriteLine($"몬스터의 HP 상태: {monster.hp}");

                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("공격을 당했다");
                            Console.WriteLine($"플레이어의 HP 상태: {player.Hp}");
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
                        else if (input == "3")
                        {

                            Skille(player, monster);
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
                else
                {
                    End();
                    Console.WriteLine("잘못 선택하셨어요");
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
                else
                {
                    End2();
                    Console.WriteLine("잘못 선택하셨어요");
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

