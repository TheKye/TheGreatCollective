using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 3)]
	public class ThreeBalloonsRecipe : RecipeFamily
	{
				public ThreeBalloonsRecipe()
		{
			Recipe item = new Recipe("ThreeBalloons", Localizer.DoStr("Three Balloons"), new IngredientElement[]
			{
				new IngredientElement("ColouredPaper", 30f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(ProcessedPaperItem), 10f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
			}, new CraftingElement[]
			{
				new CraftingElement<ThreeBalloonsItem>(1f)
			});
			base.Recipes = new List<Recipe>
			{
				item
			};
			this.ExperienceOnCraft = 1f;
			base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(75f);
			base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(BlueBalloonRecipe), 1f, typeof(PaperMillingSkill), new Type[]
			{
				typeof(PaperMillingFocusedSpeedTalent),
				typeof(PaperMillingParallelSpeedTalent)
			});
			base.Initialize(Localizer.DoStr("Three Balloons"), typeof(ThreeBalloonsRecipe));
			CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
		}
	}
}
