using System;
using System.Collections.Generic;

[Serializable]
public class OnboardModel
{    
    public List<string> Tutorials { get; private set; }

    public OnboardModel(List<string> tutorials)
    {
        Tutorials = tutorials;
    }
}