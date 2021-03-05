using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modding;
using Modding.Blocks;
using Mod4LJT.Localisation;
using Mod4LJT.Blocks;

namespace Mod4LJT.Regulation
{
    delegate void TypeChangeHandler(int index);

    class MachineInspector : SingleInstance<MachineInspector>
    {
        TankType _tankType;
        readonly string[] typeNames = Enum.GetNames(typeof(TankType));
        public string[] translatedNames = new string[6];
        public CommonRegulation regulation;
        Machine machine;
        bool hasCompliance;
        public int weakPointCount = 0;
        public bool isJunkTank = false;
        readonly Dictionary<int, BlockCount> restrictedBlocksDic = new Dictionary<int, BlockCount>()
        {
            { (int) BlockType.Bomb, new BlockCount() },
            { (int) BlockType.Cannon, new BlockCount() },
            { (int) BlockType.ShrapnelCannon, new BlockCount() },
            { (int) BlockType.WaterCannon, new BlockCount() },
            { (int) BlockType.Flamethrower, new BlockCount() },
            { (int) BlockType.Crossbow, new BlockCount() },
            { (int) BlockType.Rocket, new BlockCount() },
            { (int) BlockType.Log, new BlockCount() },
            { (int) BlockType.CogMediumPowered, new BlockCount() },
            { (int) BlockType.Wheel, new BlockCount() },
            { (int) BlockType.LargeWheel, new BlockCount() },
            { (int) BlockType.Grabber, new BlockCount() },
            { (int) BlockType.Propeller, new BlockCount() },
            { (int) BlockType.SmallPropeller, new BlockCount() },
            { (int) BlockType.FlyingBlock, new BlockCount() },
            { (int) BlockType.Vacuum, new BlockCount() },
            { (int) BlockType.SpinningBlock, new BlockCount() },
            { (int) BlockType.CircularSaw, new BlockCount() },
            { (int) BlockType.Drill, new BlockCount() },
            { (int) BlockType.BuildEdge, new BlockCount() },
            { (int) BlockType.BuildNode, new BlockCount() },
            { (int) BlockType.BuildSurface, new BlockCount() },
        };
        readonly Dictionary<int, CommonRegulation> regulations = new Dictionary<int, CommonRegulation>()
        {
            { (int) TankType.LightTank, LightTank.Instance },
            { (int) TankType.MediumTank, MediumTank.Instance },
            { (int) TankType.HeavyTank, HeavyTank.Instance },
            { (int) TankType.Destroyer, Destroyer.Instance },
            { (int) TankType.Artillery, Mod4LJT.Regulation.Artillery.Instance },
            { (int) TankType.JunkTank, JunkTank.Instance },
        };
        int _tankTypeInt = 0;
        string language;
        bool hudToggle = true;
        bool minimise = false;
        bool uf = false;
        bool playerStats = false;
        bool openURL = false;
        Rect windowRect = new Rect(25f, 125f, 525f, 10f);
        Rect windowRect2 = new Rect(700f, 400f, 400f, 10f);
        //Rect windowRect3 = new Rect(700f, 800f, 400, 10f);
        readonly GUIStyle noStyle = new GUIStyle();
        readonly GUIStyle yesStyle = new GUIStyle();
        readonly GUIStyleState noStyleState = new GUIStyleState();
        readonly GUIStyleState yesStyleState = new GUIStyleState();
        public event TypeChangeHandler OnTypeChangeFromGUI;

        void Start()
        {
            this.SetLanguage();
            StatMaster.hudHiddenChanged += () => this.hudToggle = !this.hudToggle;
            StartCoroutine(CheckVersion());
            noStyleState.textColor = Color.red;
            yesStyleState.textColor = Color.white;
            noStyle.normal = noStyleState;
            yesStyle.normal = yesStyleState;
        }

        IEnumerator CheckVersion()
        {
            yield return new WaitForSecondsRealtime(1f);
            Mod.Log("version " + Mods.GetVersion(new Guid("4713d96a-ce6c-4556-8bf4-7dc838b52973")));
        }

        public void SetTankType(TankType tankType)
        {
            this.machine = Machine.Active();
            this._tankType = tankType;
            this._tankTypeInt = (int)tankType;
            this.regulations.TryGetValue((int)_tankType, out this.regulation);
            BoundResetter.Instance.refresh = true;
            if (this._tankTypeInt == 5)
            {
                this.isJunkTank = true;
            }
            else
            {
                this.isJunkTank = false;
            }
        }

        void SetLanguage()
        {
            this.language = OptionsMaster.BesiegeConfig.Language;
            switch (this.language)
            {
                case "English":
                default:
                    LocalisationFile.languageInt = 1;
                    break;
                case "Japanese":
                    LocalisationFile.languageInt = 2;
                    break;
            }
            for (int i = 0; i < 6; i++)
            {
                this.translatedNames[i] = LocalisationFile.GetTranslatedString(typeNames[i]);
            }
        }

