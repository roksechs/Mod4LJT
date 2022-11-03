using System;
using System.Collections.Generic;

namespace Mod4LJT.Regulation
{
    abstract class CommonRegulation
    {
        public abstract int MaxBlockCount { get; }
        public abstract Dictionary<int, BlockRestriction> BlockRestriction { get; }

        protected Dictionary<int, BlockRestriction> blockRestriction;

        public CommonRegulation()
        {
            this.blockRestriction = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Bomb, new BlockRestriction(1, 1) },
                { (int) BlockType.Cannon, new BlockRestriction(0, 2, 0.1f, new float[]{ 4f, 4f }) },
                { (int) BlockType.ShrapnelCannon, new BlockRestriction(0, 4, 0.1f, 4f) },
                { (int) BlockType.WaterCannon, new BlockRestriction(0, 2, 0.1f, 4f) },
                { (int) BlockType.Flamethrower,  new BlockRestriction(0, 0)},
                { (int) BlockType.Crossbow,  new BlockRestriction(0, 0)},
                { (int) BlockType.Rocket,  new BlockRestriction(0, this.MaxBlockCount, 0.5f, 4f)},
                { (int) BlockType.Log,  new BlockRestriction(0, this.MaxBlockCount)},
                { (int) BlockType.CogMediumPowered, new BlockRestriction(0, this.MaxBlockCount, 0f, 2f) },
                { (int) BlockType.Wheel, new BlockRestriction(0, this.MaxBlockCount, 0f, 2f) },
                { (int) BlockType.LargeWheel, new BlockRestriction(0, this.MaxBlockCount, 0f, 2f) },
                { (int) BlockType.Grabber, new BlockRestriction(0, 8) },
                { (int) BlockType.Propeller,  new BlockRestriction(0, this.MaxBlockCount)},
                { (int) BlockType.SmallPropeller,  new BlockRestriction(0, this.MaxBlockCount)},
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

        private Func<List<BlockBehaviour>, bool> generateCheckFunc(int min, int max, Dictionary<int, float[]> powerDic)
        {
            return delegate (List<BlockBehaviour> blockBehaviours)
            {
                bool countOk = blockBehaviours.Count >= min && blockBehaviours.Count <= max;
                foreach(BlockBehaviour blockBehaviour in blockBehaviours)
                {

                }
                return false;
            };
        }
    }
}
