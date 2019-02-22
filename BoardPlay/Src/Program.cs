/*
 * Author       : Jeeva Raja Paul
 * Date Created : 2019-Jan-12
 * Description  : This is the main program which loads and validates the setting files and start the play
 *                if the settings are valid
*/
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace BoardPlay
{
    class Program
    {

        private static GameConfig gameConfig = new GameConfig();
        private static List<int[]> listOfMoves = new List<int[]>();
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please provide both game and move setting files as arguments.");
            }
            else
            {
                string gameSettingsFile = args[0];
                string movesFile = args[1]; if (File.Exists(gameSettingsFile) && File.Exists(movesFile))
                {
                    Console.WriteLine("================== Turtle Game ==================");
                    Console.WriteLine("Below are the default settings if not given.");
                    Console.WriteLine("Direction  : East");
                    Console.WriteLine("Start Point: (0, 0)");
                    Console.WriteLine("Exit Point : (max(X), max(Y))");
                    Console.WriteLine("================== Turtle Game ==================");
                    if (LoadAndValidateSettings(gameSettingsFile, movesFile))
                    {
                        StartPlay();
                    }
                }
                else
                {
                    Console.WriteLine("Either game or moves settings file does not exist.");
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        /// <summary>
        /// Start the play if the settings are valid
        /// </summary>
        private static void StartPlay()
        {
            try
            {
                Console.WriteLine("Starting my play ...");
                Turtle turtle = new Turtle(gameConfig);
                turtle.ShowMoveResult += new Turtle.ShowResultHandler(OnDispalyResult);
                int seqNo = 1;
                foreach (int[] sequence in listOfMoves)
                {
                    Console.Write($"    Sequence-{seqNo}: ");
                    turtle.Play(sequence);
                    seqNo++;
                    turtle.ResetGameConfig();
                }
            }
            catch (Exception ec)
            {
                Console.WriteLine("Error while playing Turtle game.");
                Console.WriteLine(ec.Message);
            }
        }

        /// <summary>
        /// Loads the setting files and validate the values accordingly
        /// </summary>
        /// <param name="gameSettingsFile"></param>
        /// <param name="movesFile"></param>
        /// <returns></returns>
        private static bool LoadAndValidateSettings(string gameSettingsFile, string movesFile)
        {
            Console.WriteLine("Loading of the game and moves setting files is started.");
            bool isSettingsLoadedAndValid = false;

            try
            {
                gameConfig.LoadSettings(gameSettingsFile);
                if (gameConfig.BoardSize is null)
                {
                    Console.WriteLine("    Info: Board size is required to be given in the game settings JSON file.");
                }
                else if (gameConfig.StartPosition.X > gameConfig.BoardSize.X || gameConfig.StartPosition.Y > gameConfig.BoardSize.Y)
                {
                    Console.WriteLine("    Info: Start position should be inside the board.");
                }
                else if (gameConfig.ExitPosition.X > gameConfig.BoardSize.X || gameConfig.ExitPosition.Y > gameConfig.BoardSize.Y)
                {
                    Console.WriteLine("    Info: Exit position should be inside the board.");
                }
                else if (gameConfig.ExitPosition.X == gameConfig.StartPosition.X && gameConfig.ExitPosition.Y == gameConfig.StartPosition.Y)
                {
                    Console.WriteLine("    Info: Start and Exist position cannot be same.");
                }
                else if (gameConfig.MinesPosition != null &&
                    gameConfig.MinesPosition.Exists(pos => pos.X == gameConfig.StartPosition.X && pos.Y == gameConfig.StartPosition.Y))
                {
                    Console.WriteLine("    Info: Start position cannot be in the mine position.");
                }
                else if (gameConfig.MinesPosition != null &&
                    gameConfig.MinesPosition.Exists(pos => pos.X == gameConfig.ExitPosition.X && pos.Y == gameConfig.ExitPosition.Y))
                {
                    Console.WriteLine("    Info: Exit position cannot be in the mine position.");
                }
                else
                {
                    string jsonContent = File.ReadAllText(movesFile);
                    dynamic moves = JObject.Parse(jsonContent);

                    if (moves.Sequence != null)
                    {
                        listOfMoves = JsonConvert.DeserializeObject<List<int[]>>(moves.Sequence.ToString());
                        if (listOfMoves.Count > 0)
                        {
                            Console.WriteLine("Game setting files are successfully loaded with all mandatory values.");
                            Console.WriteLine("------------------ Game Settings ------------------");
                            Console.WriteLine($"Direction  : {gameConfig.Direction}");
                            Console.WriteLine($"Board Size: ({gameConfig.BoardSize.X}, {gameConfig.BoardSize.Y})");
                            Console.WriteLine($"Start Point: ({gameConfig.StartPosition.X}, {gameConfig.StartPosition.Y})");
                            Console.WriteLine($"Exit Point: ({gameConfig.ExitPosition.X}, {gameConfig.ExitPosition.Y})");
                            Console.WriteLine("------------------ Game Settings ------------------");
                            isSettingsLoadedAndValid = true;
                        }
                        else
                        {
                            Console.WriteLine("    Info: There are no sequences configured, hence there is no play.");
                        }
                    }
                } //Check whether board size is configured or not
            }
            catch (Exception ec)
            {
                Console.WriteLine("    Error while loading/parsing the game settings and moves JSON files.");
                Console.WriteLine("    Please check the JSON schema and try again.");
                Console.WriteLine(ec.Message);
            }
            return isSettingsLoadedAndValid;
        }

        /// <summary>
        /// Display the result based on the fired event
        /// </summary>
        /// <param name="resultCase"></param>
        private static void OnDispalyResult(Turtle.ResultCase resultCase)
        {
            switch (resultCase)
            {
                case Turtle.ResultCase.HitWall:
                    Console.WriteLine("I hit the wall.");
                    break;
                case Turtle.ResultCase.HitMine:
                    Console.WriteLine("I am trapped in the mine.");
                    break;
                case Turtle.ResultCase.Exit:
                    Console.WriteLine("I safely came out.");
                    break;
                case Turtle.ResultCase.InMiddle:
                    Console.WriteLine("I stopped somewhere.");
                    break;
            }
        }

    }
}
