using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mod4LJT.Blocks
{
    abstract class AbstractBlock : MonoBehaviour
    {
        public BlockBehaviour BB;

        internal virtual void Awake()
        {
            this.BB = this.GetComponent<BlockBehaviour>();
        }
    }
}
