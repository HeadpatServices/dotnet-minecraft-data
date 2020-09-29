using System.Collections.Generic;
using System.Text.Json;

namespace dotnet_minecraft_data
{
    public class RecipeResult
    {
        public readonly int Count;
        public readonly int Id;

        public RecipeResult(int count, int id)
        {
            Count = count;
            Id = id;
        }
    }
    public class Recipe
    {
        public readonly int Id;
        public readonly int[] Ingredients;
        public readonly int[][] IngredientShape;
        public readonly RecipeResult Result;

        public Recipe(JsonElement jsonElement)
        {
            Id = jsonElement.GetProperty("id").GetInt32();
            if (jsonElement.TryGetProperty("ingredients", out JsonElement ingredientsElement))
            {
                List<int> ingredientsList = new List<int>();
                foreach (JsonElement ingredientElement in ingredientsElement.EnumerateArray())
                {
                    ingredientsList.Add(ingredientElement.GetInt32());
                }

                Ingredients = ingredientsList.ToArray();
            }

            if (jsonElement.TryGetProperty("inShape", out JsonElement inShapeElement))
            {
                List<int[]> inShapeList = new List<int[]>();
                foreach (JsonElement layerElement in inShapeElement.EnumerateArray())
                {
                    List<int> layerList = new List<int>();
                    foreach (JsonElement intElement in layerElement.EnumerateArray())
                    {
                        layerList.Add(intElement.GetInt32());
                    }
                    inShapeList.Add(layerList.ToArray());
                }

                IngredientShape = inShapeList.ToArray();
            }

            JsonElement resultElement = jsonElement.GetProperty("result");
            Result = new RecipeResult(resultElement.GetProperty("count").GetInt32(), resultElement.GetProperty("id").GetInt32());
        }
    }
}