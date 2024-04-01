using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class Onboard : ISavable<OnboardModel>, ILoadable<OnboardModel>
{
    public string Id { get; set; }
    private List<string> Tutorials { get; set; }

    public Onboard()
    {
        Id = "Onboard.json";
        Tutorials = new List<string>();
    }

    public bool CanShow(string id)
    {
        var tutorial = Tutorials.FirstOrDefault(t => t == id);

        if (tutorial != null) 
            return false;
        
        Tutorials.Add(id);
        return true;
    }

    public void LoadData(OnboardModel data)
    {
        Tutorials = data.Tutorials;
    }

    public OnboardModel SaveData()
    {
        return new OnboardModel(Tutorials);
    }
}