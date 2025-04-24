using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cslight.ДЗ.TaskReceipt;

namespace Cslight.ДЗ
{
    internal class TaskReceipt
    {
        static void Main(string[] args)
        {
        Author author = new Author("Вася", DateTime.Now);
        Recipe gavno = new Recipe("Борщ", "всё в воду и помешать", "борщевые ингридиенты", author, "сварить и съесть", 0.2, RecipeCategory.Soup);
        Recipe protein = new Recipe("Протеин", "молоко + протик", "протик и мовочко", author, "миксером смешать", 9.8, RecipeCategory.Cocktail);
        }
        private class Recipe
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Ingredients { get; set; }
            public string CookingAlgorithm { get; set; }
            public Author Author { get; set; }
            public double Rating { get; set; }
            public RecipeCategory Category { get; set; }
            public Recipe
                (string name, 
                string description, 
                string ingredient, 
                Author author, 
                string cookingAlgorithm, 
                double rating,
                RecipeCategory category)
            {
                Name = name;
                Description = description;
                Ingredients = ingredient;
                Author = author;
                CookingAlgorithm = cookingAlgorithm;
                Rating = rating;
                Category = category;
                Author.AddRecipe(this);
            }
        }
        public enum RecipeCategory
        {
            Soup,
            Dessert,
            Cocktail,
            MainDish,
            Snack,
            AlсoholCocktail
        }
        class Author
        {
            public string Name { get; set; }
            public DateTime CreationDate { get; set; }
            public List<Recipe> Recipes { get; set; } = new List<Recipe>();
            public double AverageRating
            {
                get
                {
                    if (Recipes.Count == 0)
                    {
                        return 0;
                    }
                    double total = 0;
                    foreach (var recipe in Recipes)
                    {
                        total += recipe.Rating;
                    }
                    return total / Recipes.Count;
                }
            }
            public Author(string name, DateTime creationDate)
            {
                Name = name;
                CreationDate = creationDate;
            }
            public void AddRecipe(Recipe recipe)
            {
                Recipes.Add(recipe);
            }
        }
    }
}
