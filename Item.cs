using System;
using System.Text.Json;

namespace dotnet_minecraft_data
{
    public class Item
    {
        public readonly int Id;
        public readonly String DisplayName;
        public readonly String Name;
        public readonly int StackSize;

        public Item(JsonElement jsonElement)
        {
            Id = jsonElement.GetProperty("id").GetInt32();
            DisplayName = jsonElement.GetProperty("displayName").GetString();
            Name = jsonElement.GetProperty("name").GetString();
            StackSize = jsonElement.GetProperty("stackSize").GetInt32();
        }
    }
}