using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class FileRepository : IRepository
{
    public async Task<Player> Get(Guid id)
    {
        string text = await File.ReadAllTextAsync("game-dev.txt");
        Player[] players = JsonConvert.DeserializeObject<Player[]>(text);

        foreach (Player player in players)
        {
            if (player.Id == id)
            {
                return player;
            }
        }


        return null;                    // if player not found
    }
    public async Task<Player[]> GetAll()
    {
        string text;

        try
        {
            text = await File.ReadAllTextAsync("game-dev.txt");
        }
        catch (FileNotFoundException ex)
        {
            return null;
        }

        Player[] players = JsonConvert.DeserializeObject<Player[]>(text);

        if (players.Length > 0)
        {
            return players;
        }
        else
        {
            return null;
        }
    }
    public async Task<Player> Create(Player player)
    {
        string text;
        Player[] players;
        List<Player> playerList;

        try
        {
            text = await File.ReadAllTextAsync("game-dev.txt");
            players = JsonConvert.DeserializeObject<Player[]>(text);
            playerList = new List<Player>(players);
        }
        catch (FileNotFoundException ex)
        {
            playerList = new List<Player>();
        }

        playerList.Add(player);

        text = JsonConvert.SerializeObject(playerList.ToArray());

        File.WriteAllText("game-dev.txt", text);

        return player;
    }
    public async Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        string text;
        Player[] players;
        List<Player> playerList;
        Player foundPlayer = null;

        try
        {
            text = await File.ReadAllTextAsync("game-dev.txt");
            players = JsonConvert.DeserializeObject<Player[]>(text);
            playerList = new List<Player>(players);
        }
        catch (FileNotFoundException ex)
        {
            return null;
        }

        foreach (Player p in playerList)
        {
            if (p.Id == id)
            {
                p.Score = player.Score;
                foundPlayer = p;
                break;
            }
        }

        text = JsonConvert.SerializeObject(playerList.ToArray());

        File.WriteAllText("game-dev.txt", text);


        return foundPlayer;
    }

    public async Task<Player> Delete(Guid id)
    {
        string text;
        Player[] players;
        List<Player> playerList;
        Player foundPlayer = null;

        try
        {
            text = await File.ReadAllTextAsync("game-dev.txt");
            players = JsonConvert.DeserializeObject<Player[]>(text);
            playerList = new List<Player>(players);
        }
        catch (FileNotFoundException ex)
        {
            return null;
        }

        foreach (Player p in playerList)
        {
            if (p.Id == id)
            {
                foundPlayer = p;
            }
        }

        if (foundPlayer != null)
        {
            playerList.Remove(foundPlayer);

            text = JsonConvert.SerializeObject(playerList.ToArray());
            File.WriteAllText("game-dev.txt", text);
        }


        return foundPlayer;
    }

}