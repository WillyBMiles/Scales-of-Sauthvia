using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extension
{

    public static bool ContainsValue<T, U>(this Dictionary<T, U> dictionary, U value)
    {
        foreach (var pair in dictionary)
        {
            if (pair.Value.Equals(value))
                return true;
        }
        return false;
    }

    public static T RandomValue<T>(this IEnumerable<T> list)
    {
        if (list.Count() == 0)
            return default;
        
        return list.Skip(Random.Range(0,list.Count())).FirstOrDefault();
    }

    public static List<T> RandomValues<T>(this IEnumerable<T> list, int number)
    {
        if (list.Count() == 0)
            return default;
        List<T> ts = new();
        
        for (int i =0; i < number; i++)
        {
            ts.Add(list.Where(t => !ts.Contains(t)).RandomValue());
        }


        return ts;
    }

    public static void SetX(this Transform transform, float newX, bool relative = false)
    {
        transform.position = new Vector3((relative ? transform.position.x : 0f) + newX, transform.position.y, transform.position.z);
    }

    public static void SetY(this Transform transform, float newY, bool relative = false)
    {
        transform.position = new Vector3(transform.position.x, (relative ? transform.position.y : 0f) + newY, transform.position.z);
    }
}