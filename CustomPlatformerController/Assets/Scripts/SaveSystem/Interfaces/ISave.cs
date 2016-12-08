using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISave {
    void Add(object element);
    void Remove(object element);
    void Save();
    void Load();
    void Clear();
    T GetObject<T>();
    T GetObject<T>(T element);
    object GetObject(object element);
    List<T> GetObjects<T>();
    List<object> GetObjects();
}
