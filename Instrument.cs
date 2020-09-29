using System;
using System.Text.Json;

namespace dotnet_minecraft_data
{
    public class Instrument
    {
        public readonly int Id;
        public readonly String Name;

        public Instrument(JsonElement jsonElement)
        {
            Id = jsonElement.GetProperty("id").GetInt32();
            Name = jsonElement.GetProperty("name").GetString();
        }
    }
}