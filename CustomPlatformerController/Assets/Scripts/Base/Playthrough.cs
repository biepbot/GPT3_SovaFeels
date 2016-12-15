using Assets.Scripts.Base.Exceptions;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Base
{
    [Serializable]
    public class Playthrough
    {
        private List<int> levels = new List<int>();

        public List<int> Levels
        {
            get
            {
                return levels;
            }
        }

        public bool HasLevels
        {
            get
            {
                return levels.Count != 0;
            }
        }

        public bool LacksLevels
        {
            get
            {
                return !HasLevels;
            }
        }

        public bool NoMoreLevels
        {
            get
            {
                return currentLevel == levels.Count - 1;
            }
        }

        public int CurrentLevel
        {
            get
            {
                return currentLevel == -1? levels[0] : levels[currentLevel];
            }
        }

        private int currentLevel = -1;

        public Playthrough()
        {

        }

        /// <summary>
        /// Raises the level number and returns it
        /// </summary>
        /// <returns></returns>
        public int NextLevel()
        {
            if (NoMoreLevels)
            {
                throw new NotEnoughLevelsException();
            }
            return levels[++currentLevel];
        }
    }
}
