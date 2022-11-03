namespace Mod4LJT.Regulation
{
    using System.Collections.Generic;

    namespace Mod4LJT.Regulation
    {
        class Artillery : CommonRegulation
        {
            public override int MaxBlockCount => 251;
            private static readonly Artillery instance = new Artillery();
            public static Artillery Instance => instance;

            public override Dictionary<int, BlockRestriction> BlockRestriction => this.blockRestriction;

            public Artillery() : base()
            {
                this.blockRestriction[(int)BlockType.Bomb] = new BlockRestriction(5, this.MaxBlockCount);
            }
        }
    }

}
