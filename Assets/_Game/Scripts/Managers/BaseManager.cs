using UnityEngine;

public class BaseManager : MonoBehaviour
{
    protected Application Application { get; private set; }

    public void Setup(Application application)
    {
        Application = application;
    }
}