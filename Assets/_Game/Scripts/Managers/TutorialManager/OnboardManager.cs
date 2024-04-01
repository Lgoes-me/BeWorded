public class OnboardManager : BaseManager
{
    private Onboard Onboard { get; set; }

    public void Init()
    {
        Onboard = new Onboard();
        Application.SaveManager.LoadData(Onboard);
    }

    public bool CanShow(string id)
    {
        var canShow = Onboard.CanShow(id);
        Application.SaveManager.SaveData(Onboard);
        return canShow;
    }
}
