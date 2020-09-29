using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.VisualBasic;

namespace dotnet_minecraft_data
{
    public enum Edition
    {
        PC,
        PE
    }
    
    public class MinecraftData
    {
        public readonly JsonDocument Blocks, Items, Foods, Biomes, Recipes, Instruments, Materials, Entities, Enchantments, Protocol, Functions, Windows, Version, Effects, Particles, EntityLoot, BlockLoot, Language;


        public MinecraftData(Edition edition, String version)
        {
            var asm = typeof(MinecraftData).GetTypeInfo().Assembly;
            
            foreach (String str in asm.GetManifestResourceNames())
                Console.WriteLine(str);
            
            using (Stream stream =
                asm.GetManifestResourceStream("dotnet_minecraft_data.minecraft_data.data.dataPaths.json"))
            {
                
                byte[] bufferDataPathJson = new byte[stream.Length];
                stream.Read(bufferDataPathJson);

                using (JsonDocument dataPathDocument = JsonDocument.Parse(Encoding.UTF8.GetString(bufferDataPathJson)))
                {
                    JsonElement versionPathElement = dataPathDocument.RootElement.GetProperty(edition.ToString().ToLower()).GetProperty(version);

                    foreach (JsonProperty pathProperty in versionPathElement.EnumerateObject())
                    {
                        JsonElement element = pathProperty.Value;
                        String asmPath =
                            $"dotnet_minecraft_data.minecraft_data.data.{element.GetString().Replace(".", "._").Replace("/", "._")}.{pathProperty.Name}.json";
                        using (Stream dataStream =
                            asm.GetManifestResourceStream(asmPath))
                        {
                            Console.WriteLine($"Reading {asmPath}");
                            byte[] fileBuffer = new byte[dataStream.Length];
                            dataStream.Read(fileBuffer);

                            JsonDocument dataDocument = JsonDocument.Parse(Encoding.UTF8.GetString(fileBuffer));
                            switch (pathProperty.Name)
                            {
                                case "biomes":
                                {
                                    Biomes = dataDocument;
                                    break;
                                }
                                case "blocks":
                                {
                                    Blocks = dataDocument;
                                    break;
                                }
                                case "items":
                                {
                                    Items = dataDocument;
                                    break;
                                }
                                case "foods":
                                {
                                    Foods = dataDocument;
                                    break;
                                }
                                case "recipies":
                                {
                                    Recipes = dataDocument;
                                    break;
                                }
                                case "instruments":
                                {
                                    Instruments = dataDocument;
                                    break;
                                }
                                case "materials":
                                {
                                    Materials = dataDocument;
                                    break;
                                }
                                case "entities":
                                {
                                    Entities = dataDocument;
                                    break;
                                }
                                case "enchantments":
                                {
                                    Enchantments = dataDocument;
                                    break;
                                }
                                case "protocol":
                                {
                                    Protocol = dataDocument;
                                    break;
                                }
                                case "windows":
                                {
                                    Windows = dataDocument;
                                    break;
                                }
                                case "version":
                                {
                                    Version = dataDocument;
                                    break;
                                }
                                case "effects":
                                {
                                    Effects = dataDocument;
                                    break;
                                }
                                case "particles":
                                {
                                    Particles = dataDocument;
                                    break;
                                }
                                case "entityLoot":
                                {
                                    EntityLoot = dataDocument;
                                    break;
                                }
                                case "blockLoot":
                                {
                                    BlockLoot = dataDocument;
                                    break;
                                }
                                case "language":
                                {
                                    Language = dataDocument;
                                    break;
                                }
                                default:
                                {
                                    Console.WriteLine("Missing " + pathProperty.Name);
                                    break;
                                }
                            }
                        }
                   }
                }
            }
        }

        public Dictionary<int, Block> GetBlocks()
        {
            Dictionary<int, Block> blocks = new Dictionary<int, Block>();
            
            foreach (JsonElement blockElement in Blocks.RootElement.EnumerateArray())
            {
                Block block = new Block(blockElement);
                blocks.Add(block.Id, block);
            }

            return blocks;
        }
        
        public Dictionary<String, Block> GetBlocksByName()
        {
            Dictionary<String, Block> blocks = new Dictionary<String, Block>();
            
            foreach (JsonElement blockElement in Blocks.RootElement.EnumerateArray())
            {
                Block block = new Block(blockElement);
                blocks.Add(block.Name, block);
            }

            return blocks;
        } 
        
        public Dictionary<int, Block> GetBlocksByStateId()
        {
            Dictionary<int, Block> blocks = new Dictionary<int, Block>();
            
            foreach (JsonElement blockElement in Blocks.RootElement.EnumerateArray())
            {
                Block block = new Block(blockElement);
                for (int x = block.MinStateId; x <= block.MaxStateId; x++)
                    blocks.Add(x, block);
            }

            return blocks;
        }
        
        

