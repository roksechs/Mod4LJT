using System.Collections.Generic;

namespace Mod4LJT.Regulation
{
    class SelfPropelledArtillery : AbstractRegulation
    {
        public override int MaxBlockCount => 251;
        private static readonly SelfPropelledArtillery instance = new SelfPropelledArtillery();
        public static SelfPropelledArtillery Instance => instance;

        public override Dictionary<int, BlockRestriction> BlockRestriction => this.blockRestriction;

        public SelfPropelledArtillery() : base()
        {
            this.blockRestriction[(int)BlockType.Bomb] = new BlockRestriction(5, this.MaxBlockCount);
        }
    }
}
