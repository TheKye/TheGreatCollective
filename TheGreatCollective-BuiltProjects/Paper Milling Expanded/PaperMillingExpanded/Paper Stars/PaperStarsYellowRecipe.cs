using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 4)]
	public class PaperStarsYellowRecipe : RecipeFamily
	{
				public PaperStarsYellowRecipe()
		{
			base.Recipes = new List<Recipe>
			{
				new Recipe("PaperStarsYellow", Localizer.DoStr("Paper Stars Yellow"), new IngredientElement[]
				{
					new IngredientElement(typeof(ColouredPaperYellowItem), 4f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
				}, new CraftingElement[]
				{
					new CraftingElement<PaperStarsYellowItem>(1f)
				})
			};
			this.ExperienceOnCraft = 2f;
			base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(40f, typeof(PaperMillingSkill));
			base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(PaperStarsYellowRecipe), 0.5f, typeof(PaperMillingSkill), new Type[]
			{
				typeof(PaperMillingFocusedSpeedTalent),
				typeof(PaperMillingParallelSpeedTalent)
			});
			base.Initialize(Localizer.DoStr("Paper Stars Yellow"), typeof(PaperStarsYellowRecipe));
			CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
		}
	}
}
