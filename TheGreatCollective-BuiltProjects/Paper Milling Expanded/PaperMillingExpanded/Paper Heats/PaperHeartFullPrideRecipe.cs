using System;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 4)]
	public class PaperHeartFullPrideRecipe : Recipe
	{
				public PaperHeartFullPrideRecipe()
		{
			Recipe recipe = new Recipe("PaperHeartFullPride", Localizer.DoStr("Paper Heart Full Pride"), new IngredientElement[]
			{
				new IngredientElement(typeof(ColouredPaperGreenItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(ColouredPaperYellowItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(ColouredPaperRedItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(ColouredPaperPinkItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(ColouredPaperPurpleItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(ColouredPaperBlueItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
			}, new CraftingElement[]
			{
				new CraftingElement<PaperHeartFullPrideItem>(1f)
			});
			CraftingComponent.AddTagProduct(typeof(PaperMillingWorkBenchObject), typeof(PaperHeartFullRedRecipe), recipe);
		}
	}
}