        public List<Block> GetBlocksArray()
        {
            List<Block> blocks = new List<Block>();
            foreach (JsonElement blockElement in Blocks.RootElement.EnumerateArray())
            {
                blocks.Add(new Block(blockElement));
            }

            return blocks;
        }
        
        public Dictionary<int, Item> GetItems()
        {
            Dictionary<int, Item> items = new Dictionary<int, Item>();
            
            foreach (JsonElement itemElement in Items.RootElement.EnumerateArray())
            {
                Item item = new Item(itemElement);
                items.Add(item.Id, item);
            }

            return items;
        }
        
        public Dictionary<String, Item> GetItemsByName()
        {
            Dictionary<String, Item> items = new Dictionary<String, Item>();
            
            foreach (JsonElement itemElement in Items.RootElement.EnumerateArray())
            {
                Item item = new Item(itemElement);
                items.Add(item.Name, item);
            }

            return items;
        }
        
        public List<Item> GetItemsArray()
        {
            List<Item> items = new List<Item>();
            foreach (JsonElement itemElement in Items.RootElement.EnumerateArray())
            {
                items.Add(new Item(itemElement));
            }

            return items;
        }
        
        public Dictionary<int, Food> GetFoods()
        {
            Dictionary<int, Food> foods = new Dictionary<int, Food>();
            
            foreach (JsonElement foodElement in Foods.RootElement.EnumerateArray())
            {
                Food food = new Food(foodElement);
                foods.Add(food.Id, food);
            }

            return foods;
        }
        
        public Dictionary<String, Food> GetFoodsByName()
        {
            Dictionary<String, Food> foods = new Dictionary<String, Food>();
            
            foreach (JsonElement foodElement in Foods.RootElement.EnumerateArray())
            {
                Food food = new Food(foodElement);
                foods.Add(food.Name, food);
            }

            return foods;
        }
        
        public Dictionary<int, Food> GetFoodsByFoodPoints()
        {
            Dictionary<int, Food> foods = new Dictionary<int, Food>();
            
            foreach (JsonElement foodElement in Foods.RootElement.EnumerateArray())
            {
                Food food = new Food(foodElement);
                foods.Add(food.FoodPoints, food);
            }

            return foods;
        }
        
        public Dictionary<float, Food> GetFoodsBySaturation()
        {
            Dictionary<float, Food> foods = new Dictionary<float, Food>();
            
            foreach (JsonElement foodElement in Foods.RootElement.EnumerateArray())
            {
                Food food = new Food(foodElement);
                foods.Add(food.Saturation, food);
            }

            return foods;
        }
        
        public List<Food> GetFoodsArray()
        {
            List<Food> foods = new List<Food>();
            foreach (JsonElement foodElement in Foods.RootElement.EnumerateArray())
            {
                foods.Add(new Food(foodElement));
            }

            return foods;
        }

        public Dictionary<int, Biome> GetBiomes()
        {
            Dictionary<int, Biome> biomes = new Dictionary<int, Biome>();
            
            foreach (JsonElement biomeElement in Biomes.RootElement.EnumerateArray())
            {
                Biome biome = new Biome(biomeElement);
                biomes.Add(biome.Id, biome);
            }

            return biomes;
        }
        
        public List<Biome> GetBiomesArray()
        {
            List<Biome> biomes = new List<Biome>();
            foreach (JsonElement biomesElement in Biomes.RootElement.EnumerateArray())
            {
                biomes.Add(new Biome(biomesElement));
            }

            return biomes;
        }
        
        public Dictionary<int, Recipe> GetRecipes()
        {
            Dictionary<int, Recipe> recipes = new Dictionary<int, Recipe>();
            
            foreach (JsonElement recipeElement in Recipes.RootElement.EnumerateArray())
            {
                Recipe recipe = new Recipe(recipeElement);
                recipes.Add(recipe.Id, recipe);
            }

            return recipes;
        }

        public Dictionary<int, Instrument> GetInstruments()
        {
            Dictionary<int, Instrument> instruments = new Dictionary<int, Instrument>();

            foreach (JsonElement element in Instruments.RootElement.EnumerateArray())
            {
                Instrument instrument = new Instrument(element);
                instruments.Add(instrument.Id, instrument);
            }

            return instruments;
        }

        public List<Instrument> GetInstrumentsArray()
        {
            List<Instrument> instruments = new List<Instrument>();
            foreach (JsonElement element in Instruments.RootElement.EnumerateArray())
            {
                instruments.Add(new Instrument(element));
            }

            return instruments;
        }
    }
}