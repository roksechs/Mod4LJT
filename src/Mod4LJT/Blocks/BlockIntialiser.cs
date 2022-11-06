using Mod4LJT.Regulation;
using Modding.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mod4LJT.Blocks
{
    class BlockIntialiser : SingleInstance<BlockIntialiser>
    {
        public override string Name => "LJTBlockIntialiser";

        public void AddBlockScript(BlockBehaviour blockBehaviour)
        {

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
                        Properties.Resources.LightTank, Properties.Resources.MediumTank, Properties.Resources.HeavyTank,
                        Properties.Resources.Destroyer, Properties.Resources.SelfPropelledArtillery, Properties.Resources.JunkTank,
                    }, false));
                    ljtMachine.TankTypeMenu = tankTypeMenu;
                    if ((blockBehaviour.ParentMachine as ServerMachine).isLocalMachine)
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
