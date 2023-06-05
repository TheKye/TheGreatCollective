namespace Eco.Mods.TechTree
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Eco.Gameplay.Systems.Chat;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.Networking;
    using Eco.Gameplay.Systems.Messaging.Chat.Commands;
    using Vector3 = System.Numerics.Vector3;
    using System.ComponentModel;

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(StockpileComponent))]
    [RequireComponent(typeof(WorldStockpileComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(StorageComponent))]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
    [ChatCommandHandler]
    public partial class ConveyorBeltObject : WorldObject
    {
        public static readonly Vector3i DefaultDim = new(1, 1, 1);
        public override LocString DisplayName => Localizer.DoStr("Conveyor Belt");
        public virtual Type RepresentedItemType => typeof(ConveyorBeltItem);

        private readonly PeriodicUpdate updateThrottle = new(2, true);
        private Orientation _orientation = Orientation.NORTH;

        private PublicStorageComponent front = null;
        private PublicStorageComponent back = null;

        private enum Orientation
        {
            NORTH = 0,
            EAST,
            SOUTH,
            WEST
        };

        protected override void Initialize()
        {
            base.Initialize();
            Vector3 rot = this.Rotation.Right;

            if (rot.X == 1)
                _orientation = Orientation.NORTH;
            else if (rot.Z < -0.9 && rot.Z > -1.1)
                _orientation = Orientation.EAST;
            else if (rot.X == -1)
                _orientation = Orientation.SOUTH;
            else if (rot.Z > 0.9 && rot.Z < 1.1)
                _orientation = Orientation.WEST;


            this.GetComponent<StockpileComponent>().Initialize(DefaultDim);

            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(DefaultDim.x * DefaultDim.z);
            storage.Storage.AddInvRestriction(new StockpileStackRestriction(DefaultDim.y)); // limit stack sizes to the y-height of the stockpile
            this.GetComponent<LinkComponent>().Initialize(1);
        }

        public override void Tick()
        {
            try
            {
                if (updateThrottle.DoUpdate)
                {
                    if (this.IsConveyorEmpty())
                        PullFromBack();
                    else
                        PushFront();
                }
            }
            catch (Exception)
            {
                front = null;
                back = null;
            }
        }

        private void UpdateBlock(bool inverted)
        {
            Vector3i wantedPosition;
            if ((!inverted && _orientation == Orientation.NORTH) || (inverted && _orientation == Orientation.SOUTH))
                wantedPosition = new Vector3i(this.Position3i.x, this.Position3i.y, this.Position3i.z + 1);
            else if ((!inverted && _orientation == Orientation.EAST) || (inverted && _orientation == Orientation.WEST))
                wantedPosition = new Vector3i(this.Position3i.x + 1, this.Position3i.y, this.Position3i.z);
            else if ((!inverted && _orientation == Orientation.SOUTH) || (inverted && _orientation == Orientation.NORTH))
                wantedPosition = new Vector3i(this.Position3i.x, this.Position3i.y, this.Position3i.z - 1);
            else if ((!inverted && _orientation == Orientation.WEST) || (inverted && _orientation == Orientation.EAST))
                wantedPosition = new Vector3i(this.Position3i.x - 1, this.Position3i.y, this.Position3i.z);
            else
                wantedPosition = new Vector3i(this.Position3i.x, this.Position3i.y, this.Position3i.z);

            // check front then front-top then front-bottom
            Vector3i blockTop = wantedPosition + new Vector3i(0, 1, 0);
            Vector3i blockBottom = wantedPosition + new Vector3i(0, -1, 0);

            IEnumerable<WorldObject> list = NetObjectManager.Default.GetObjectsWithin(this.Position, 7f).OfType<WorldObject>();
            if (list == null) return;

            foreach (WorldObject item in list)
            {
                if (item == null) return;

                List<Vector3i> occupancy = item.WorldOccupancy;
                if (occupancy == null) return;

                foreach (Vector3i position in occupancy)
                {
                    if (!inverted)
                    {
                        if (front == null && (position == wantedPosition || position == blockTop || position == blockBottom))
                        {
                            front = item.GetComponent<PublicStorageComponent>();
                            // ChatManager.ServerMessageToAll(Localizer.Format("UPDATE FRONT {0}", front), true);
                            return;
                        }
                    }
                    else
                    {
                        if (back == null && (position == wantedPosition || position == blockTop || position == blockBottom))
                        {
                            back = item.GetComponent<PublicStorageComponent>();
                            // ChatManager.ServerMessageToAll(Localizer.Format("UPDATE BACK {0}", back), true);
                            return;
                        }
                    }
                }
            }
        }

        private void PushFront()
        {
            if (front == null || !(front is PublicStorageComponent) || !front.Enabled)
            {
                front = null;
                this.UpdateBlock(false);
            }
            PublicStorageComponent our = this.GetComponent<PublicStorageComponent>();
            MoveFromTo(our, front);
        }
        private void PullFromBack()
        {
            if (back == null || !(back is PublicStorageComponent) || !back.Enabled)
            {
                back = null;
                this.UpdateBlock(true);
            }
            PublicStorageComponent our = this.GetComponent<PublicStorageComponent>();
            MoveFromTo(back, our);
        }

        private static ItemStack GetFirstItemStackNotEmpty(IEnumerable<ItemStack> stacks)
        {
            foreach (ItemStack stack in stacks)
            {
                if (stack != null && !stack.Empty()) return stack;
            }
            return null;
        }

        private static void MoveFromTo(PublicStorageComponent from, PublicStorageComponent to)
        {
            if (from == null || to == null) return;

            Inventory fromStorage = from.Storage;
            if (fromStorage == null) return;

            Inventory toStorage = to.Storage;
            if (toStorage == null) return;

            MoveFromTo(fromStorage, toStorage);
        }
        private static void MoveFromTo(Inventory from, Inventory to)
        {
            if (from == null || to == null) return;

            IEnumerable<ItemStack> stacks = from.Stacks;
            if (stacks == null) return;

            ItemStack stack = GetFirstItemStackNotEmpty(stacks);
            if (stack == null) return;

            Item itemToGive = stack.Item;
            if (itemToGive == null) return;

            int itemQuantity = DefaultDim.y;
            from.TryMoveItems<Item>(itemToGive.Type, itemQuantity, to);
        }

        private bool IsConveyorEmpty()
        {
            Inventory our = this.GetComponent<PublicStorageComponent>().Storage;
            return our.IsEmpty;
        }
    }
}