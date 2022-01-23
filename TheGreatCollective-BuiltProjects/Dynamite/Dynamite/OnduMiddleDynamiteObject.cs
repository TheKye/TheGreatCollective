using System;
using System.Collections.Generic;
using System.Text;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(SolidGroundComponent), null)]
    [RequireComponent(typeof(DynamiteComponent), null)]
    public class OnduMiddleDynamiteObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Medium Dynamite");

        public virtual Type RepresentedItemType => typeof(OnduMiddleDynamiteItem);

        public override InteractResult OnActLeft(InteractionContext context) => InteractResult.NoOp;

        public override InteractResult OnActRight(InteractionContext context)
        {
            bool flag = !this.start;
            InteractResult result;
            if (flag)
            {
                this.start = true;
                base.SetAnimatedState("fumee", true);
                base.GetComponent<DynamiteComponent>(null).Credit(7, 1, 5, 3);
                result = base.GetComponent<DynamiteComponent>(null).Interact(context);
            }
            else
            {
                StringBuilder stringBuilder = new();
                stringBuilder.Append(Localizer.DoStr("Already actived !"));
                ChatManager.ServerMessageToPlayer(new LocString(stringBuilder.ToString()), context.Player.User, (Shared.Services.DefaultChatTags)2, (Shared.Services.MessageCategory)8, false);
                result = InteractResult.Success;
            }
            return result;
        }

        public override InteractResult OnActInteract(InteractionContext context) => InteractResult.NoOp;

        protected override void Initialize() => base.Initialize();

        public override void Destroy() => base.Destroy();

        public bool start = false;

        [Serialized]
        [LocDisplayName("Medium Dynamite")]
        public class OnduMiddleDynamiteItem : WorldObjectItem<OnduMiddleDynamiteObject>
        {
            public override LocString DisplayDescription => Localizer.DoStr("Medium dynamite. It has a destruction diameter of 7 blocks and gives 25% rubbles.");
        }

        [RequiresSkill(typeof(ElectronicsSkill), 1)]
        public class OnduMiddleDynamiteRecipe : RecipeFamily
        {
            public OnduMiddleDynamiteRecipe()
            {
                Recipe item = new("MediumDynamite", Localizer.DoStr("Medium Dynamite"), new IngredientElement[]
                {
                    new IngredientElement(typeof(PaperItem), 6f, typeof(ElectronicsSkill), typeof(ElectronicsLavishResourcesTalent)),
                    new IngredientElement("Rock", 10f, typeof(ElectronicsSkill), typeof(ElectronicsLavishResourcesTalent)),
                    new IngredientElement(typeof(CoalItem), 12f, typeof(ElectronicsSkill), typeof(ElectronicsLavishResourcesTalent))
                }, new CraftingElement[]
                {
                    new CraftingElement<OnduMiddleDynamiteObject.OnduMiddleDynamiteItem>(1f)
                });
                base.Recipes = new List<Recipe>
                {
                    item
                };
                this.ExperienceOnCraft = 1f;
                base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(100f, typeof(ElectronicsSkill));
                base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(OnduMiddleDynamiteObject.OnduMiddleDynamiteRecipe), 1f, typeof(ElectronicsSkill), new Type[]
                {
                    typeof(ElectronicsFocusedSpeedTalent),
                    typeof(ElectronicsParallelSpeedTalent)
                });
                base.Initialize(Localizer.DoStr("Medium Dynamite"), typeof(OnduMiddleDynamiteObject.OnduMiddleDynamiteRecipe));
                CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
            }
        }
    }
}
