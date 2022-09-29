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

            public override Dictionary<int, BlockRestriction> ChildBlockRestriction => this.blockRestrictions;
            public TankType tankType = TankType.Artillery;
            new public Dictionary<int, BlockRestriction> blockRestrictions;

            public Artillery()
            {
                this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Bomb,  new BlockRestriction(5, this.MaxBlockCount)},
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

}
