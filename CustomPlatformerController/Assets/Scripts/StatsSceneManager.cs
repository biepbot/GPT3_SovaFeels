using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Base;
using UnityEngine;
using UnityEngine.UI;

public class StatsSceneManager : MonoBehaviour
{
    public GameObject categoryText;
    public GameObject viewport;
    public int spaceBetweenRows = 70;
    public int textBoxWidth = 1660;
    public int textBoxLength = 100;

    private static SaveSystem saveSystem = new SaveSystem();
    private List<CategorySettings> categories;

    // Use this for initialization
    void Start()
    {
        LoadStats();
    }

    private void LoadStats()
    {
        saveSystem.Clear();
        saveSystem.Load(Files.STATS_FNAME);
        categories = saveSystem.GetObject<List<CategorySettings>>();
        saveSystem.Clear();

        int counter = 0;
        foreach (CategorySettings category in categories)
        {
            categoryText = Instantiate(Resources.Load("CategoryText")) as GameObject;
            categoryText.name = category.categoryName;
            categoryText.GetComponent<Text>().text = category.categoryName + " - Totaal: " + category.Total + " - A: " + category.handle + ", K: " + category.fight + ", W: " + category.hide;
            categoryText.transform.SetParent(viewport.transform);
            categoryText.transform.localPosition = new Vector3(5, 0, 0);
            categoryText.transform.localPosition += new Vector3(0, -spaceBetweenRows * counter, 0);
            categoryText.GetComponent<RectTransform>().sizeDelta = new Vector2(textBoxWidth, textBoxLength);
            counter++;
        }
    }
}
