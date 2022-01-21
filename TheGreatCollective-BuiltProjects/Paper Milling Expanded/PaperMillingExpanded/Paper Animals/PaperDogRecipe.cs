using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 4)]
	public class PaperDogRecipe : RecipeFamily
	{
				public PaperDogRecipe()
		{
			Recipe item = new Recipe("Paper Dog", Localizer.DoStr("Paper Dog"), new IngredientElement[]
			{
				new IngredientElement(typeof(ColouredPaperBlackItem), 10f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
			}, new CraftingElement[]
			{
				new CraftingElement<PaperDogItem>(1f)
			});
			base.Recipes = new List<Recipe>
			{
				item
			};
			this.ExperienceOnCraft = 2f;
			base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(40f, typeof(PaperMillingSkill));
			base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(PaperDogRecipe), 3f, typeof(PaperMillingSkill), new Type[]
			{
				typeof(PaperMillingFocusedSpeedTalent),
				typeof(PaperMillingParallelSpeedTalent)
			});
			base.Initialize(Localizer.DoStr("Paper Dog"), typeof(PaperDogRecipe));
			CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
		}
	}
}
