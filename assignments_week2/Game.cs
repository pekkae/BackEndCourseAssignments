using System.Collections.Generic;
public class Game<T> where T : IPlayer
{
    private List<T> _players;

    public Game(List<T> players)
    {
        _players = players;
    }

    public T[] GetTop10Players()
    {
        // ... write code that returns 10 players with highest scores
        List<T> top10 = new List<T>();
        //List<T> sorted = _players.OrderByDescending(s => T.Score).ToList();

        _players.Sort((T x, T y) => y.Score - x.Score);

        for (int i = 0; i < 10; i++)
        {
            top10.Add(_players[i]);
        }

        return top10.ToArray();
    }
}