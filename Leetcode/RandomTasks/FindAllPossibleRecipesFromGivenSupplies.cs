using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

// https://leetcode.com/problems/find-all-possible-recipes-from-given-supplies/

namespace LeetCodeSolutions.RandomTasks
{
	[TestClass]
	public class FindAllPossibleRecipesFromGivenSupplies
	{
		[TestMethod]
		public void Solve()
		{
			string[] recipies = new[] { "bread" };
			IList<IList<string>> ingredients = new List<IList<string>>()
			{
				new List<string>()
				{
					"yeast", "flour"
				}
			};

			string[] supplies = new[] {"yeast", "flour", "corn"};

			var result = FindAllRecipes(recipies, ingredients, supplies);

			result.Count.ShouldBe(1);
		}

		[TestMethod]
		public void Solve2()
		{
			string[] recipies = new[] { "bread", "sandwich"};
			IList<IList<string>> ingredients = new List<IList<string>>()
			{
				new List<string>()
				{
					"yeast", "flour"
				},
				new List<string>()
				{
					"bread","meat"
				},
			};

			string[] supplies = new[] { "yeast", "flour", "meat" };

			var result = FindAllRecipes(recipies, ingredients, supplies);

			result.Count.ShouldBe(2);
		}

		[TestMethod]
		public void Solve3()
		{
			string[] recipies = new[] { "bread", "sandwich", "burger" };
			IList<IList<string>> ingredients = new List<IList<string>>()
			{
				new List<string>()
				{
					"yeast", "flour"
				},
				new List<string>()
				{
					"bread","meat"
				},
				new List<string>()
				{
					"sandwich","meat","bread"
				},
			};

			string[] supplies = new[] { "yeast", "flour", "meat" };

			var result = FindAllRecipes(recipies, ingredients, supplies);

			result.Count.ShouldBe(3);
		}

		[TestMethod]
		public void Solve4()
		{
			string[] recipies = new[] { "xevvq", "izcad", "p", "we", "bxgnm", "vpio", "i", "hjvu", "igi", "anp", "tokfq", "z", "kwdmb", "g", "qb", "q", "b", "hthy" };
			IList<IList<string>> ingredients = new List<IList<string>>()
			{
				new List<string> {"wbjr"},
				new List<string> {"otr", "fzr", "g"},
				new List<string> {"fzr", "wi", "otr", "xgp", "wbjr", "igi", "b"},
				new List<string> {"fzr", "xgp", "wi", "otr", "tokfq", "izcad", "igi", "xevvq", "i", "anp"},
				new List<string> {"wi", "xgp", "wbjr"},
				new List<string> {"wbjr", "bxgnm", "i", "b", "hjvu", "izcad", "igi", "z", "g"},
				new List<string> {"xgp", "otr", "wbjr"},
				new List<string> {"wbjr", "otr"},
				new List<string> {"wbjr", "otr", "fzr", "wi", "xgp", "hjvu", "tokfq", "z", "kwdmb"},
				new List<string> {"xgp", "wi", "wbjr", "bxgnm", "izcad", "p", "xevvq"},
				new List<string> {"bxgnm"},
				new List<string> {"wi", "fzr", "otr", "wbjr"},
				new List<string> {"wbjr", "wi", "fzr", "xgp", "otr", "g", "b", "p"},
				new List<string> {"otr", "fzr", "xgp", "wbjr"},
				new List<string> {"xgp", "wbjr", "q", "vpio", "tokfq", "we"},
				new List<string> {"wbjr", "wi", "xgp", "we"},
				new List<string> {"wbjr"},
				new List<string> {"wi"}
			};

			string[] supplies = new[] { "wi", "otr", "wbjr", "fzr", "xgp" };

			var result = FindAllRecipes(recipies, ingredients, supplies);

			result.Count.ShouldBe(10);
		}

		public IList<string> FindAllRecipes(string[] recipes, IList<IList<string>> ingredients, string[] supplies)
		{
			// Convert recipes to HashSet to search easily which is used later on
			HashSet<string> recipesSet = new HashSet<string>();

			// Find the indegree of each ingredient
			Dictionary<string, List<string>> ingredientsToRecipes = new();
			Dictionary<string, int> indegreeMap = new();

			// For each of the recipes
			for (int i = 0; i < recipes.Length; i++)
			{
				// Get the current recipe
				string recipe = recipes[i];

				recipesSet.Add(recipe);

				foreach (var ingredient in ingredients[i])
				{
					// Add the ingredients for each recipe to form a ingredient to recipe map
					//ingredientsToRecipes.computeIfAbsent(ingredient, j->new ArrayList()).add(recipe);

					if (!ingredientsToRecipes.ContainsKey(ingredient))
					{
						ingredientsToRecipes[ingredient] = new List<string>()
						{
							recipe
						};
					}
					else
					{
						ingredientsToRecipes[ingredient].Add(recipe);
					}

					// Increment the indegree count for each of the recipes
					indegreeMap[recipe] = indegreeMap.GetValueOrDefault(recipe, 0) + 1;
				}
			}

			Queue<string> suppliesQ = new();

			// Add the initially available supplies (these are not dependent on anything)
			foreach (var supply in supplies)
			{
				suppliesQ.Enqueue(supply);
			}

			/*
			 IMP: When an recipe is built, it becomes a supply for a future recipe
			  
			 We pull each supply from the initial queue and see which recipe can be 
			 built completely from those supplies. Once a recipe is built, we add
			 that recipe as a supply in the queue so that another recipe dependent on
			 this built recipe can also be built later on
			*/

			List<string> finalRecipes = new();
			while (suppliesQ.Count > 0)
			{
				string currentSupplyCumRecipe = suppliesQ.Dequeue(); // Get a new supply/recipe/ingredient (which is built and not dependent on anyone)

				if (recipesSet.Contains(currentSupplyCumRecipe))
				{ // i.e. this is a recipe and not just a standalone supply
					finalRecipes.Add(currentSupplyCumRecipe);
				}

				// Check which recipes are dependent on this ingredient/supply which has already been built and is independent now
				if (ingredientsToRecipes.ContainsKey(currentSupplyCumRecipe))
				{
					// For each of those recipes
					foreach (var recipe in ingredientsToRecipes[currentSupplyCumRecipe])
					{

						// Decrement the indegree count
						int indegreeCount = indegreeMap[recipe] - 1;

						// If indegreeCount is 0, that means this recipe is now completely built and independent
						if (indegreeCount == 0)
						{
							indegreeMap.Remove(recipe);
							suppliesQ.Enqueue(recipe); // Add this recipe as a supply for other recipes
						}
						else
						{
							indegreeMap[recipe] = indegreeCount; // Add the decremented indegree count
						}
					}
				}
			}

			return finalRecipes;
		}
	}
}
