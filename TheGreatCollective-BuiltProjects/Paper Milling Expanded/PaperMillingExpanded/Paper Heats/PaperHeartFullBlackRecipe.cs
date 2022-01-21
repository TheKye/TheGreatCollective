using System;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 4)]
	public class PaperHeartFullBlackRecipe : Recipe
	{
				public PaperHeartFullBlackRecipe()
		{
			Recipe recipe = new Recipe("PaperHeartFullBlack", Localizer.DoStr("Paper Heart Full Black"), new IngredientElement[]
			{
				new IngredientElement(typeof(ColouredPaperBlackItem), 6f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
			}, new CraftingElement[]
			{
				new CraftingElement<PaperHeartFullBlackItem>(1f)
			});
			CraftingComponent.AddTagProduct(typeof(PaperMillingWorkBenchObject), typeof(PaperHeartFullRedRecipe), recipe);
		}
	}
}
