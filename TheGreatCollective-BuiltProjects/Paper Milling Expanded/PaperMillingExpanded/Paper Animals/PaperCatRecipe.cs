using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 5)]
	public class PaperCatRecipe : RecipeFamily
	{
				public PaperCatRecipe()
		{
			Recipe item = new Recipe("PaperCat", Localizer.DoStr("Paper Cat"), new IngredientElement[]
			{
				new IngredientElement(typeof(ColouredPaperBlackItem), 10f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
			}, new CraftingElement[]
			{
				new CraftingElement<PaperCatItem>(1f)
			});
			base.Recipes = new List<Recipe>
			{
				item
			};
			this.ExperienceOnCraft = 2f;
			base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(40f, typeof(PaperMillingSkill));
			base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(PaperCatRecipe), 3f, typeof(PaperMillingSkill), new Type[]
			{
				typeof(PaperMillingFocusedSpeedTalent),
				typeof(PaperMillingParallelSpeedTalent)
			});
			base.Initialize(Localizer.DoStr("Paper Cat"), typeof(PaperCatRecipe));
			CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
		}
	}
}
