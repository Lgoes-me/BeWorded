using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static List<T> RandomElementList<T>(this List<T> list, int quantity, int seed, bool allowRepeats = false)
    {
        var newList = new List<T>();
        newList.AddRange(list);

        var responseList = new List<T>();
        
        for (int i = 0; i < quantity; i++)
        {
            var randomElement = newList.RandomElement(seed + i);
            
            if(!allowRepeats)
                newList.Remove(randomElement);
            
            responseList.Add(randomElement);
        }
        
        return responseList;
    }
    
    public static T RandomElement<T>(this List<T> list, int seed)
    {
        var random = new Random(seed);
        return list[random.Next(list.Count)];
    }
    
    public static T RandomWeightedElement<T>(this List<(T, int)> list, int seed)
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

        return weightedList.RandomElement(seed);
    }
    
    public static string RandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}