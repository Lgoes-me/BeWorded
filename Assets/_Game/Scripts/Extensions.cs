using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T RandomElement<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static T RandomWeightedElement<T>(this List<(T, int)> list)
    {
        var weightedList = new List<T>();
        
        foreach (var (item, weight) in list)
        {
            var count = weight;
            while (count != 0)
            {
                weightedList.Add(item);
                count--;
            }
        }

        return weightedList.RandomElement();
    }
}