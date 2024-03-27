using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static List<T> RandomElementList<T>(this List<T> list, int quantity, bool allowRepeats = false)
    {
        var newList = new List<T>();
        newList.AddRange(list);

        var responseList = new List<T>();
        
        for (int i = 0; i < quantity; i++)
        {
            var randomElement = newList.RandomElement();
            
            if(!allowRepeats)
                newList.Remove(randomElement);
            
            responseList.Add(randomElement);
        }
        
        return responseList;
    }
    
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