        public void Update()
        {
            if (this.language != OptionsMaster.BesiegeConfig.Language)
            {
                this.SetLanguage();
            }
            if (this.minimise)
                this.windowRect.size = new Vector2(200f, 10f);
            if (openURL)
            {
                Application.OpenURL("https://scrapbox.io/bsgjapancom-28777692/Lazy%E5%BC%8F%E3%82%B8%E3%83%A3%E3%83%B3%E3%82%AF%E3%82%BF%E3%83%B3%E3%82%AF%EF%BC%88LJT%EF%BC%89");
                this.openURL = false;
            }
        }

        public void OnGUI()
        {
            if (StatMaster.SimulationState >= SimulationState.GlobalSimulation || StatMaster.inMenu || StatMaster.isMainMenu || (StatMaster.SimulationState == SimulationState.SpectatorMode && StatMaster.isMP)) return;
            if (hudToggle)
            {
                foreach (var kvp in this.restrictedBlocksDic)
                {
                    kvp.Value.currentCount = 0;
                    kvp.Value.highestPowerValue = 0;
                }
                this.weakPointCount = 0;
                this.hasCompliance = true;
                this.windowRect = GUILayout.Window(32575339, this.windowRect, new GUI.WindowFunction(this.MainWindow), LocalisationFile.GetTranslatedString("Title"));
                if (this.uf)
                    this.windowRect2 = GUILayout.Window(32575340, this.windowRect2, new GUI.WindowFunction(this.UsageAndFunction), LocalisationFile.GetTranslatedString("UF"));
            }
        }

        public void UsageAndFunction(int windowId)
        {
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF1"));
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF2"));
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF3"));
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF4"));
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF5"));
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF6"));
            GUI.DragWindow();
        }

        //public void StatusWindow(int windowId)
        //{
        //    GUILayout.BeginHorizontal();
        //    GUILayout.Label(LocalisationFile.GetTranslatedString("Player"), GUILayout.Width(100f));
        //    GUILayout.Label(LocalisationFile.GetTranslatedString("Machine"), GUILayout.Width(100f));
        //    GUILayout.Label(LocalisationFile.GetTranslatedString("TankType"), GUILayout.Width(100f));
        //    GUILayout.Label(LocalisationFile.GetTranslatedString("Compliance"), GUILayout.Width(100f));
        //    GUILayout.EndHorizontal();
        //    foreach (var kvp in LJTMachine.MachineDic)
        //    {
        //        GUILayout.BeginHorizontal();
        //        GUILayout.Label(kvp.Key.Player.Name, GUILayout.Width(100f));
        //        GUILayout.Label(kvp.Key.Name, GUILayout.Width(100f));
        //        GUILayout.Label(LocalisationFile.GetTranslatedString(((TankType)kvp.Value.TankTypeInt).ToString()), GUILayout.Width(100f));
        //        GUILayout.Label(kvp.Value.HasCompliance ? "OK" : "NO", kvp.Value.HasCompliance ? this.yesStyle : this.noStyle, GUILayout.Width(100f));
        //        GUILayout.EndHorizontal();
        //    }
        //}

        public void MainWindow(int windowId)
        {
            if (!this.minimise)
            {
                this.RegulationCheck();
            }
            GUILayout.FlexibleSpace();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal();
            this.openURL = GUILayout.Button(LocalisationFile.GetTranslatedString("Link"));
            GUILayout.FlexibleSpace();
            this.uf = GUILayout.Toggle(this.uf, LocalisationFile.GetTranslatedString("UF"));
            GUILayout.Space(15f);
            this.minimise = GUILayout.Toggle(this.minimise, LocalisationFile.GetTranslatedString("Minimise"));
            GUILayout.EndHorizontal();
            GUI.DragWindow(new Rect(0, 0, 10000f, 20f));
        }

