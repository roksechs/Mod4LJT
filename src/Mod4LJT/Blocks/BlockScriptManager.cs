using Mod4LJT.Regulation;
using Modding.Blocks;
using System;
using System.Linq;


namespace Mod4LJT.Blocks
{
    class BlockScriptManager : SingleInstance<BlockScriptManager>
    {
        public override string Name => "BlockScriptManager";

        public void AddBlockScript(Block block)
        {
            if (!StatMaster.levelSimulating)
            {
                LJTMachine ljtMachine = null;
                switch (block.Prefab.InternalObject.Type)
                {
                    case BlockType.StartingBlock:
                        if (StatMaster.isMP)
                        {
                            if ((ljtMachine = block.Machine.InternalObject.gameObject.GetComponent<LJTMachine>()) == null)
                            {
                                ljtMachine = block.Machine.InternalObject.gameObject.AddComponent<LJTMachine>();
                                ljtMachine.PlayerMachine = block.Machine;
                            }
                        }
                        this.AddTankTypeMenu(block, ljtMachine);
                        break;
                    case BlockType.Bomb:
                        MToggle weakPointToggle = block.InternalObject.AddToggle(new MToggle("Weak Point", "weak_point", false));
                        weakPointToggle.Toggled += flag =>
                        {
                            if (flag)
                            {
                                WeakPointBomb weakPoint = block.GameObject.AddComponent<WeakPointBomb>();
                                weakPoint.machineDamageController = block.Machine.InternalObjectServer.DamageController;
                            }
                        };
                        break;
                    case BlockType.Grabber:
                        GrabberFix grabberFix = block.GameObject.AddComponent<GrabberFix>();
                        grabberFix.ljtMachine = block.Machine.InternalObject.gameObject.GetComponent<LJTMachine>();
                        break;
                }
            }
        }

        private void AddTankTypeMenu(Block block, LJTMachine ljtMachine)
        {
            MMenu tankTypeMenu = block.InternalObject.AddMenu(new MMenu("tankTypeMenu", 5, Enum.GetNames(typeof(TankType)).ToList(), false));
            ljtMachine.tankTypeMenu = tankTypeMenu;
            tankTypeMenu.ValueChanged += tankTypeInt =>
            {
                if (!StatMaster.levelSimulating)
                {
                    if (block.Machine == PlayerMachine.GetLocal())
                    {
                        Mod.machineInspectorUI.SetTankType((TankType)tankTypeInt);
                    }
                }
            };
            Mod.machineInspectorUI.OnTypeChangeFromGUI += x =>
            {
                if (block.Machine == PlayerMachine.GetLocal())
                {
                    EntryPoint.Log("GUI");
                    tankTypeMenu.SetValue(x);
                    tankTypeMenu.ApplyValue();
                    block.InternalObject.OnSave(new XDataHolder());
                }
            };
        }
    }
}
