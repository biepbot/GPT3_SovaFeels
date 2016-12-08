using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

public class SaveSystem : ISave
{
    private static int defaultSize = 16;
    private static string defaultPath = "/SaveData";
    private static string fileName = "/data.dat";


    private int size;
    private int itemCount;

    private static SaveSystem instance = null;

    public static SaveSystem Instance { get { return instance = (instance == null) ? new SaveSystem() : instance; } }

    private List<object> objects;

    protected SaveSystem()
    {
        this.objects = new List<object>();
    }

    public void Add(object element)
    {
        if (ExtensionMethods.IsSerializable(element))
        {
            objects.Add(element);
        }
        else
        {
            throw new SerializationException("The given object is not serializable");
        }
    }

    public void Remove(object element)
    {
        objects.Remove(element);
    }

    public void Save()
    {
        string path = Application.persistentDataPath + /*defaultPath +*/ fileName;
        if (File.Exists(path))
            File.Delete(path);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(path, FileMode.CreateNew);
        bf.Serialize(fs, objects);
        fs.Close();
    }

    public void Load()
    {
        string path = Application.persistentDataPath /*+ defaultPath*/+ fileName;
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.OpenRead(path);
            List<object> deserializedObjects = (List<object>)bf.Deserialize(fs);
            foreach(object o in deserializedObjects)
            {
                objects.Add(o);
            }
        }
    }

    public void Clear()
    {
        this.objects.Clear();
    }

    public T GetObject<T>()
    {
        foreach(object o in objects)
        {
            if(o.GetType() == typeof(T))
            {
                return (T)o;
            }
        }
        return default(T);
    }

    public T GetObject<T>(T element)
    {
        foreach(object o in objects)
        {
            if (element.Equals(o))
            {
                return (T)o;
            }
        }
        return default(T);
    }

    public object GetObject(object element)
    {
        foreach(object o in objects)
        {
            if (o.Equals(element))
            {
                return o;
            }
        }
        return null;
    }

    public List<T> GetObjects<T>()
    {
        List<T> types = new List<T>();
        foreach(object o in objects)
        {
            if(o.GetType() == typeof(T))
            {
                types.Add((T)o);
            }
        }
        return types;
    }

    public List<object> GetObjects()
    {
        return objects;
    }
}

public static class ExtensionMethods
{
    /// <summary>
    /// Source: http://stackoverflow.com/a/4037838
    /// Checks wether the object is serializable.
    /// </summary>
    /// <param name="obj">The object you want to serialize.</param>
    /// <returns></returns>
    public static bool IsSerializable(this object obj)
    {
        Type t = obj.GetType();

        return Attribute.IsDefined(t, typeof(DataContractAttribute)) || t.IsSerializable || (obj is IXmlSerializable);
    }
}

[System.Serializable]
public class TestData
{
    private int test1;
    private int test2;
    private string test3;
    private float test4;

    public TestData(int test1, int test2, string test3, float test4)
    {
        this.test1 = test1;
        this.test2 = test2;
        this.test3 = test3;
        this.test4 = test4;
    }
}

[System.Serializable]
public class TestData2
{
    private int test1;
    private string test3;
    private float test4;

    public TestData2(int test1, string test3, float test4)
    {
        this.test1 = test1;
        this.test3 = test3;
        this.test4 = test4;
    }
}
