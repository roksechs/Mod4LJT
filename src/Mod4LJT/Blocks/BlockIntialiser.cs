using Mod4LJT.Regulation;
using Modding.Blocks;
using System;
using System.Linq;


namespace Mod4LJT.Blocks
{
    class BlockIntialiser : SingleInstance<BlockIntialiser>
    {
        public override string Name => "LJTBlockIntialiser";

        public void AddBlockScript(BlockBehaviour block)
        {

            switch (block.Prefab.Type)
            {
                case BlockType.StartingBlock:
                    LJTMachine ljtMachine;
                    if ((ljtMachine = block.ParentMachine.gameObject.GetComponent<LJTMachine>()) == null)
                    {
                        ljtMachine = block.ParentMachine.gameObject.AddComponent<LJTMachine>();
                        ljtMachine.Machine = block.ParentMachine;
                    }
                    MMenu tankTypeMenu = block.AddMenu(new MMenu("tankTypeMenu", 5, Enum.GetNames(typeof(TankType)).ToList(), false));
                    ljtMachine.TankTypeMenu = tankTypeMenu;
                    if ((block.ParentMachine as ServerMachine).isLocalMachine)
                    {
                        Mod.machineInspectorUI.LJTMachine = ljtMachine;
                        Mod.machineInspectorUI.OnTypeChangeFromGUI += x =>
                        {
                            tankTypeMenu.SetValue(x);
                            tankTypeMenu.ApplyValue();
                            block.OnSave(new XDataHolder());
                        };
                    }
                    break;
                case BlockType.Bomb:
                    MToggle weakPointToggle = block.AddToggle(new MToggle("Weak Point", "weak_point", false));
                    WeakPointBomb weakPoint = block.gameObject.AddComponent<WeakPointBomb>();
                    weakPointToggle.Toggled += isActive =>
                    {
                        EntryPoint.Log(isActive.ToString());
                        weakPoint.isActive = isActive;
                    };
                    if (StatMaster.isMP)
                    {
                        weakPoint.machineDamageController = (block.ParentMachine as ServerMachine).DamageController;
                    }
                    break;
                case BlockType.Grabber:
                    GrabberFix grabberFix = block.gameObject.AddComponent<GrabberFix>();
                    grabberFix.ljtMachine = block.ParentMachine.gameObject.GetComponent<LJTMachine>();
                    break;
            }
        }
    }
}
