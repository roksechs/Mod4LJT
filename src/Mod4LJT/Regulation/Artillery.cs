using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod4LJT.Regulation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    namespace Mod4LJT.Regulation
    {
        class Artillery : global::Mod4LJT.Regulation.Regulation
        {
            private static readonly Artillery instance = new Artillery();
            public static Artillery Instance => instance;

            public override Dictionary<int, BlockRestriction> ChildBlockRestriction => this.blockRestrictions;
            public TankType tankType = TankType.Artillery;
            new public Dictionary<int, BlockRestriction> blockRestrictions;

            public Artillery()
            {
                this.blockRestrictions = new Dictionary<int, BlockRestriction>()
            {
                { (int) BlockType.Bomb,  new BlockRestriction(0, int.MaxValue)},
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
