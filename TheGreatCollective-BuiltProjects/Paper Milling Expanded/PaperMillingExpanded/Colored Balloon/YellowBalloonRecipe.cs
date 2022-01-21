using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 3)]
	public class YellowBalloonRecipe : Recipe
	{
				public YellowBalloonRecipe()
		{
			base.Name = "YellowBalloon";
			base.DisplayName = Localizer.DoStr("Yellow Balloon");
			base.Ingredients = new List<IngredientElement>
			{
				new IngredientElement(typeof(ColouredPaperYellowItem), 4f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(PlantFibersItem), 10f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
			};
			base.Items = new List<CraftingElement>
			{
				new CraftingElement<YellowBalloonItem>(1f)
			};
			CraftingComponent.AddTagProduct(typeof(PaperMillingWorkBenchObject), typeof(BlueBalloonRecipe), this);
		}
	}
}
