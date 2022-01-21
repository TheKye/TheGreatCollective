using System;
using System.Collections.Generic;
using Eco.EM.Artistry;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
	[RequiresSkill(typeof(PaperMillingSkill), 7)]
	public class LandClaimPaperRecipe : RecipeFamily
	{
		public LandClaimPaperRecipe()
		{
			base.Recipes = new List<Recipe>
			{
				new Recipe("LandClaimPaper", Localizer.DoStr("Land Claim Paper"), new IngredientElement[]
				{
					new IngredientElement(typeof(GoldBarItem), 1f, true),
					new IngredientElement(typeof(BlueDyeItem), 2f, true),
					new IngredientElement(typeof(YellowDyeItem), 2f, true),
					new IngredientElement(typeof(BlackDyeItem), 2f, true),
					new IngredientElement(typeof(ClothItem), 1f, true),
					new IngredientElement(typeof(ProcessedPaperItem), 50f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
					new IngredientElement(typeof(PaperItem), 100f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
				}, new CraftingElement[]
				{
					new CraftingElement<PropertyClaimItem>(1f)
				})
			};
			this.ExperienceOnCraft = 1f;
			base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(100f, typeof(PaperMillingSkill));
			base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(LandClaimPaperRecipe), 20f, typeof(PaperMillingSkill), new Type[]
			{
				typeof(PaperMillingFocusedSpeedTalent),
				typeof(PaperMillingParallelSpeedTalent)
			});
			base.Initialize(Localizer.DoStr("Land Claim Paper"), typeof(LandClaimPaperRecipe));
			CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
		}
	}
}
