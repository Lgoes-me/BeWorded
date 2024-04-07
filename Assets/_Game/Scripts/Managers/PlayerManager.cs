using System;
using System.Collections.Generic;

public class PlayerManager : BaseManager
{
    public Player Player { get; private set; }
    public List<Player> LoadedPlayers { get; private set; }
    
    public void Init()
    {
        LoadedPlayers = Application.SaveManager.LoadFilesList<PlayerModel, Player>();
    }
    
    public void CreateNewPlayer(string seed = "")
    {
        Player = Application.ConfigManager.PlayerConfig.CreatePlayer(seed);
    }
    
    public bool HasLoadedPlayer()
    {
        return LoadedPlayers.Count > 0;
    }
    
    public void LoadSavedPlayer()
    {
        if (!HasLoadedPlayer())
            throw new Exception("Não devia chamar essa função sem checar antes se tem player");
        
        Player =  LoadedPlayers[0];
    }
}