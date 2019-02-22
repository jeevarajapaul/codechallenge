/*
 * Author       : Jeeva Raja Paul
 * Date Created : 2019-Jan-12
 * Description  : A game in which the Turtle moves 
 *              across each postion some of which has mines as well.
 *              Turle is allowed to turn/rotate 90 degree right angle/clock wise (left rotate or anti clock wise is not allowed)
 *                         
 *              If all the moves are consumed and the turtle neither hit wall, caught in mine or exit,
 *              then it is considered to be in the middle.
*/
using System;

namespace BoardPlay
{
    internal class Turtle
    {
        private const int MOVE_ACTION = 1;
        private const int ROTATE_ACTION = 0;
        private const int START_INDEX = 0;

        private readonly int noOfDirections = Enum.GetValues(typeof(DirectionEnum)).Length;
        private readonly GameConfig boardConfig;

        private Position currentPosition = new Position();
        private enum DirectionEnum { North = 1, East, South, West };
        private DirectionEnum currentDirection;
        public enum ResultCase { InMiddle = 1, HitWall, HitMine, Exit };
        public delegate void ShowResultHandler(ResultCase resultCase);
        public event ShowResultHandler ShowMoveResult;

        public Turtle(GameConfig injectedboardConfig)
        {
            boardConfig = injectedboardConfig;
            ResetGameConfig();
        }

        public void ResetGameConfig()
        {
            currentPosition.X = boardConfig.StartPosition.X;
            currentPosition.Y = boardConfig.StartPosition.Y;
            currentDirection = (DirectionEnum)Enum.Parse(typeof(DirectionEnum), boardConfig.Direction);
        }

        /// <summary>
        /// This function takes the array of moves which is configured in the Moves.JSON
        /// 0 denotes rotation and 1 donates move
        /// </summary>
        /// <param name="sequenceToMove"></param>
        public void Play(int[] sequenceToMove)
        {
            bool inMiddle = true;
            for (int eachIdx = START_INDEX; eachIdx < sequenceToMove.Length; eachIdx++)
            {
                if (sequenceToMove[eachIdx] == MOVE_ACTION)
                {
                    if (!Move())
                    {
                        inMiddle = false;
                        break;
                    }
                }
                else
                {
                    Rotate();
                }
            }
            if (inMiddle)
            {
                ShowMoveResult(ResultCase.InMiddle);
            }
        }
        private bool Move()
        {
            switch (currentDirection)
            {
                case DirectionEnum.North:
                    currentPosition.Y--;
                    break;
                case DirectionEnum.East:
                    currentPosition.X++;
                    break;
                case DirectionEnum.South:
                    currentPosition.Y++;
                    break;
                case DirectionEnum.West:
                    currentPosition.X--;
                    break;
                default:
                    break;
            }
            return IsFurtherMovePossible();
        }

        /// <summary>
        /// This function check whether further move is possible or not based on mine, exit or hit wall
        /// </summary>
        /// <returns></returns>
        private bool IsFurtherMovePossible()
        {
            bool reachedEnd = true;

            if (boardConfig.MinesPosition != null && boardConfig.MinesPosition.Exists(pos => pos.X == currentPosition.X && pos.Y == currentPosition.Y))
            {
                ShowMoveResult(ResultCase.HitMine);
            }
            else if (currentPosition.X > boardConfig.BoardSize.X || currentPosition.Y > boardConfig.BoardSize.Y ||
                currentPosition.X < START_INDEX || currentPosition.Y < START_INDEX)
            {
                ShowMoveResult(ResultCase.HitWall);
            }
            else if (currentPosition.X == boardConfig.ExitPosition.X && currentPosition.Y == boardConfig.ExitPosition.Y)
            {
                ShowMoveResult(ResultCase.Exit);
            }
            else
            {
                reachedEnd = false;
            }
            return !reachedEnd;
        }

        /// <summary>
        /// This function rotates the Turtle 90 degree clock wise
        /// </summary>
        private void Rotate()
        {
            if ((int)currentDirection == noOfDirections)
            {
                currentDirection = DirectionEnum.North;
            }
            else
            {
                currentDirection++;
            }
        }
    }
}
