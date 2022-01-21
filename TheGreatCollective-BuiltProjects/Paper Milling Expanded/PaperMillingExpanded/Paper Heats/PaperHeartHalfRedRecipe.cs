using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 4)]
	public class PaperHeartHalfRedRecipe : RecipeFamily
	{
				public PaperHeartHalfRedRecipe()
		{
			base.Recipes = new List<Recipe>
			{
				new Recipe("PaperHeartHalfRed", Localizer.DoStr("Paper Heart Half Red"), new IngredientElement[]
				{
					new IngredientElement(typeof(ColouredPaperRedItem), 6f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
				}, new CraftingElement[]
				{
					new CraftingElement<PaperHeartHalfRedItem>(1f)
				})
			};
			this.ExperienceOnCraft = 2f;
			base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(40f, typeof(PaperMillingSkill));
			base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(PaperHeartHalfRedRecipe), 1f, typeof(PaperMillingSkill), new Type[]
			{
				typeof(PaperMillingFocusedSpeedTalent),
				typeof(PaperMillingParallelSpeedTalent)
			});
			base.Initialize(Localizer.DoStr("Paper Heart Half Red"), typeof(PaperHeartHalfRedRecipe));
			CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
		}
	}
}
