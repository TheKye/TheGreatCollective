using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 0)]
	public class PaperMillingWorkBenchRecipe : RecipeFamily
	{
				public PaperMillingWorkBenchRecipe()
		{
			Recipe item = new Recipe("PaperMillingWorkBench", Localizer.DoStr("Paper Milling Workbench"), new IngredientElement[]
			{
				new IngredientElement("Wood", 10f, false),
				new IngredientElement(typeof(PaperItem), 50f, false),
				new IngredientElement("WoodBoard", 40f, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent))
			}, new CraftingElement[]
			{
				new CraftingElement<PaperMillingWorkBenchItem>(1f)
			});
			base.Recipes = new List<Recipe>
			{
				item
			};
			base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(100f);
			base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(10f);
			base.Initialize(Localizer.DoStr("Paper Milling Workbench"), typeof(PaperMillingWorkBenchRecipe));
			CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
		}
	}
}
