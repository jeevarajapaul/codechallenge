/*
 * Author       : Jeeva Raja Paul
 * Date Created : 2019-Jan-12
 * Description  : This class loads the game and moves settings from the files which are provided
 *                as the arguments 
*/
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace BoardPlay
{
    internal class GameConfig
    {
        private dynamic gameConfigData;

        public Position StartPosition { get; private set; }
        public Position BoardSize { get; private set; }
        public Position ExitPosition { get; private set; }
        public List<Position> MinesPosition { get; private set; }

        public string Direction { get; set; }

        public GameConfig()
        {
        }

        /// <summary>
        /// Loads the game setting and moves configuration files
        /// </summary>
        /// <param name="boardSettingFile"></param>
        public void LoadSettings(string boardSettingFile)
        {
            try
            {
                string jsonContent = File.ReadAllText(boardSettingFile);
                gameConfigData = JObject.Parse(jsonContent);
                //If board size is not configured, there is no point of reading other values
                if (gameConfigData.BoardSize != null)
                {
                    BoardSize = JsonConvert.DeserializeObject<Position>(gameConfigData.BoardSize.ToString());

                    if (gameConfigData.Direction is null)
                    {
                        Direction = "East";
                    }
                    else
                    {
                        Direction = gameConfigData.Direction;
                    }
                    var mines = gameConfigData.Mines;
                    if (mines != null)
                    {
                        MinesPosition = JsonConvert.DeserializeObject<List<Position>>(mines.ToString());
                    }
                    if (gameConfigData.StartPoint is null)
                    {
                        StartPosition = new Position();
                    }
                    else
                    {
                        StartPosition = JsonConvert.DeserializeObject<Position>(gameConfigData.StartPoint.ToString());
                    }
                    if (gameConfigData.ExitPoint is null)
                    {
                        ExitPosition = new Position
                        {
                            X = BoardSize.X,
                            Y = BoardSize.Y
                        };
                    }
                    else
                    {
                        ExitPosition = JsonConvert.DeserializeObject<Position>(gameConfigData.ExitPoint.ToString());
                    }
                }
            }
            catch (Exception ec)
            {
                throw ec;
            }
        }
    }
}
