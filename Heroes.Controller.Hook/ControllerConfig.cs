using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Controller.Hook.Shared;

namespace Heroes.Controller.Hook
{
    public class Config : JsonSerializable<Config>
    {
        /// <summary>
        /// Uses original inputs acquired from the game.
        /// </summary>
        public bool UseOriginalInputs { get; set; } = false;

        public static string GetFilePath(string modFolder, int port) => $"{modFolder}\\Controller-{port}.json";
        public static Config FromPath(string modFolder, int port) => JsonSerializable<Config>.FromPath(GetFilePath(modFolder, port));
        public static void   ToPath(Config config, string modFolder, int port) => JsonSerializable<Config>.ToPath(config, GetFilePath(modFolder, port));
    }
}
