using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{

    private const int minDiff = 1;
    private const int maxDiff = 4;

    [Range(minDiff, maxDiff)]
    public int levelDifficulty;
    public int handle;
    public int fight;
    public int hide;
    public int coins;
    public int amountOfPlaythroughs;

    public bool destroy = false;

    private static SaveSystem ss = new SaveSystem();

    private static GameStats instance = null;
    public static GameStats Instance { get { return instance; } }

    private void Awake()
    {
        Load();
        DontDestroyObject();
    }

    private void DontDestroyObject()
    {
        if (!destroy)
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public int Total
    {
        get
        {
            return (fight + handle + hide);
        }
    }

    public Stats GetStats()
    {
        Stats stats = new Stats()
        {
            amountOfPlaythroughs = this.amountOfPlaythroughs,
            handle = this.handle,
            fight = this.fight,
            hide = this.hide,
            levelDifficulty = this.levelDifficulty,
            coins = this.coins,
        };
        return stats;
    }

    public void RewardCoins(int coins)
    {
        this.coins += coins;
    }

    public void DecreaseCoins(int coins)
    {
        this.coins -= coins;
    }

    public void DecreaseDifficulty()
    {
        levelDifficulty = (levelDifficulty > minDiff) ? --levelDifficulty : minDiff;
    }

    public void IncreaseDifficulty()
    {
        levelDifficulty = (levelDifficulty < maxDiff) ? ++levelDifficulty : maxDiff;
    }

    public bool Load()
    {
        ss.Clear();
        if (ss.Load(Files.STATS_FNAME))
        {
            List<Stats> stats = ss.GetObject<List<Stats>>();
            if (stats != null)
            {
                foreach (Stats s in stats)
                {
                    if (s.categoryName == null)
                    {
                        levelDifficulty = s.levelDifficulty;
                        handle = s.handle;
                        fight = s.fight;
                        hide = s.hide;
                        coins = s.coins;
                        amountOfPlaythroughs = s.amountOfPlaythroughs;
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
        List<Stats> stats = ss.GetObject<List<Stats>>();
        if (stats != null)
        {
            for (int i = 0; i < stats.Count; i++)
            {
                if (stats[i].categoryName == null)
                {
                    stats[i] = GetStats();
                }
            }
            ss.Save(Files.STATS_FNAME);
            return true;
        }
        else
        {
            return false;
        }
    }
}
