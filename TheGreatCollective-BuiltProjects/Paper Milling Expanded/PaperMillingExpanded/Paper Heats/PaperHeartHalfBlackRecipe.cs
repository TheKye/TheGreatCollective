using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 4)]
	public class PaperHeartHalfBlackRecipe : Recipe
	{
				public PaperHeartHalfBlackRecipe()
		{
			base.Name = "PaperHeartHalfBlack";
			base.DisplayName = Localizer.DoStr("Paper Heart Half Black");
			base.Ingredients = new List<IngredientElement>
			{
				new IngredientElement(typeof(ColouredPaperBlackItem), 6f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
			};
			base.Items = new List<CraftingElement>
			{
				new CraftingElement<PaperHeartHalfBlackItem>(1f)
			};
			CraftingComponent.AddTagProduct(typeof(PaperMillingWorkBenchObject), typeof(PaperHeartHalfRedRecipe), this);
		}
	}
}
