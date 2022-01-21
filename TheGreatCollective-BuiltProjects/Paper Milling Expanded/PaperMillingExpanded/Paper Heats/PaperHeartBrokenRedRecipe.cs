using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 4)]
	public class PaperHeartBrokenRedRecipe : RecipeFamily
	{
				public PaperHeartBrokenRedRecipe()
		{
			base.Recipes = new List<Recipe>
			{
				new Recipe("PaperHeartBrokenRed", Localizer.DoStr("Paper Heart Broken Red"), new IngredientElement[]
				{
					new IngredientElement(typeof(ColouredPaperRedItem), 6f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
				}, new CraftingElement[]
				{
					new CraftingElement<PaperHeartBrokenRedItem>(1f)
				})
			};
			this.ExperienceOnCraft = 2f;
			base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(40f, typeof(PaperMillingSkill));
			base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(PaperHeartBrokenRedRecipe), 1f, typeof(PaperMillingSkill), new Type[]
			{
				typeof(PaperMillingFocusedSpeedTalent),
				typeof(PaperMillingParallelSpeedTalent)
			});
			base.Initialize(Localizer.DoStr("Paper Heart Broken Red"), typeof(PaperHeartBrokenRedRecipe));
			CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
		}
	}
}
