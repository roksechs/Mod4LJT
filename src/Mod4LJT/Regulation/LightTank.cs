﻿using System.Collections.Generic;

namespace Mod4LJT.Regulation
{
    class LightTank : CommonRegulation
    {
        public override int MaxBlockCount => 221;
        private static readonly LightTank instance = new LightTank();
        public static LightTank Instance => instance;

        public override Dictionary<int, BlockRestriction> ChildBlockRestriction => this.blockRestrictions;
        public TankType tankType = TankType.LightTank;
        new public Dictionary<int, BlockRestriction> blockRestrictions;

        public LightTank()
        {
            this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Cannon,  new BlockRestriction(0, 3, 0.1f, new float[]{ 8f, 6f, 4f })},
                { (int) BlockType.ShrapnelCannon,  new BlockRestriction(0, 4, 0.1f, 8f)},
                { (int) BlockType.WaterCannon,  new BlockRestriction(0, 6, 0.1f, 4f)},
                { (int) BlockType.Flamethrower,  new BlockRestriction(0, 1)},
                { (int) BlockType.Crossbow,  new BlockRestriction(0, 1)},
                { (int) BlockType.Log,  new BlockRestriction(0, 10)},
                { (int) BlockType.CogMediumPowered,  new BlockRestriction(0, 221, 0f, 4f)},
                { (int) BlockType.Wheel,  new BlockRestriction(0, 221, 0f, 4f)},
                { (int) BlockType.LargeWheel,  new BlockRestriction(0, 221, 0f, 4f)},
                { (int) BlockType.SmallPropeller,  new BlockRestriction(0, 221)},
                { (int) BlockType.Propeller,  new BlockRestriction(0, 221)},
                { (int) BlockType.FlyingBlock,  new BlockRestriction(0, 8, 0f, 20f)},
            };
            foreach (var kvp in base.blockRestrictions)
            {
                if (!this.blockRestrictions.ContainsKey(kvp.Key))
                {
                    this.blockRestrictions.Add(kvp.Key, kvp.Value);
                }
            }
        }
    }
}
