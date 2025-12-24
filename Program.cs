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
                new Question("What is 2 + 2?",
                    new List<string>{ "3", "4", "5", "6" }, "4"),

                new Question("What language is this game written in?",
                    new List<string>{ "Java", "C#", "Python", "JS" }, "C#"),

                new Question("Which keyword is used for inheritance?",
                    new List<string>{ "extends", "inherits", ":", "base" }, ":"),

                new Question("What does OOP stand for?",
                    new List<string>{ "Object Oriented Programming", "Open Office Program", "Order Of Process", "None" },
                    "Object Oriented Programming"),

                new Question("Which is a value type?",
                    new List<string>{ "string", "class", "int", "array" }, "int"),

                new Question("Which symbol ends a C# statement?",
                    new List<string>{ ".", ":", ";", "," }, ";"),

                new Question("What does Console.ReadLine() return?",
                    new List<string>{ "int", "string", "bool", "char" }, "string"),

                new Question("Which access modifier is most restrictive?",
                    new List<string>{ "public", "protected", "internal", "private" }, "private"),

                new Question("Which keyword allows overriding?",
                    new List<string>{ "override", "new", "base", "sealed" }, "override"),

                new Question("What is used to store multiple values?",
                    new List<string>{ "variable", "list", "method", "class" }, "list"),

                // 10 more
                new Question("Which method is program entry point?",
                    new List<string>{ "Start()", "Main()", "Run()", "Init()" }, "Main()"),

                new Question("Which converts string to int safely?",
                    new List<string>{ "Parse()", "TryParse()", "Convert()", "Cast()" }, "TryParse()"),

                new Question("Which is NOT OOP principle?",
                    new List<string>{ "Encapsulation", "Inheritance", "Compilation", "Polymorphism" }, "Compilation"),

                new Question("What does virtual allow?",
                    new List<string>{ "Overriding", "Hiding", "Locking", "Sealing" }, "Overriding"),

                new Question("Which stores true/false?",
                    new List<string>{ "int", "string", "bool", "double" }, "bool"),

                new Question("Which keyword creates object?",
                    new List<string>{ "new", "create", "make", "alloc" }, "new"),

                new Question("Which loop runs at least once?",
                    new List<string>{ "for", "while", "do-while", "foreach" }, "do-while"),

                new Question("What does abstract class mean?",
                    new List<string>{ "Cannot be inherited", "Cannot be instantiated", "Is sealed", "Is static" },
                    "Cannot be instantiated"),

                new Question("Which keyword stops a loop?",
                    new List<string>{ "exit", "return", "stop", "break" }, "break"),

                new Question("Which collection has dynamic size?",
                    new List<string>{ "array", "list", "enum", "struct" }, "list")
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