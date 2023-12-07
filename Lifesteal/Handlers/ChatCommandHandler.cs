﻿using Lifesteal.API;
using Lifesteal.ChatCommands;

namespace Lifesteal.Handlers;

public class ChatCommandHandler
{
    public static Task<bool> Run(string message, LifestealPlayer player)
    {
        string chatCommandPrefix = Program.ServerConfiguration.ChatCommandPrefix ?? "!";
        if (!message.StartsWith(chatCommandPrefix)) return Task.FromResult(true);

        string[] args = message[chatCommandPrefix.Length..].Split(' ');
        string commandName = args[0];

        ChatCommand? command = GetCommandFromName(commandName);
        if (command == null)
        {
            player.Message($"Command {commandName} not found.");
            return Task.FromResult(false);
        }

        try
        {
            command.Action?.Invoke(args.Skip(1).ToArray(), player);
            Program.Logger.Info($"Command {commandName} with args {string.Join(" ", args.Skip(1).ToArray())} executed by \"{player.Name}\""); 
        }
        catch (Exception e)
        {
            player.Message($"An error occurred while executing command {commandName}");
            Program.Logger.Error($"An error occurred while executing command {commandName} with args {string.Join(" ", args.Skip(1).ToArray())} by \"{player.Name}\"");
            return Task.FromResult(false);
        }
        
        return Task.FromResult(false);
    }

    private static ChatCommand? GetCommandFromName(string name)
    {
        return ChatCommandList.Commands.FirstOrDefault(command => command.Name == name);
    }
}