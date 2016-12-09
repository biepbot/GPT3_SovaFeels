using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SaveSystem ss = new SaveSystem();
        TestData td1 = new TestData(1, 1, "test", 0.5f);
        TestData td2 = new TestData(1, 2, "test2", 3.5f);
        TestData2 td3 = new TestData2(1, "test3", 0.2f);
        ss.Add(td1);
        ss.Add(td2);
        ss.Add(td3);
        ss.Save(); //Saves all objects in the savesystem to a binary object.
        ss.Clear(); //Clears the objects in the savesystem.
        ss.Load(); //
        TestData2 l1 = ss.GetObject<TestData2>();
        l1.Test1 = 1337;
        l1.Test3 = "Replaced";
        ss.Replace(l1);
        TestData2 r1 = ss.GetObject<TestData2>();
        List<TestData> l2 = ss.GetObjects<TestData>();
        List<object> objects = ss.GetObjects();
        ss.RemoveAll<TestData>();
        List<object> objects2 = ss.GetObjects();

        Debug.Log("Did it works?");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
