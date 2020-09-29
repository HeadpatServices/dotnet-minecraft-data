using System;
using System.Text.Json;

namespace dotnet_minecraft_data
{
    public class Biome
    {
        public readonly int Id;
        public readonly String Name;
        public readonly String DisplayName;
        public readonly float Rainfall;
        public readonly float Temperature;

        public Biome(JsonElement jsonElement)
        {
            Id = jsonElement.GetProperty("id").GetInt32();
            DisplayName = jsonElement.GetProperty("displayName").GetString();
            Name = jsonElement.GetProperty("name").GetString();
            Rainfall = jsonElement.GetProperty("rainfall").GetSingle();
            Temperature = jsonElement.GetProperty("temperature").GetSingle();
        }
    }
}