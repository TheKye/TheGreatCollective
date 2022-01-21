using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 4)]
	public class PaperHeartFullRedRecipe : RecipeFamily
	{
				public PaperHeartFullRedRecipe()
		{
			Recipe item = new Recipe("PaperHeartFullRed", Localizer.DoStr("Paper Heart Full Red"), new IngredientElement[]
			{
				new IngredientElement(typeof(ColouredPaperRedItem), 6f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
			}, new CraftingElement[]
			{
				new CraftingElement<PaperHeartFullRedItem>(1f)
			});
			base.Recipes = new List<Recipe>
			{
				item
			};
			this.ExperienceOnCraft = 2f;
			base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(40f, typeof(PaperMillingSkill));
			base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(PaperHeartFullRedRecipe), 1f, typeof(PaperMillingSkill), new Type[]
			{
				typeof(PaperMillingFocusedSpeedTalent),
				typeof(PaperMillingParallelSpeedTalent)
			});
			base.Initialize(Localizer.DoStr("Paper Heart Full Red"), typeof(PaperHeartFullRedRecipe));
			CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
		}
	}
}
