using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 4)]
	public class PaperSunPrideRecipe : Recipe
	{
				public PaperSunPrideRecipe()
		{
			base.Name = "PaperSunPride";
			base.DisplayName = Localizer.DoStr("Paper Sun Pride");
			base.Ingredients = new List<IngredientElement>
			{
				new IngredientElement(typeof(ColouredPaperGreenItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(ColouredPaperYellowItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(ColouredPaperRedItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(ColouredPaperPurpleItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(ColouredPaperBlueItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
			};
			base.Items = new List<CraftingElement>
			{
				new CraftingElement<PaperSunPrideItem>(1f)
			};
			CraftingComponent.AddTagProduct(typeof(PaperMillingWorkBenchObject), typeof(PaperSunYellowRecipe), this);
		}
	}
}
