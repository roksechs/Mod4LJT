using Modding.Blocks;
using System.Collections.Generic;


namespace Mod4LJT.Blocks
{
    class BlockIntialiser : SingleInstance<BlockIntialiser>
    {
        public override string Name => "LJTBlockIntialiser";

        public void AddBlockScript(Block block)
        {
            BlockBehaviour blockBehaviour = block.InternalObject;
            switch (blockBehaviour.Prefab.Type)
            {
                case BlockType.StartingBlock:
                    LJTMachine ljtMachine;
                    if ((ljtMachine = blockBehaviour.ParentMachine.gameObject.GetComponent<LJTMachine>()) == null)
                    {
                        ljtMachine = blockBehaviour.ParentMachine.gameObject.AddComponent<LJTMachine>();
                        ljtMachine.Machine = blockBehaviour.ParentMachine;
                    }
                    MMenu tankTypeMenu = blockBehaviour.AddMenu(new MMenu("tankTypeMenu", 5, new List<string>() {
                        Properties.Text.LightTank, Properties.Text.MediumTank, Properties.Text.HeavyTank,
                        Properties.Text.Destroyer, Properties.Text.SelfPropelledArtillery, Properties.Text.JunkTank,
                    }, false));
                    ljtMachine.TankTypeMenu = tankTypeMenu;
                    if (blockBehaviour.ParentMachine.isLocalMachine)
                    {
                        Mod.machineInspectorUI.LJTMachine = ljtMachine;
                        Mod.machineInspectorUI.OnTypeChangeFromGUI += x =>
                        {
                            tankTypeMenu.SetValue(x);
                            tankTypeMenu.ApplyValue();
                            blockBehaviour.OnSave(new XDataHolder());
                        };
                    }
                    break;
                case BlockType.Bomb:
                    MToggle weakPointToggle = blockBehaviour.AddToggle(new MToggle("Weak Point", "weak_point", false));
                    WeakPointBomb weakPoint = blockBehaviour.gameObject.AddComponent<WeakPointBomb>();
                    weakPointToggle.Toggled += isActive =>
                    {
                        EntryPoint.Log(isActive.ToString());
                        weakPoint.isActive = isActive;
                    };
                    if (StatMaster.isMP)
                    {
                        weakPoint.machineDamageController = (blockBehaviour.ParentMachine as ServerMachine).DamageController;
                    }
                    break;
                case BlockType.Grabber:
                    GrabberFix grabberFix = blockBehaviour.gameObject.AddComponent<GrabberFix>();
                    grabberFix.ljtMachine = blockBehaviour.ParentMachine.gameObject.GetComponent<LJTMachine>();
                    break;
            }
        }
    }
}
