﻿using System;
using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{

    private const float minDiff = 1f;
    private const float maxDiff = 3f;

    private const float difficultyIncreaseStep = 0.1f;
    private const float difficultyDecreaseStep = 0.1f;

    [Range(minDiff, maxDiff)]
    public float levelDifficulty;
    public int coins;
    public int amountOfPlaythroughs;
    public DateTime lastFinishedPlaythrough;

    public bool destroy = false;

    public List<Stats> stats;

    private static SaveSystem ss = new SaveSystem();

    private static GameStats instance = null;
    public static GameStats Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameStats();
            }
            return instance;
        }
    }

    private GameStats()
    {
        Load();
    }

    public void RewardCoins(int coins)
    {
        foreach (Stats stat in stats)
        {
            if (stat.categoryName == null)
            {
                stat.coins += coins;
            }
        }
    }

    public void DecreaseCoins(int coins)
    {
        foreach (Stats stat in stats)
        {
            if (stat.categoryName == null)
            {
                stat.coins -= coins;
            }
        }
    }

    public void DecreaseDifficulty()
    {
        foreach (Stats stat in stats)
        {
            if (stat.categoryName == null)
            {
                stat.levelDifficulty = ((stat.levelDifficulty - difficultyDecreaseStep) > minDiff) ? stat.levelDifficulty - difficultyDecreaseStep : minDiff;
            }
        }
    }

    public void IncreaseDifficulty()
    {
        foreach (Stats stat in stats)
        {
            if (stat.categoryName == null)
            {
                stat.levelDifficulty = ((stat.levelDifficulty + difficultyIncreaseStep) < maxDiff) ? stat.levelDifficulty + difficultyIncreaseStep : maxDiff;
            }
        }
    }

    public void IncreaseConfrontation(string level)
    {
        foreach (Stats stat in stats)
        {
            if (stat.categoryName != null)
            {
                if (stat.categoryName.Equals(level))
                {
                    stat.handle++;
                }
            }
        }
    }

    public void IncreaseIgnoreFlight(string level)
    {
        foreach (Stats stat in stats)
        {
            if (stat.categoryName != null)
            {
                if (stat.categoryName.Equals(level))
                {
                    stat.hide++;
                }
            }
        }
    }

    public void IncreaseFight(string level)
    {
        foreach (Stats stat in stats)
        {
            if (stat.categoryName != null)
            {
                if (stat.categoryName.Equals(level))
                {
                    stat.fight++;
                }
            }
        }
    }

    public void EndPlaythrough(DateTime date)
    {
        foreach (Stats stat in stats)
        {
            if (stat.categoryName == null)
            {
                stat.amountOfPlaythroughs++;
                stat.lastFinishedPlaythrough = date;
            }
        }
    }

    public bool Load()
    {
        ss.Clear();
        bool statsExist = ss.Load(Files.STATS_FNAME);

        if (!statsExist)
        {
            Debug.Log("Creating new Stats file");

            stats = new List<Stats>();

            levelDifficulty = 1;

            stats.Add(new Stats()
            {
                amountOfPlaythroughs = 0,
                levelDifficulty = this.levelDifficulty,
                coins = 0
            });

            stats.Add(new Stats()
            {
                categoryName = "1",
                handle = 0,
                hide = 0,
                fight = 0
            });

            stats.Add(new Stats()
            {
                categoryName = "2",
                handle = 0,
                hide = 0,
                fight = 0
            });

            stats.Add(new Stats()
            {
                categoryName = "3",
                handle = 0,
                hide = 0,
                fight = 0
            });

            stats.Add(new Stats()
            {
                categoryName = "4",
                handle = 0,
                hide = 0,
                fight = 0
            });

            ss.Clear();
            ss.Add(stats);
            ss.Save(Files.STATS_FNAME);
            ss.Clear();
        }

        if (ss.Load(Files.STATS_FNAME))
        {
            stats = ss.GetObject<List<Stats>>();
            if (stats != null)
            {
                foreach (Stats s in stats)
                {
                    if (s.categoryName == null)
                    {
                        levelDifficulty = s.levelDifficulty;
                        coins = s.coins;
                        amountOfPlaythroughs = s.amountOfPlaythroughs;
                        lastFinishedPlaythrough = s.lastFinishedPlaythrough;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool Save()
    {
        if (stats != null)
        {
            ss.Clear();
            ss.Add(stats);
            ss.Save(Files.STATS_FNAME);
            ss.Clear();
            return true;
        }
        else
        {
            return false;
        }
    }
}
