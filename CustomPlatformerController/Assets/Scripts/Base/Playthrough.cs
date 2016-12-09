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
            return currentLevel++;
        }
    }
}
