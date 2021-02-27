using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod4LJT
{
    class LJTReferenceMaster : SingleInstance<LJTReferenceMaster>
    {
        public override string Name => "LJTReferenceMaster";

        public Blocks.MachineHealthController machineHealthController;
    }
}
