using System;
using System.Collections.Generic;
using Eco.Core.Items;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Components;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Objects;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.EM.Artistry;

namespace Eco.Mods.TechTree
{
	[Serialized]
	[LocDisplayName("Coloured Paper Pink")]
	[Weight(125)]
	[Fuel(100f)]
	[Tag("Fuel", 1)]
	[Currency]
	[Tag("Currency", 1)]
	[Ecopedia("Items", "Products", true, display: InPageTooltip.DynamicTooltip)]
	[Tag("ColouredPaper", 1)]
	[Tag("Burnable Fuel", 1)]
	public class ColouredPaperPinkItem : Item
	{
        public override LocString DisplayDescription => Localizer.DoStr("It's pink paper.");
    }

	[RequiresSkill(typeof(PaperMillingSkill), 2)]
	public class ColouredPaperPinkRecipe : Recipe
	{
		public ColouredPaperPinkRecipe()
		{
			base.Name = "ColouredPaperPink";
			base.DisplayName = Localizer.DoStr("Coloured Paper Pink");
			base.Ingredients = new List<IngredientElement>
			{
				new IngredientElement(typeof(PaperItem), 5f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(PinkDyeItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
			};
			base.Items = new List<CraftingElement>
			{
				new CraftingElement<ColouredPaperPinkItem>(3f)
			};
			CraftingComponent.AddTagProduct(typeof(PaperMillingWorkBenchObject), typeof(ColouredPaperGreyRecipe), this);
		}
	}
}
