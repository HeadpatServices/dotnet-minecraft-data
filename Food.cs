using System;
using System.Text.Json;

namespace dotnet_minecraft_data
{
    public class Food
    {
        public readonly int Id;
        public readonly String DisplayName;
        public readonly String Name;
        public readonly int StackSize;
        public readonly int FoodPoints;
        public readonly float Saturation;
        public readonly float EffectiveQuality;
        public readonly float SaturationRatio;

        public Food(JsonElement jsonElement)
        {
            Id = jsonElement.GetProperty("id").GetInt32();
            DisplayName = jsonElement.GetProperty("displayName").GetString();
            Name = jsonElement.GetProperty("name").GetString();
            StackSize = jsonElement.GetProperty("stackSize").GetInt32();
            FoodPoints = jsonElement.GetProperty("foodPoints").GetInt32();
            Saturation = jsonElement.GetProperty("saturation").GetSingle();
            EffectiveQuality = jsonElement.GetProperty("effectiveQuality").GetSingle();
            SaturationRatio = jsonElement.GetProperty("saturationRatio").GetSingle();
        }
    }
}