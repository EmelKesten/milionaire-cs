using System;
using System.Collections.Generic;

namespace QuizConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.ShowMenu();
        }
    }

    abstract class GameBase
    {
        public abstract void Start();
    }

    class Game : GameBase
    {
        private Player player;
        private Quiz quiz;

        public Game()
        {
            player = new Player("Player1");
            quiz = new Quiz();
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== QUIZ GAME =====");
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. Show High Score");
                Console.WriteLine("3. Exit");
                Console.Write("Choose option: ");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    Start();
                }
                else if (input == "2")
                {
                    Console.WriteLine($"High Score: {player.HighScore}");
                    Console.WriteLine("Press any key...");
                    Console.ReadKey();
                }
                else if (input == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                    Console.ReadKey();
                }
            }
        }

        public override void Start()
        {
            player.Score = 100;
            quiz.ShuffleQuestions();

            foreach (Question q in quiz.Questions)
            {
                Console.Clear();
                Console.WriteLine(q.Text);

                for (int i = 0; i < q.Answers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {q.Answers[i]}");
                }

                int choice = GetValidInput(1, 4);

                if (q.Answers[choice - 1] == q.CorrectAnswer)
                {
                    Console.WriteLine("Correct!");
                    player.Score *= 2;
                }
                else
                {
                    Console.WriteLine("Wrong!");
                    break;
                }

                Console.WriteLine($"Current Score: {player.Score}");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }

            Console.WriteLine("Game Over!");
            Console.WriteLine($"Final Score: {player.Score}");

            if (player.Score > player.HighScore)
            {
                player.HighScore = player.Score;
                Console.WriteLine("NEW HIGH SCORE!");
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private int GetValidInput(int min, int max)
        {
            while (true)
            {
                Console.Write("Your answer: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int value))
                {
                    if (value >= min && value <= max)
                        return value;
                }

                Console.WriteLine("Invalid input, try again.");
            }
        }
    }

    class Player
    {
        private int score;
        private int highScore;

        public string Name { get; set; }

        // ===== GETTER & SETTER + PROPERTY =====
        public int Score
        {
            get { return score; }
            set
            {
                if (value >= 0)
                    score = value;
            }
        }

        public int HighScore
        {
            get { return highScore; }
            set
            {
                if (value > highScore)
                    highScore = value;
            }
        }

        public Player(string name)
        {
            Name = name;
            Score = 100;
            HighScore = 0;
        }
    }

    class Question
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswer { get; set; }

        public Question(string text, List<string> answers, string correct)
        {
            Text = text;
            Answers = answers;
            CorrectAnswer = correct;
        }
    }

    class Quiz
    {
        public List<Question> Questions { get; private set; }

        public Quiz()
        {
            Questions = new List<Question>()
{
    new Question("Koliko je 2 + 2?",
        new List<string>{ "3", "4", "5", "6" }, "4"),

    new Question("Koji je glavni grad Bosne i Hercegovine?",
        new List<string>{ "Mostar", "Banja Luka", "Sarajevo", "Tuzla" }, "Sarajevo"),

    new Question("Koje boje je nebo po vedrom danu?",
        new List<string>{ "Zeleno", "Plavo", "Crveno", "Žuto" }, "Plavo"),

    new Question("Koliko dana ima jedna sedmica?",
        new List<string>{ "5", "6", "7", "8" }, "7"),

    new Question("Koja životinja daje mlijeko?",
        new List<string>{ "Pas", "Krava", "Kokoš", "Riba" }, "Krava"),

    new Question("Koje godišnje doba dolazi poslije proljeća?",
        new List<string>{ "Jesen", "Zima", "Ljeto", "Proljeće" }, "Ljeto"),

    new Question("Koliko sati ima jedan dan?",
        new List<string>{ "12", "18", "24", "36" }, "24"),

    new Question("Čime se piše na papiru?",
        new List<string>{ "Kašikom", "Olovkom", "Viljuškom", "Čašom" }, "Olovkom"),

    new Question("Koja planeta je najbliža Suncu?",
        new List<string>{ "Zemlja", "Mars", "Merkur", "Venera" }, "Merkur"),

    new Question("Koliko nogu ima pauk?",
        new List<string>{ "6", "8", "10", "12" }, "8"),

    new Question("Koji je najveći kontinent?",
        new List<string>{ "Evropa", "Afrika", "Azija", "Australija" }, "Azija"),

    new Question("Koje je nacionalno piće u BiH?",
        new List<string>{ "Čaj", "Kafa", "Sok", "Mlijeko" }, "Kafa"),

    new Question("Koji mjesec ima 28 ili 29 dana?",
        new List<string>{ "Januar", "Februar", "Mart", "April" }, "Februar"),

    new Question("Šta koristimo za mjerenje vremena?",
        new List<string>{ "Vaga", "Sat", "Metar", "Termometar" }, "Sat"),

    new Question("Koja životinja laje?",
        new List<string>{ "Mačka", "Pas", "Krava", "Ovca" }, "Pas"),

    new Question("Koja boja nastaje miješanjem plave i žute?",
        new List<string>{ "Crvena", "Zelena", "Ljubičasta", "Narandžasta" }, "Zelena"),

    new Question("Koliko minuta ima jedan sat?",
        new List<string>{ "30", "45", "60", "90" }, "60"),

    new Question("Šta jedemo za doručak?",
        new List<string>{ "Krevet", "Hljeb", "Cipele", "Kamen" }, "Hljeb"),

    new Question("Koja životinja mjauče?",
        new List<string>{ "Pas", "Krava", "Mačka", "Konj" }, "Mačka"),

    new Question("Koje godišnje doba je najhladnije?",
        new List<string>{ "Ljeto", "Proljeće", "Jesen", "Zima" }, "Zima")
};
        }

        public void ShuffleQuestions()
        {
            Random rnd = new Random();
            for (int i = Questions.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = Questions[i];
                Questions[i] = Questions[j];
                Questions[j] = temp;
            }
        }
    }
}