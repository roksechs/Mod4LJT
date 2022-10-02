using Mod4LJT.Regulation;
using Modding.Blocks;
using System;
using System.Collections;
using System.Linq;


namespace Mod4LJT.Blocks
{
    static class BlockScriptManager
    {
        public static void AddBlockScript(Block block)
        {
            if (StatMaster.levelSimulating) return;
            switch (block.Prefab.InternalObject.Type)
            {
                case BlockType.StartingBlock:
                    LJTMachine ljtMachine = null;
                    if (StatMaster.isMP)
                    {
                        if (!block.Machine.InternalObject.gameObject.GetComponent<LJTMachine>())
                        {
                            ljtMachine = block.Machine.InternalObject.gameObject.AddComponent<LJTMachine>();
                            ljtMachine.PlayerMachine = block.Machine;
                        }
                    }
                    AddTankTypeMenu(block, ljtMachine);
                    break;
                case BlockType.Bomb:
                    WeakPointBomb weakPoint = block.GameObject.AddComponent<WeakPointBomb>();
                    MToggle weakPointToggle = block.InternalObject.AddToggle(new MToggle("Weak Point", "weak_point", false));
                    weakPointToggle.Toggled += x =>
                    {
                        if (x)
                        {
                            weakPoint.isWeakPoint = true;
                        }
                        else
                        {
                            weakPoint.isWeakPoint = false;
                        }
                    };
                    break;
                case BlockType.Grabber:
                    block.GameObject.AddComponent<GrabberFix>();
                    break;
            }
        }

        private static void AddTankTypeMenu(Block block, LJTMachine ljtMachine)
        {
            MMenu tankTypeMenu = block.InternalObject.AddMenu(new MMenu("tankTypeMenu", 5, Enum.GetNames(typeof(TankType)).ToList(), false));
            tankTypeMenu.ValueChanged += tankTypeInt =>
            {
                if (!StatMaster.levelSimulating)
                {
                    if (block.Machine == PlayerMachine.GetLocal())
                    {
                        MachineInspector.Instance.SetTankType((TankType)tankTypeInt);
                        if (ljtMachine != null)
                        {
                            ljtMachine.TankTypeInt = tankTypeInt;
                        }
                    }
                }
            };
            MachineInspector.Instance.OnTypeChangeFromGUI += x =>
            {
                if (block.Machine == PlayerMachine.GetLocal())
                {
                    tankTypeMenu.SetValue(x);
                    tankTypeMenu.ApplyValue();
                    block.InternalObject.OnSave(new XDataHolder());
                }
            };
        }
    }
}
