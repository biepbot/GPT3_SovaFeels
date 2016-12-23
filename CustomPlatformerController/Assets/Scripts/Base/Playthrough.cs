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
                return currentLevel == -1 ? levels[0] : levels[currentLevel];
            }
        }

        private int currentLevel = -1;

        [NonSerialized]
        private int levelGainedCoins = 0;

        public int LevelGainedCoins { get { return levelGainedCoins; } }

        private int playthroughCoins = 0;

        public int PlaythroughCoins { get { return playthroughCoins; } }

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

        /// <summary>
        /// Increases the coins in the current level.
        /// Can be used when a player gives the right answer to a NPC in a level.
        /// </summary>
        /// <param name="coins">The amount of coins you want to give the player</param>
        public void IncreaseCoinsCurrentLevel(int coins)
        {
            this.playthroughCoins += coins;
        }

        /// <summary>
        /// This method is used to move the currently gained coins from the level to the gained coins from the playthrough.
        /// After the levelgainedcoins have been added to the playthroughcoins the levelgainedcoins will be reset.
        /// </summary>
        public void IncreaseCoinsAfterLevelEnded()
        {
            this.playthroughCoins += levelGainedCoins;
            levelGainedCoins = 0;
        }
    }
}
