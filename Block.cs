using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.VisualBasic;

namespace dotnet_minecraft_data
{
    public class BlockState
    {
        public readonly String Name;
        public readonly String Type;
        public readonly int NumValues;
        public readonly String[] Values;

        public BlockState(String name, String type, int numValues, String[] values)
        {
            Name = name;
            Type = type;
            NumValues = numValues;
            Values = values;
        }
    }
    
    public class Block
    {
        public readonly int Id;
        public readonly String DisplayName;
        public readonly String Name;
        public readonly double Hardness;
        public readonly int MinStateId;
        public readonly int MaxStateId;
        public readonly BlockState[] States;
        public readonly int[] Drops;
        public readonly bool Diggable;
        public readonly bool Transparent;
        public readonly int FilterLight;
        public readonly int EmitLight;
        public readonly String BoundingBox;
        public readonly int StackSize;
        public readonly String Material;
        public readonly Dictionary<int, bool> HarvestTools = new Dictionary<int, bool>();
        public readonly int DefaultState;

        public Block(int id, String displayName, String name, double hardness, int minStateId, int maxStateId,
            BlockState[] states, int[] drops, bool diggable, bool transparent, int filterLight, int emitLight,
            String boundingBox, int stackSize, int defaultState, String material = "", Dictionary<int, bool> harvestTools = null)
        {
            Id = id;
            DisplayName = displayName;
            Name = name;
            Hardness = hardness;
            MinStateId = minStateId;
            MaxStateId = maxStateId;
            States = states;
            Drops = drops;
            Diggable = diggable;
            Transparent = transparent;
            FilterLight = filterLight;
            EmitLight = emitLight;
            BoundingBox = boundingBox;
            StackSize = stackSize;
            DefaultState = defaultState;
            Material = material;
            HarvestTools = harvestTools == null ? HarvestTools : harvestTools;
        }

        public Block(JsonElement jsonElement)
        {
            Id = jsonElement.GetProperty("id").GetInt32();
            DisplayName = jsonElement.GetProperty("displayName").GetString();
            Name = jsonElement.GetProperty("name").GetString();
            if (jsonElement.GetProperty("hardness").GetRawText() != "null")
                Hardness = jsonElement.GetProperty("hardness").GetSingle();
            MinStateId = jsonElement.GetProperty("minStateId").GetInt32();
            MaxStateId = jsonElement.GetProperty("maxStateId").GetInt32();
            
            List<BlockState> states = new List<BlockState>();
            foreach (JsonElement stateElement in jsonElement.GetProperty("states").EnumerateArray())
            {
                String name = stateElement.GetProperty("name").GetString();
                String type = stateElement.GetProperty("type").GetString();
                int numVals = stateElement.GetProperty("num_values").GetInt32();
                List<String> values = new List<String>();
                if (stateElement.TryGetProperty("values", out JsonElement valuesArray))
                {
                    foreach (JsonElement valuesElement in valuesArray.EnumerateArray())
                    {
                        values.Add(valuesElement.GetString());
                    }
                }
                states.Add(new BlockState(name, type, numVals, values.ToArray()));
            }
            States = states.ToArray();

            List<int> drops = new List<int>();
            foreach (JsonElement dropsElement in jsonElement.GetProperty("drops").EnumerateArray())
            {
                drops.Add(dropsElement.GetInt32());
            }
            Drops = drops.ToArray();

            Diggable = jsonElement.GetProperty("diggable").GetBoolean();
            Transparent = jsonElement.GetProperty("transparent").GetBoolean();
            FilterLight = jsonElement.GetProperty("filterLight").GetInt32();
            EmitLight = jsonElement.GetProperty("emitLight").GetInt32();
            BoundingBox = jsonElement.GetProperty("boundingBox").GetString();
            StackSize = jsonElement.GetProperty("stackSize").GetInt32();
            if (jsonElement.TryGetProperty("material", out JsonElement materialElement))
                Material = materialElement.GetString();
            if (jsonElement.TryGetProperty("harvestTools", out JsonElement harvestTools))
            {
                foreach (JsonProperty toolProp in harvestTools.EnumerateObject())
                {
                    HarvestTools.Add(Int32.Parse(toolProp.Name), toolProp.Value.GetBoolean());
                }
            }
            DefaultState = jsonElement.GetProperty("defaultState").GetInt32();
        }
    }
}