        public void RegulationCheck()
        {
            if (!this.machine)
            {
                GUILayout.Label(LocalisationFile.GetTranslatedString("Caution"));
                return;
            }
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal();
            GUILayout.Label(LocalisationFile.GetTranslatedString("TankType"), GUILayout.Width(150f));
            GUILayout.Label(LocalisationFile.GetTranslatedString(this._tankType.ToString()));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            this._tankTypeInt = GUILayout.SelectionGrid(_tankTypeInt, translatedNames, 3);
            if (this._tankTypeInt != (int)this._tankType)
                this.OnTypeChangeFromGUI(this._tankTypeInt);
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal();
            GUILayout.Label(LocalisationFile.GetTranslatedString("Block"), GUILayout.Width(150f));
            GUILayout.Label(LocalisationFile.GetTranslatedString("Minimum"), GUILayout.Width(75f));
            GUILayout.Label(LocalisationFile.GetTranslatedString("Maximum"), GUILayout.Width(75f));
            GUILayout.Label(LocalisationFile.GetTranslatedString("Current"), GUILayout.Width(75f));
            GUILayout.Label(LocalisationFile.GetTranslatedString("Power"), GUILayout.Width(75f));
            GUILayout.Label(LocalisationFile.GetTranslatedString("Judge"), GUILayout.Width(75f));
            GUILayout.EndHorizontal();
            foreach (BlockBehaviour BB in this.machine.BuildingBlocks)
            {
                if (!this.restrictedBlocksDic.ContainsKey((int)BB.Prefab.Type)) continue;
                this.restrictedBlocksDic[(int)BB.Prefab.Type].currentCount++;
                switch (BB.Prefab.Type)
                {
                    case BlockType.Cannon:
                    case BlockType.ShrapnelCannon:
                        if (this.restrictedBlocksDic[(int)BB.Prefab.Type].highestPowerValue < (BB as CanonBlock).StrengthSlider.Value)
                            this.restrictedBlocksDic[(int)BB.Prefab.Type].highestPowerValue = (BB as CanonBlock).StrengthSlider.Value;
                        continue;
                    case BlockType.CogMediumPowered:
                    case BlockType.Wheel:
                    case BlockType.LargeWheel:
                        if (this.restrictedBlocksDic[(int)BB.Prefab.Type].highestPowerValue < (BB as CogMotorControllerHinge).SpeedSlider.Value)
                            this.restrictedBlocksDic[(int)BB.Prefab.Type].highestPowerValue = (BB as CogMotorControllerHinge).SpeedSlider.Value;
                        continue;
                    case BlockType.Rocket:
                        if ((BB as TimedRocket).PowerSlider.Value <= 0.5f)
                            this.restrictedBlocksDic[(int)BB.Prefab.Type].currentCount--;
                        continue;
                    case BlockType.Bomb:
                        if (BB.gameObject.GetComponent<WeakPointBomb>().isWeakPoint)
                            this.weakPointCount++;
                        continue;
                }
            }
            foreach (var kvp in this.restrictedBlocksDic)
            {
                int min;
                int max;
                int current;
                bool powerFlag;
                bool judge;
                switch (kvp.Key)
                {
                    case (int)BlockType.Propeller:
                        min = this.regulation.ChildBlockRestriction[kvp.Key].minNum;
                        max = this.regulation.ChildBlockRestriction[kvp.Key].maxNum;
                        current = kvp.Value.currentCount + this.restrictedBlocksDic[(int)BlockType.SmallPropeller].currentCount;
                        judge = powerFlag = kvp.Value.highestPowerValue <= this.regulation.ChildBlockRestriction[kvp.Key].maxPowers[0];
                        judge &= current >= min && current <= max;
                        this.hasCompliance &= judge;
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(ReferenceMaster.TranslateBlockName((BlockType)kvp.Key).ToUpper(), GUILayout.Width(150f));
                        GUILayout.Label(min.ToString(), GUILayout.Width(75f));
                        GUILayout.Label(max.ToString(), GUILayout.Width(75f));
                        GUILayout.Label(current.ToString(), GUILayout.Width(75f));
                        GUILayout.Space(75f);
                        GUILayout.Label(judge ? "OK" : "NO", judge ? this.yesStyle : this.noStyle, GUILayout.Width(75f));
                        GUILayout.EndHorizontal();
                        continue;
                    case (int)BlockType.SmallPropeller:
                    case (int)BlockType.BuildEdge:
                    case (int)BlockType.BuildNode:
                        continue;
                    case (int)BlockType.Cannon:
                        min = this.regulation.ChildBlockRestriction[kvp.Key].minNum;
                        max = this.regulation.ChildBlockRestriction[kvp.Key].maxNum;
                        current = kvp.Value.currentCount;
                        if (current == 0)
                        {
                            judge = powerFlag = kvp.Value.highestPowerValue <= this.regulation.ChildBlockRestriction[kvp.Key].maxPowers[0];
                        }
                        else if (current <= max)
                        {
                            judge = powerFlag = kvp.Value.highestPowerValue <= this.regulation.ChildBlockRestriction[kvp.Key].maxPowers[current - 1];
                        }
                        else
                        {
                            judge = powerFlag = false;
                        }
                        judge &= current >= min;
                        this.hasCompliance &= judge;
                        GUILayout.BeginHorizontal();
                        GUILayout.Label((ReferenceMaster.TranslateBlockName((BlockType)kvp.Key).ToUpper()), GUILayout.Width(150f));
                        GUILayout.Label(min.ToString(), GUILayout.Width(75f));
                        GUILayout.Label(max.ToString(), GUILayout.Width(75f));
                        GUILayout.Label(current.ToString(), GUILayout.Width(75f));
                        GUILayout.Label(powerFlag ? "OK" : "NO", powerFlag ? this.yesStyle : this.noStyle, GUILayout.Width(75f));
                        GUILayout.Label(judge ? "OK" : "NO", judge ? this.yesStyle : this.noStyle, GUILayout.Width(75f));
                        GUILayout.EndHorizontal();
                        continue;
                    case (int)BlockType.ShrapnelCannon:
                        min = this.regulation.ChildBlockRestriction[kvp.Key].minNum;
                        max = this.regulation.ChildBlockRestriction[kvp.Key].maxNum;
                        current = kvp.Value.currentCount;
                        judge = powerFlag = kvp.Value.highestPowerValue <= this.regulation.ChildBlockRestriction[kvp.Key].maxPowers[0];
                        judge &= current >= min && current <= max;
                        this.hasCompliance &= judge;
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(ReferenceMaster.TranslateBlockName((BlockType)kvp.Key).ToUpper(), GUILayout.Width(150f));
                        GUILayout.Label(min.ToString(), GUILayout.Width(75f));
                        GUILayout.Label(max.ToString(), GUILayout.Width(75f));
                        GUILayout.Label(current.ToString(), GUILayout.Width(75f));
                        GUILayout.Label(powerFlag ? "OK" : "NO", powerFlag ? this.yesStyle : this.noStyle, GUILayout.Width(75f));
                        GUILayout.Label(judge ? "OK" : "NO", judge ? this.yesStyle : this.noStyle, GUILayout.Width(75f));
                        GUILayout.EndHorizontal();
                        continue;
                    default:
                        min = this.regulation.ChildBlockRestriction[kvp.Key].minNum;
                        max = this.regulation.ChildBlockRestriction[kvp.Key].maxNum;
                        current = kvp.Value.currentCount;
                        judge = powerFlag = kvp.Value.highestPowerValue <= this.regulation.ChildBlockRestriction[kvp.Key].maxPowers[0];
                        judge &= current >= min && current <= max;
                        this.hasCompliance &= judge;
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(ReferenceMaster.TranslateBlockName((BlockType)kvp.Key).ToUpper(), GUILayout.Width(150f));
                        GUILayout.Label(min.ToString(), GUILayout.Width(75f));
                        GUILayout.Label(max.ToString(), GUILayout.Width(75f));
                        GUILayout.Label(current.ToString(), GUILayout.Width(75f));
                        if (this.regulation.ChildBlockRestriction[kvp.Key].maxPowers[0] == 0)
                            GUILayout.Space(75f);
                        else
                            GUILayout.Label(powerFlag ? "OK" : "NO", powerFlag ? this.yesStyle : this.noStyle, GUILayout.Width(75f));
                        GUILayout.Label(judge ? "OK" : "NO", judge ? this.yesStyle : this.noStyle, GUILayout.Width(75f));
                        GUILayout.EndHorizontal();
                        continue;
                }
            }
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal();
            GUILayout.Label(LocalisationFile.GetTranslatedString("WeakPointBomb"), GUILayout.Width(150f));
            GUILayout.Label(1.ToString(), GUILayout.Width(75f));
            GUILayout.Label(1.ToString(), GUILayout.Width(75f));
            GUILayout.Label(this.weakPointCount.ToString(), GUILayout.Width(75f));
            GUILayout.Space(75f);
            bool flag4 = this.weakPointCount == 1;
            this.hasCompliance &= flag4;
            GUILayout.Label(flag4 ? "OK" : "NO", flag4 ? this.yesStyle : this.noStyle, GUILayout.Width(75f));
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal();
            GUILayout.Label(LocalisationFile.GetTranslatedString("Machine(All)"), GUILayout.Width(150f));
            GUILayout.Label(0.ToString(), GUILayout.Width(75f));
            GUILayout.Label(this.regulation.MaxBlockCount.ToString(), GUILayout.Width(75f));
            GUILayout.Label(this.machine.DisplayBlockCount.ToString(), GUILayout.Width(75f));
            GUILayout.Space(75f);
            bool flag2 = this.regulation.MaxBlockCount >= this.machine.DisplayBlockCount;
            this.hasCompliance &= flag2;
            GUILayout.Label(this.hasCompliance ? "OK" : "NO", this.hasCompliance ? this.yesStyle : this.noStyle, GUILayout.Width(75f));
            GUILayout.EndHorizontal();
        }

        public override string Name => "MachineInspector";
    }
}
