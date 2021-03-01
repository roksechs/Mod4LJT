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
            if (StatMaster.levelSimulating) return;
            switch (block.Prefab.InternalObject.Type)
            {
                case BlockType.StartingBlock:
                    this.AddTankTypeMenu(block);
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

        void AddTankTypeMenu(Block block)
        {
            MMenu tankTypeMenu = block.InternalObject.AddMenu(new MMenu("tankTypeMenu", 5, Enum.GetNames(typeof(TankType)).ToList(), false));
            tankTypeMenu.ValueChanged += x =>
            {
                if (block.Machine.InternalObject == Machine.Active())
                {
                    MachineInspector.Instance.SetTankType((TankType)x);
                }
                if (StatMaster.isMP)
                {
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
