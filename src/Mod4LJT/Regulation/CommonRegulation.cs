using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod4LJT.Regulation
{
    abstract class CommonRegulation
    {
        public abstract int MaxBlockCount { get; }
        protected Dictionary<int, BlockRestriction> blockRestrictions;
        public abstract Dictionary<int, BlockRestriction> ChildBlockRestriction { get; }

        public CommonRegulation()
        {
            this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Bomb, new BlockRestriction(1, 1) },
                { (int) BlockType.Cannon, new BlockRestriction(0, 2, new float[]{ 4, 4 }) },
                { (int) BlockType.ShrapnelCannon, new BlockRestriction(0, 4, 4f) },
                { (int) BlockType.CogMediumPowered, new BlockRestriction(0, 251, 2f) },
                { (int) BlockType.WaterCannon, new BlockRestriction(0, 2) },
                { (int) BlockType.Grabber, new BlockRestriction(0, 8) },
                { (int) BlockType.Flamethrower,  new BlockRestriction(0, 0)},
                { (int) BlockType.Crossbow,  new BlockRestriction(0, 0)},
                { (int) BlockType.Propeller,  new BlockRestriction(0, 0)},
                { (int) BlockType.SmallPropeller,  new BlockRestriction(0, 0)},
                { (int) BlockType.FlyingBlock, new BlockRestriction(0, 0) },
                { (int) BlockType.Vacuum, new BlockRestriction(0, 0) },
                { (int) BlockType.SpinningBlock, new BlockRestriction(0, 0) },
                { (int) BlockType.CircularSaw, new BlockRestriction(0, 0) },
                { (int) BlockType.Drill, new BlockRestriction(0, 0) },
                { (int) BlockType.BuildEdge, new BlockRestriction(0, 0) },
                { (int) BlockType.BuildNode, new BlockRestriction(0, 0) },
                { (int) BlockType.BuildSurface, new BlockRestriction(0, 0) },
            };
        }
    }
}
