using System;
using System.Linq;
using Modding.Blocks;
using Mod4LJT.Regulation;


namespace Mod4LJT.Blocks
{
    class BlockScriptManager : SingleInstance<BlockScriptManager>
    {
        public void AddBlockScript(Block block)
        {
            if (!StatMaster.levelSimulating)
            {
                switch (block.Prefab.InternalObject.Type)
                {
                    case BlockType.StartingBlock:
                        this.AddHealthController(block);
                        this.AddTankTypeMenu(block);
                        break;
                    case BlockType.Bomb:
                        MToggle weakPointToggle = block.InternalObject.AddToggle(new MToggle("Weak Point", "weak_point", false));
                        weakPointToggle.Toggled += x =>
                        {
                            if (x)
                            {
                                if (!block.GameObject.GetComponent<WeakPointBomb>())
                                {
                                    block.GameObject.AddComponent<WeakPointBomb>();
                                    Mod.Log("Added weak point");
                                }
                            }
                            else
                            {
                                if (block.GameObject.GetComponent<WeakPointBomb>())
                                {
                                    Destroy(block.GameObject.GetComponent<WeakPointBomb>());
                                    Mod.Log("Removed weak point");
                                }
                            }
                        };
                        break;
                    case BlockType.Grabber:
                        block.GameObject.AddComponent<GrabberFix>();
                        break;
                }
            }
        }

        void AddHealthController(Block block)
        {
            if (block.Machine.InternalObject == Machine.Active())
                LJTReferenceMaster.Instance.machineHealthController = block.GameObject.AddComponent<Blocks.MachineHealthController>();
        }

        void AddTankTypeMenu(Block block)
        {
            MMenu tankTypeMenu = block.InternalObject.AddMenu(new MMenu("tankTypeMenu", 0, Enum.GetNames(typeof(TankType)).ToList(), false));
            tankTypeMenu.ValueChanged += x =>
            {
                if (block.Machine.InternalObject == Machine.Active())
                {
                    MachineInspector.Instance.SetTankType((TankType)x);
                }
                if (StatMaster.isMP)
                {
                    //LJTPlayerLabelManager.Instance.ChangeTeamIcon(block.Machine.Player.InternalObject, x);
                    LJTPlayerLabelManager.Instance.SetPlayerTankType(block.Machine.Player.InternalObject, x);
                }
            };
            //MachineInspector.Instance.OnTypeChangeFromGUI += x =>
            //{
            //    if (block.Machine.InternalObject == Machine.Active())
            //    {
            //        tankTypeMenu.SetValue(x);
            //        tankTypeMenu.ApplyValue();
            //        block.InternalObject.OnSave(new XDataHolder());
            //    }
            //};
        }

        public override string Name => "Block Script Manager";
    }
}
