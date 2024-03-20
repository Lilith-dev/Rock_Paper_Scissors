using System;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Homeworks
{
    class MainClass
    {
        enum gameChoice
        {
            Rock = 1,
            Scissors = 2,
            Paper = 3
        }

        private static void writeColorLine(int lenth = 1, ConsoleColor foregroundColor = ConsoleColor.White, bool endLine = true, string symbol = "▀")
        {
            // "██" "▀" "▄"
            Console.ForegroundColor = foregroundColor;
            for (int i = 0; i < lenth; i++)
            {
                Console.Write(symbol);
            }
            if (endLine)
            {
                Console.Write("\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void writeBorderedText(string text, ConsoleColor borderColor, int marging = 4, char margingSymbol = ' ')
        {
            int maxLineLength = 0;
            string[] lines = text.Replace("\r", "").Split('\n');

            foreach (string line in lines)
            {
                if (line.Length > maxLineLength)
                {
                    maxLineLength = line.Length;

                }
            }

            writeColorLine(maxLineLength + marging * 2 + 4, borderColor, true, "▄");

            foreach (string line in lines)
            {
                writeColorLine(1, borderColor, false, "██");
                for (int i = 0; i < marging; i++)
                {
                    Console.Write(margingSymbol);
                }
                Console.Write(line);
                for (int i = 0; i < marging + (maxLineLength - line.Length); i++)
                {
                    Console.Write(margingSymbol);
                }
                writeColorLine(1, borderColor, true, "██");
            }

            writeColorLine(maxLineLength + marging * 2 + 4, borderColor);

        }

        private static string concatDisplayChoices(string choiceLeft, string choiceRight)
        {

            string[] linesChoiceLeft = choiceLeft.Replace("\r", "").Split('\n');
            string[] linesChoiceRight = choiceRight.Replace("\r", "").Split('\n');
            string result = "";

            int maxLLength = 0;
            int maxLRLength = 0;
            foreach (string line in linesChoiceLeft)
            {
                if (line.Length > maxLLength)
                {
                    maxLLength = line.Length;

                }
            }
            foreach (string line in linesChoiceRight)
            {
                if (line.Length > maxLRLength)
                {
                    maxLRLength = line.Length;

                }
            }


            for (int i = 0; i < linesChoiceLeft.Length; i++)
            {
                result += linesChoiceLeft[i];
                result += new string(' ', maxLLength - linesChoiceLeft[i].Length);
                if (i == 2)
                {
                    result += " VS ";
                }
                else
                {
                    result += "    ";
                }
                char[] charArray = linesChoiceRight[i].ToCharArray();
                Array.Reverse(charArray);
                result += new string(' ', maxLRLength - linesChoiceRight[i].Length);
                result += new string(charArray).Replace(")", "}").Replace("(", ")").Replace("}", "(");
                result += "\n";
            }
            return result;
        }

        public static void Homework_Rock_Paper_Scissors()
        {

            string nickname = "";
            int age = 0;
            int roundsPlayed = 0;
            int victories = 0;
            int countRoundsPerFight = 3;
            Random random = new Random();

            string[] loosePhrases = new string[3]
            {
                "Ха! Лузер!",
                "Не плач наступного разу",
                "Щож, це мабуть найкраще, що ти зміг"
            };

            string[] winPhrases = new string[3]
           {
                "Красава, розірвав цю падлюку!",
                "Ти диви, переміг і не забарився!",
                "Дурний переміг ще дурнішого! Це жарт, жарт заспокойся)"
           };

            writeBorderedText("Привіт! Вітаю тебе у грі 'Камінь, Ножиці, Папір!", ConsoleColor.Blue);
            while (string.IsNullOrWhiteSpace(nickname))
            {
                Console.Write("Введи будь-ласка свій супер крутецький нікнейм: ");
                nickname = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(nickname))
                {
                    Console.WriteLine($"Нічого собі, крутий нік!");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Неправильний ввід. Спробуй ще раз ввести коректний нікнейм.");
                }
            }

            Console.WriteLine("Май на увазі додаток можна використовувати після виповнення 12 років!");
            string ageInput = "";
            while (!int.TryParse(ageInput, out age))
            {

                Console.Write("Тож скільки тобі рочків? ");
                ageInput = Console.ReadLine();

                if (!int.TryParse(ageInput, out age))
                {
                    Console.WriteLine("Неправильний ввід. Спробуй ще раз ввести свій вік.");
                }

            }

            if (age >= 12)
            {
                Console.WriteLine("Та дорослий, дорослий, добре)");
               
            }
            else
            {
                Console.WriteLine("Нажаль тобі ще не достатньо рочків.");
                Console.WriteLine("Bye!");
                return;
            }

            string yesNoChoice = "";
            string yesChoice = "yes", noChoice = "no";
            bool isFirstFight = true;


            while (true)
            {
                yesNoChoice = "";

                Console.Write("\n\n");
                Console.WriteLine("Подивимость, що тут у нас:");
                writeBorderedText(
                    $"Нікнейм: {nickname}\n" +
                    $"Вік: {age}\n" +
                    $"Зіграно раундів: {roundsPlayed}\n" +
                    $"Перемог: {victories}", ConsoleColor.Blue);


                while (yesNoChoice.ToLower() != yesChoice && yesNoChoice.ToLower() != noChoice)
                {

                    Console.Write("Ти готовий до бою? (Yes/No): ");
                    yesNoChoice = Console.ReadLine();

                    if (yesNoChoice.ToLower() != yesChoice && yesNoChoice.ToLower() != noChoice)
                    {
                        Console.WriteLine("Неправильний ввід. Спробуй ввести ще раз.");
                        Console.WriteLine($"user_input: {yesNoChoice}");
                    }

                }

                if (yesNoChoice.ToLower() == noChoice)
                {
                    Console.WriteLine($"Бувай друже, {nickname}!");
                    break;

                }

                if (isFirstFight)
                {
                    Console.WriteLine("Пам'ятай правила, як своє ім'я:");
                    writeBorderedText("Папір б'є камінь, але програє ножицям\nНожиці б'ють папір, але програють камінню\nКаміння б'є ножиці, але програє паперу", ConsoleColor.Red);
                }

                // rounds 1, 2, 3
                int weaponChoice = 0;
                string weaponChoiceInput = "";
                int aiWeaponChoice = 0;
                int playerWinsCount = 0;

                for (int roundNumber = 0; roundNumber < countRoundsPerFight; roundNumber++)
                {
                    Console.WriteLine($"Ррраунд #{roundNumber + 1}");

                    weaponChoice = 0;
                    writeBorderedText("Вибери тип зброї:\n1. Камінь\n2. Ножиці\n3. Папір", ConsoleColor.Blue);
         

                    weaponChoiceInput = "";
                    while (!int.TryParse(weaponChoiceInput, out weaponChoice) || weaponChoice > 3 || weaponChoice < 1)
                    {

                        Console.Write("Твій вибір (1-3): ");
                        weaponChoiceInput = Console.ReadLine();

                        if (!int.TryParse(weaponChoiceInput, out weaponChoice) || weaponChoice > 3 || weaponChoice < 1)
                        {
                            Console.WriteLine("Неправильний ввід. Спробуй ще раз.");
                        }
                    }
                    string[] displayChoice = new string[3]
                    {
                        """
                            _______
                        ---'   ____)
                                (_____)
                                (_____)
                                (____)
                        ---.__(___)
                        """,
                        """
                            _______
                        ---'   ____)____
                                    ______)
                                __________)
                                (____)
                        ---.__(___)
                        """,
                        """
                            _______
                        ---'    ____)____
                                    ______)
                                    _______)
                                    _______)
                        ---.__________)
                        """
                    };
                    // AI select weapon
                    aiWeaponChoice = random.Next(1, 4);
                    if (aiWeaponChoice == (int)gameChoice.Paper)
                    {
                        Console.WriteLine("ШІ вибрав: папір");
                        writeBorderedText(concatDisplayChoices(displayChoice[weaponChoice - 1], displayChoice[aiWeaponChoice - 1]), ConsoleColor.Cyan);
                    }

                    if (aiWeaponChoice == (int)gameChoice.Rock)
                    {
                        Console.WriteLine("ШІ вибрав: камінь");
                        writeBorderedText(concatDisplayChoices(displayChoice[weaponChoice - 1], displayChoice[aiWeaponChoice - 1]), ConsoleColor.Cyan);
                        
                    }
                    if (aiWeaponChoice == (int)gameChoice.Scissors)
                    {
                        Console.WriteLine("ШІ вибрав: ножиці");
                        writeBorderedText(concatDisplayChoices(displayChoice[weaponChoice - 1], displayChoice[aiWeaponChoice - 1]), ConsoleColor.Cyan);
                    }

                    // Determine the winner
                    if (weaponChoice == aiWeaponChoice)
                    {
                        Console.WriteLine("Поки що нічия, але ШІ повстане колись..");
                    }
                    else if ((weaponChoice == 1 && aiWeaponChoice == 2) ||
                             (weaponChoice == 2 && aiWeaponChoice == 3) ||
                             (weaponChoice == 3 && aiWeaponChoice == 1))
                    {
                        Console.WriteLine("Щож, наразі ти переміг!");
                        playerWinsCount++;
                    }
                    else
                    {
                        Console.WriteLine("ШІ переміг, судний день вже близько.");
                    }

                    roundsPlayed++;

                }

                if (playerWinsCount > 1)
                {
                  Console.WriteLine(winPhrases[random.Next(0, winPhrases.Length)]);
                    victories++;
                }
                else
                {
                    Console.WriteLine(loosePhrases[random.Next(0, loosePhrases.Length)]);
                }

                isFirstFight = false;
            }


            return;
        }
        
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Homework_Rock_Paper_Scissors();



        }
    }
}