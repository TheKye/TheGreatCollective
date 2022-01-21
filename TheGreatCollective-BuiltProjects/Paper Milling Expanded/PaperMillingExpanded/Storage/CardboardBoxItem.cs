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
    [LocDisplayName("Cardboard Box")]
    [Ecopedia("Crafted Objects", "Storage", true, display: InPageTooltip.DynamicTooltip)]
    public class CardboardBoxItem : WorldObjectItem<CardboardBoxObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A cardboard box you can store items in.");
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(LinkComponent), null)]
    [RequireComponent(typeof(PublicStorageComponent), null)]
    public class CardboardBoxObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Cardboard Box");

        public override TableTextureMode TableTexture => TableTextureMode.Paper;

        public virtual Type RepresentedItemType => typeof(CardboardBoxItem);

        protected override void Initialize()
        {
            PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
            component.Initialize(10);
            component.Storage.AddInvRestriction(new NotCarriedRestriction());
        }

        public override void Destroy() => base.Destroy();

        protected override void PostInitialize()
        {
            base.PostInitialize();
            base.GetComponent<LinkComponent>(null).Initialize(5f);
        }
    }

    [RequiresSkill(typeof(PaperMillingSkill), 2)]
    public class CardboardBoxRecipe : RecipeFamily
    {
        public CardboardBoxRecipe()
        {
            Recipe item = new("CardboardBox", Localizer.DoStr("Cardboard Box"),
                new IngredientElement[]
                {
                    new IngredientElement(typeof(ProcessedPaperItem), 20f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
                },
                new CraftingElement[]
                {
                    new CraftingElement<CardboardBoxItem>(1f)
                });
            base.Recipes = new List<Recipe>
                {
                    item
                };
            base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(25f, typeof(PaperMillingSkill));
            base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(CardboardBoxRecipe), 2f, typeof(PaperMillingSkill), new Type[]
            {
                    typeof(PaperMillingFocusedSpeedTalent),
                    typeof(PaperMillingParallelSpeedTalent)
            });
            base.Initialize(Localizer.DoStr("Cardboard Box"), typeof(CardboardBoxRecipe));
            CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
        }
    }
}
