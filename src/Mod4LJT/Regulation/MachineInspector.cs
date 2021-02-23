using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modding;

namespace Mod4LJT.Regulation
{
    //delegate void TypeChangeHandler(int index);
    delegate void CannonCountHandler(int count);
    delegate void ShrapnelCannonCountHandler(int count);

    class MachineInspector : SingleInstance<MachineInspector>
    {
        private TankType _tankType;
        public CommonRegulation regulation;
        private Machine machine;
        private bool hasCompliance;
        private Dictionary<int, CommonRegulation> regulations = new Dictionary<int, CommonRegulation>()
        {
            { (int) TankType.LightTank, LightTank.Instance },
            { (int) TankType.MediumTank, MediumTank.Instance },
            { (int) TankType.HeavyTank, HeavyTank.Instance },
            { (int) TankType.Destroyer, Destroyer.Instance },
            { (int) TankType.Artillery, Mod4LJT.Regulation.Artillery.Instance },
        };
        //int _tankTypeInt = 0;
        bool hudToggle = true;
        bool minimise = false;
        Rect windowRect = new Rect(25f, 125f, 200f, 10f);
        int cannonCount = 0;
        int shrapnelCannonCount = 0;
        //public event TypeChangeHandler OnTypeChangeFromGUI;
        public event CannonCountHandler OnCannonCountChange;
        public event ShrapnelCannonCountHandler OnShrapnelCannonCountChange;

        void Awake()
        {
            this.SetTankType(TankType.LightTank);
            StartCoroutine(CheckVersion());
        }

        private IEnumerator CheckVersion()
        {
            yield return new WaitForSeconds(1f);
            Mod.Log("version " + Mods.GetVersion(new Guid("4713d96a-ce6c-4556-8bf4-7dc838b52973")));
        }

        public void SetTankType(TankType tankType)
        {
            this.machine = Machine.Active();
            this._tankType = tankType;
            //this._tankTypeInt = (int)tankType;
            this.regulations.TryGetValue((int)_tankType, out this.regulation);
            BoundResetter.Instance.refresh = true;
        }

        public void Update()
        {
            if (InputManager.ToggleHUDKey())
            {
                this.hudToggle = !this.hudToggle;
            }
            if (this.minimise)
                this.windowRect.size = new Vector2(200f, 10f);
        }

        public void OnGUI()
        {
            if (StatMaster.SimulationState >= SimulationState.GlobalSimulation || StatMaster.inMenu || StatMaster.isMainMenu || (StatMaster.SimulationState == SimulationState.SpectatorMode && StatMaster.isMP)) return;
            if (hudToggle)
            {
                this.hasCompliance = true;
                this.windowRect = GUILayout.Window(32575339, this.windowRect, new GUI.WindowFunction(this.Mapper), "LJT Regulation");
            }
        }

        public void Mapper(int windowId)
        {
            if (!this.minimise)
            {
                this.RegulationLabels();
            }
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            this.minimise = GUILayout.Toggle(this.minimise, "Minimise");
            GUILayout.EndHorizontal();
            GUI.DragWindow();
        }

        public void RegulationLabels()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Tank Type", GUILayout.Width(150f));
            GUILayout.Label(this._tankType.ToString(), GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            //GUILayout.BeginHorizontal();
            //GUILayout.Toolbar(this._tankTypeInt, Enum.GetNames(typeof(TankType)));
            //if (this._tankTypeInt != (int)this._tankType)
            //    this.OnTypeChangeFromGUI(this._tankTypeInt);
            //GUILayout.EndHorizontal();
            GUILayout.Space(15f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Block", GUILayout.Width(150f));
            GUILayout.Label("Minimum", GUILayout.Width(100f));
            GUILayout.Label("Maximum", GUILayout.Width(100f));
            GUILayout.Label("Current", GUILayout.Width(100f));
            GUILayout.Label("Judgement", GUILayout.Width(100f));
            GUILayout.EndHorizontal();
            foreach (var kvp in this.regulation.ChildBlockRestriction)
            {
                if(kvp.Key == (int)BlockType.Propeller)
                {
                    int num3 = this.GetNumOfBlock((int)BlockType.Propeller) + this.GetNumOfBlock((int)BlockType.SmallPropeller);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label((ReferenceMaster.TranslateBlockName((BlockType)kvp.Key)), GUILayout.Width(150f));
                    GUILayout.Label(kvp.Value.minNum.ToString(), GUILayout.Width(100f));
                    GUILayout.Label(kvp.Value.maxNum.ToString(), GUILayout.Width(100f));
                    GUILayout.Label(num3.ToString(), GUILayout.Width(100f));
                    bool flag3 = num3 >= kvp.Value.minNum && num3 <= kvp.Value.maxNum;
                    this.hasCompliance &= flag3;
                    GUILayout.Label(flag3 ? "OK" : "NO", GUILayout.Width(100f));
                    GUILayout.EndHorizontal();
                }
                else if(kvp.Key == (int)BlockType.SmallPropeller || kvp.Key == (int)BlockType.BuildEdge || kvp.Key == (int)BlockType.BuildNode)
                {
                    continue;
                }
                else
                {
                    int num2 = this.GetNumOfBlock(kvp.Key);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label((ReferenceMaster.TranslateBlockName((BlockType)kvp.Key)), GUILayout.Width(150f));
                    GUILayout.Label(kvp.Value.minNum.ToString(), GUILayout.Width(100f));
                    GUILayout.Label(kvp.Value.maxNum.ToString(), GUILayout.Width(100f));
                    GUILayout.Label(num2.ToString(), GUILayout.Width(100f));
                    bool flag1 = num2 >= kvp.Value.minNum && num2 <= kvp.Value.maxNum;
                    this.hasCompliance &= flag1;
                    GUILayout.Label(flag1 ? "OK" : "NO", GUILayout.Width(100f));
                    GUILayout.EndHorizontal();
                    if (kvp.Key == (int)BlockType.Cannon)
                    {
                        if (this.cannonCount != num2)
                        {
                            this.cannonCount = num2;
                            this.OnCannonCountChange(this.cannonCount);
                        }
                    }
                    else if (kvp.Key == (int)BlockType.ShrapnelCannon)
                    {
                        if (this.shrapnelCannonCount != num2)
                        {
                            this.shrapnelCannonCount = num2;
                            this.OnShrapnelCannonCountChange(this.shrapnelCannonCount);
                        }
                    }
                }
            }
            GUILayout.Space(15f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Machine (All)", GUILayout.Width(150f));
            GUILayout.Label(0.ToString(), GUILayout.Width(100f));
            GUILayout.Label(this.regulation.MaxBlockCount.ToString(), GUILayout.Width(100f));
            GUILayout.Label(this.machine.DisplayBlockCount.ToString(), GUILayout.Width(100f));
            bool flag2 = this.regulation.MaxBlockCount >= this.machine.DisplayBlockCount;
            this.hasCompliance &= flag2;
            GUILayout.Label(this.hasCompliance ? "OK" : "NO", GUILayout.Width(100f));
            GUILayout.EndHorizontal();
        }

        public int GetNumOfBlock(int blockId)
        {
            int index = 0;
            this.machine.BuildingBlocks.ForEach(BB => 
            {
                if (BB.BlockID == blockId)
                {
                    if (blockId == (int)BlockType.Rocket)
                    {
                        if ((BB as TimedRocket).PowerSlider.Value > 0.5f)
                            index++;
                    }
                    else
                    {
                        index++;
                    }
                }
            });
            return index;
        }

        public override string Name => "MachineInspector";
    }
}
