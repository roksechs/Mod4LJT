﻿using System.Collections.Generic;

namespace Mod4LJT.Regulation
{
    class MediumTank : CommonRegulation
    {
        public override int MaxBlockCount => 251;
        private static readonly MediumTank instance = new MediumTank();
        public static MediumTank Instance => instance;

        public override Dictionary<int, BlockRestriction> ChildBlockRestriction => this.blockRestrictions;
        public TankType tankType = TankType.MediumTank;
        new public Dictionary<int, BlockRestriction> blockRestrictions;

        public MediumTank()
        {
            this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Cannon,  new BlockRestriction(0, 4, 0.1f,  new float[]{ 10f, 7f, 5f, 5f })},
                { (int) BlockType.ShrapnelCannon,  new BlockRestriction(0, 4, 0.1f, 10f)},
                { (int) BlockType.Flamethrower,  new BlockRestriction(0, 2)},
                { (int) BlockType.Crossbow,  new BlockRestriction(0, 2)},
                { (int) BlockType.Log,  new BlockRestriction(0, 30)},
                { (int) BlockType.Propeller,  new BlockRestriction(0, 10)},
                { (int) BlockType.SmallPropeller,  new BlockRestriction(0, 10)},
                { (int) BlockType.WaterCannon, new BlockRestriction(0, 4, 0.1f, 4f)},
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
