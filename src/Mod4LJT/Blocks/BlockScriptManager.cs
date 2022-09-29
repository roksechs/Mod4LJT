using Mod4LJT.Regulation;
using Modding.Blocks;
using System;
using System.Collections;
using System.Linq;


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
                    if (StatMaster.isMP)
                    {
                        if (!block.Machine.InternalObject.gameObject.GetComponent<LJTMachine>())
                        {
                            LJTMachine ljtMachine = block.Machine.InternalObject.gameObject.AddComponent<LJTMachine>();
                            ljtMachine.PlayerMachine = block.Machine;
                        }
                    }
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
            tankTypeMenu.ValueChanged += tankTypeInt =>
            {
                if (!StatMaster.levelSimulating)
                {
                    if (block.Machine == PlayerMachine.GetLocal())
                    {
                        MachineInspector.Instance.SetTankType((TankType)tankTypeInt);
                        StartCoroutine(this.SetTankTypeCoroutine(block.Machine, tankTypeInt));
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

        IEnumerator SetTankTypeCoroutine(PlayerMachine playerMachine, int tankTypeInt)
        {
            yield return null;
            if (LJTMachine.MachineDic.TryGetValue(playerMachine, out LJTMachine ljtMachine))
            {
                ljtMachine.TankTypeInt = tankTypeInt;
            }
        }

        public override string Name => "Block Script Manager";
    }
}
