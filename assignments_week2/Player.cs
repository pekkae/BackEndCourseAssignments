using System;
using System.Collections.Generic;

public class Player : IPlayer
{
    public Guid Id { get; set; }
    public int Score { get; set; }
    public List<Item> Items { get; set; }
}

public class Item
{
    public Guid Id { get; set; }
    public int Level { get; set; }
}