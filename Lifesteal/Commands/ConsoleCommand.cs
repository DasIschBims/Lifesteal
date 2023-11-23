﻿using BattleBitAPI.Server;
using Lifesteal.API;
using log4net;

namespace Lifesteal.Types;

public class ConsoleCommand : Attribute
{
    public string Name { get; }
    public string Description { get; }
    public string Usage { get; }
    public Action<string[]>? Action { get; set; }
    protected GameServer<LifestealPlayer> Server { get; set; }
    protected  ILog Logger => Program.Logger;
        
    protected ConsoleCommand(string name, string description, string usage = "", Action<string[]>? action = null)
    {
        Name = name;
        Description = description;
        Usage = usage;
        Action = action;
        Server = Program.Server;
    }
        
    public override string ToString()
    {
        return $"{Name} - {Description}";
    }
}