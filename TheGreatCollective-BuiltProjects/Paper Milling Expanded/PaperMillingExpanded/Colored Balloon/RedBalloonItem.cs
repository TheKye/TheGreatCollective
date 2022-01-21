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

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Red Balloon")]
    [Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
    [Tag("Housing", 1)]
    [Tag("Balloon", 1)]
    public class RedBalloonItem : WorldObjectItem<RedBalloonObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("For celebrating.");

        [TooltipChildren]
        public HomeFurnishingValue HousingTooltip => RedBalloonItem.HousingVal;

        [TooltipChildren]
        public static HomeFurnishingValue HousingVal => new()
        {
            Category = 0,
            SkillValue = 0.2f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnPercent = 0.1f
        };
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(HousingComponent), null)]
    [RequireComponent(typeof(SolidGroundComponent), null)]
    public class RedBalloonObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Red Balloon");

        public override TableTextureMode TableTexture => TableTextureMode.Paper;

        public virtual Type RepresentedItemType => typeof(RedBalloonItem);

        protected override void Initialize()
        {
            base.GetComponent<HousingComponent>(null).HomeValue = RedBalloonItem.HousingVal;
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }

    [RequiresSkill(typeof(PaperMillingSkill), 3)]
    public class RedBalloonRecipe : Recipe
    {
        public RedBalloonRecipe()
        {
            base.Name = "RedBalloon";
            base.DisplayName = Localizer.DoStr("Red Balloon");
            base.Ingredients = new List<IngredientElement>
            {
                new IngredientElement(typeof(ColouredPaperRedItem), 4f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(PlantFibersItem), 10f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
            };
            base.Items = new List<CraftingElement>
            {
                new CraftingElement<RedBalloonItem>(1f)
            };
            CraftingComponent.AddTagProduct(typeof(PaperMillingWorkBenchObject), typeof(BlueBalloonRecipe), this);
        }
    }
}
