using Mod4LJT.Blocks;
using Mod4LJT.ModLocalisation;
using Modding.Blocks;
using System.Collections.Generic;
using UnityEngine;

namespace Mod4LJT.Regulation
{
    delegate void TypeChangeDelegate(int index);

    class MachineInspectorUI : SingleInstance<MachineInspectorUI>, Localisation.ILocalisationAware
    {
        LJTMachine ljtMachine;
        bool hasCompliance;
        public int weakPointCount = 0;
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
        bool hudToggle = true;
        bool minimise = false;
        bool uf = false;
        bool playerStats = false;
        bool openURL = false;
        Rect windowRect = new Rect(25f, 125f, 525f, 100f);
        Rect windowRect2 = new Rect(700f, 400f, 400f, 100f);
        Rect windowRect3 = new Rect(700f, 125f, 400f, 50f);
        float labelWidth1 = 160f;
        float labelWidth2 = 75f;
        float labelWidth3 = 100f;
        readonly GUIStyle noStyle = new GUIStyle();
        readonly GUIStyle defaultStyle = new GUIStyle();
        readonly GUIStyle nameStyle = new GUIStyle();

        public LJTMachine LJTMachine { get => this.ljtMachine; set => this.ljtMachine = value; }

        public override string Name => "MachineInspectorUI";

        public event TypeChangeDelegate OnTypeChangeFromGUI;

        void Start()
        {
            this.SetLanguage();
            StatMaster.hudHiddenChanged += () => this.hudToggle = !this.hudToggle;
            this.defaultStyle.wordWrap = true;
            this.nameStyle.wordWrap = true;
            this.defaultStyle.alignment = TextAnchor.MiddleCenter;
            this.nameStyle.alignment = TextAnchor.MiddleLeft;
            this.noStyle.alignment = TextAnchor.MiddleCenter;
            this.noStyle.normal.textColor = Color.red;
            this.defaultStyle.normal.textColor = Color.white;
            this.nameStyle.normal.textColor = Color.white;
        }

        void SetLanguage()
        {
            switch (OptionsMaster.BesiegeConfig.Language)
            {
                case "English":
                default:
                    LocalisationFile.languageInt = 1;
                    break;
                case "Japanese":
                    LocalisationFile.languageInt = 2;
                    break;
            }
        }

        public void Update()
        {
            if (this.minimise)
                this.windowRect.size = new Vector2(200f, 10f);
            if (this.openURL)
            {
                Application.OpenURL("https://scrapbox.io/bsgjapancom-28777692/Lazy%E5%BC%8F%E3%82%B8%E3%83%A3%E3%83%B3%E3%82%AF%E3%82%BF%E3%83%B3%E3%82%AF%EF%BC%88LJT%EF%BC%89");
                this.openURL = false;
            }
        }

        public void OnGUI()
        {
            if (StatMaster.PlayMode.Equals(BesiegePlayMode.BuildMode) && this.hudToggle)
            {
                foreach (BlockCount blockCount in this.restrictedBlocksDic.Values)
                {
                    blockCount.currentCount = 0;
                    blockCount.highestPowerValue = 0;
                }
                this.weakPointCount = 0;
                this.windowRect = GUILayout.Window(32575339, this.windowRect, new GUI.WindowFunction(this.MainWindow), Properties.Resources.Title);
                if (this.uf)
                    this.windowRect2 = GUILayout.Window(32575340, this.windowRect2, new GUI.WindowFunction(this.UsageAndFunction), LocalisationFile.GetTranslatedString("UF"));
                if (this.playerStats)
                    this.windowRect3 = GUILayout.Window(32575341, this.windowRect3, new GUI.WindowFunction(this.StatusWindow), Properties.Resources.PlayerStats);

            }
        }

        public void UsageAndFunction(int windowId)
        {
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF1"), this.nameStyle);
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF2"), this.nameStyle);
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF3"), this.nameStyle);
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF4"), this.nameStyle);
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF5"), this.nameStyle);
            GUILayout.Label("・" + LocalisationFile.GetTranslatedString("UF6"), this.nameStyle);
            GUI.DragWindow();
        }

        public void StatusWindow(int windowId)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(Properties.Resources.Player, this.defaultStyle, GUILayout.Width(this.labelWidth3));
            GUILayout.Label(Properties.Resources.MachineAll, this.defaultStyle, GUILayout.Width(this.labelWidth3));
            GUILayout.Label(Properties.Resources.TankType, this.defaultStyle, GUILayout.Width(this.labelWidth3));
            GUILayout.Label(Properties.Resources.Compliance, this.defaultStyle, GUILayout.Width(this.labelWidth3));
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            foreach (KeyValuePair<Machine, LJTMachine> kvp in LJTMachine.MachineDic)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(Playerlist.GetPlayer(kvp.Key.PlayerID).name, this.defaultStyle, GUILayout.Width(this.labelWidth3));
                GUILayout.Label(kvp.Key.Name, this.defaultStyle, GUILayout.Width(this.labelWidth3));
                GUILayout.Label(LocalisationFile.GetTranslatedString(kvp.Value.TankType.ToString()), this.defaultStyle, GUILayout.Width(this.labelWidth3));
                GUILayout.Label(kvp.Value.HasCompliance ? "OK" : "NO", kvp.Value.HasCompliance ? this.defaultStyle : this.noStyle, GUILayout.Width(this.labelWidth3));
                GUILayout.EndHorizontal();
            }
            GUI.DragWindow();
        }

        public void MainWindow(int windowId)
        {
            this.hasCompliance = true;
            this.InspectBlocks();
            GUILayout.FlexibleSpace();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal();
            this.openURL = GUILayout.Button(LocalisationFile.GetTranslatedString("Link"));
            GUILayout.FlexibleSpace();
            this.playerStats = GUILayout.Toggle(this.playerStats, Properties.Resources.PlayerStats);
            GUILayout.Space(15f);
            this.uf = GUILayout.Toggle(this.uf, LocalisationFile.GetTranslatedString("UF"));
            GUILayout.Space(15f);
            this.minimise = GUILayout.Toggle(this.minimise, Properties.Resources.Minimise);
            GUILayout.EndHorizontal();
            GUI.DragWindow(new Rect(0, 0, 10000f, 20f));
        }

        public void InspectBlocks()
        {
            if (this.ljtMachine)
            {
                if (!this.minimise)
                {
                    GUILayout.Space(5f);
                    GUILayout.BeginHorizontal();
                    int tankTypeInt = GUILayout.SelectionGrid((int)this.ljtMachine.TankType, new string[] {
                        Properties.Resources.LightTank, Properties.Resources.MediumTank, Properties.Resources.HeavyTank,
                        Properties.Resources.Destroyer, Properties.Resources.SelfPropelledArtillery, Properties.Resources.JunkTank,
                    }, 3);
                    if (this.ljtMachine.TankType.Equals((TankType)tankTypeInt))
                        this.OnTypeChangeFromGUI(tankTypeInt);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(5f);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(Properties.Resources.Block, this.nameStyle, GUILayout.Width(this.labelWidth1));
                    GUILayout.Label(Properties.Resources.Minimum, this.defaultStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.Label(Properties.Resources.Maximum, this.defaultStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.Label(Properties.Resources.Current, this.defaultStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.Label(Properties.Resources.Power, this.defaultStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.Label(Properties.Resources.Judge, this.defaultStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.EndHorizontal();
                    GUILayout.Space(5f);
                }
                foreach (BlockBehaviour BB in this.ljtMachine.Machine.BuildingBlocks)
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
                            WeakPointBomb weakPointBomb;
                            if ((weakPointBomb = BB.gameObject.GetComponent<WeakPointBomb>()) != null && weakPointBomb.isActive)
                                this.weakPointCount++;

                            continue;
                    }
                }
                foreach (KeyValuePair<int, BlockCount> kvp in this.restrictedBlocksDic)
                {
                    int min;
                    int max;
                    int current;
                    bool powerFlag;
                    bool judge;
                    switch (kvp.Key)
                    {
                        case (int)BlockType.Propeller:
                            min = this.ljtMachine.Regulation.BlockRestriction[kvp.Key].minCount;
                            max = this.ljtMachine.Regulation.BlockRestriction[kvp.Key].maxCount;
                            current = kvp.Value.currentCount + this.restrictedBlocksDic[(int)BlockType.SmallPropeller].currentCount;
                            judge = powerFlag = kvp.Value.highestPowerValue <= this.ljtMachine.Regulation.BlockRestriction[kvp.Key].maxPowers[0];
                            judge &= current >= min && current <= max;
                            this.hasCompliance &= judge;
                            if (this.minimise) continue;
                            GUILayout.BeginHorizontal();
                            GUILayout.Label(ReferenceMaster.TranslateBlockName((BlockType)kvp.Key).ToUpper(), this.nameStyle, GUILayout.Width(this.labelWidth1));
                            GUILayout.Label(min.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(max.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(current.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Space(this.labelWidth2);
                            GUILayout.Label(judge ? "OK" : "NO", judge ? this.defaultStyle : this.noStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.EndHorizontal();
                            continue;
                        case (int)BlockType.SmallPropeller:
                        case (int)BlockType.BuildEdge:
                        case (int)BlockType.BuildNode:
                            continue;
                        case (int)BlockType.Cannon:
                            min = this.ljtMachine.Regulation.BlockRestriction[kvp.Key].minCount;
                            max = this.ljtMachine.Regulation.BlockRestriction[kvp.Key].maxCount;
                            current = kvp.Value.currentCount;
                            if (current == 0)
                            {
                                judge = powerFlag = kvp.Value.highestPowerValue <= this.ljtMachine.Regulation.BlockRestriction[kvp.Key].maxPowers[0];
                            }
                            else if (current <= max)
                            {
                                judge = powerFlag = kvp.Value.highestPowerValue <= this.ljtMachine.Regulation.BlockRestriction[kvp.Key].maxPowers[current - 1];
                            }
                            else
                            {
                                judge = powerFlag = false;
                            }
                            judge &= current >= min;
                            this.hasCompliance &= judge;
                            if (this.minimise) continue;
                            GUILayout.BeginHorizontal();
                            GUILayout.Label((ReferenceMaster.TranslateBlockName((BlockType)kvp.Key).ToUpper()), this.nameStyle, GUILayout.Width(this.labelWidth1));
                            GUILayout.Label(min.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(max.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(current.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(powerFlag ? "OK" : "NO", powerFlag ? this.defaultStyle : this.noStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(judge ? "OK" : "NO", judge ? this.defaultStyle : this.noStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.EndHorizontal();
                            continue;
                        case (int)BlockType.ShrapnelCannon:
                            min = this.ljtMachine.Regulation.BlockRestriction[kvp.Key].minCount;
                            max = this.ljtMachine.Regulation.BlockRestriction[kvp.Key].maxCount;
                            current = kvp.Value.currentCount;
                            judge = powerFlag = kvp.Value.highestPowerValue <= this.ljtMachine.Regulation.BlockRestriction[kvp.Key].maxPowers[0];
                            judge &= current >= min && current <= max;
                            this.hasCompliance &= judge;
                            if (this.minimise) continue;
                            GUILayout.BeginHorizontal();
                            GUILayout.Label(ReferenceMaster.TranslateBlockName((BlockType)kvp.Key).ToUpper(), this.nameStyle, GUILayout.Width(this.labelWidth1));
                            GUILayout.Label(min.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(max.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(current.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(powerFlag ? "OK" : "NO", powerFlag ? this.defaultStyle : this.noStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(judge ? "OK" : "NO", judge ? this.defaultStyle : this.noStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.EndHorizontal();
                            continue;
                        default:
                            min = this.ljtMachine.Regulation.BlockRestriction[kvp.Key].minCount;
                            max = this.ljtMachine.Regulation.BlockRestriction[kvp.Key].maxCount;
                            current = kvp.Value.currentCount;
                            judge = powerFlag = kvp.Value.highestPowerValue <= this.ljtMachine.Regulation.BlockRestriction[kvp.Key].maxPowers[0];
                            judge &= current >= min && current <= max;
                            this.hasCompliance &= judge;
                            if (this.minimise) continue;
                            GUILayout.BeginHorizontal();
                            GUILayout.Label(ReferenceMaster.TranslateBlockName((BlockType)kvp.Key).ToUpper(), this.nameStyle, GUILayout.Width(this.labelWidth1));
                            GUILayout.Label(min.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(max.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(current.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                            if (this.ljtMachine.Regulation.BlockRestriction[kvp.Key].maxPowers[0] == 0)
                                GUILayout.Space(this.labelWidth2);
                            else
                                GUILayout.Label(powerFlag ? "OK" : "NO", powerFlag ? this.defaultStyle : this.noStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.Label(judge ? "OK" : "NO", judge ? this.defaultStyle : this.noStyle, GUILayout.Width(this.labelWidth2));
                            GUILayout.EndHorizontal();
                            continue;
                    }
                }
                if (!this.minimise)
                {
                    GUILayout.Space(5f);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(Properties.Resources.WeakPointBomb, this.nameStyle, GUILayout.Width(this.labelWidth1));
                    GUILayout.Label(1.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.Label(1.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.Label(this.weakPointCount.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.Space(this.labelWidth2);
                }
                bool flag4 = this.weakPointCount == 1;
                this.hasCompliance &= flag4;
                if (!this.minimise)
                {
                    GUILayout.Label(flag4 ? "OK" : "NO", flag4 ? this.defaultStyle : this.noStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.EndHorizontal();
                    GUILayout.Space(5f);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(Properties.Resources.MachineAll, this.nameStyle, GUILayout.Width(this.labelWidth1));
                    GUILayout.Label(0.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.Label(this.ljtMachine.Regulation.MaxBlockCount.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.Label(this.ljtMachine.Machine.DisplayBlockCount.ToString(), this.defaultStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.Space(this.labelWidth2);
                }
                bool flag2 = this.ljtMachine.Regulation.MaxBlockCount >= this.ljtMachine.Machine.DisplayBlockCount;
                this.hasCompliance &= flag2;
                if (!this.minimise)
                {
                    GUILayout.Label(this.hasCompliance ? "OK" : "NO", this.hasCompliance ? this.defaultStyle : this.noStyle, GUILayout.Width(this.labelWidth2));
                    GUILayout.EndHorizontal();
                }
                this.ljtMachine.HasCompliance = this.hasCompliance;
            }
            else
            {
                GUILayout.Label(LocalisationFile.GetTranslatedString("Caution"), this.defaultStyle);
            }
        }

        public void OnLocalisationChange()
        {
            this.SetLanguage();
        }
    }
}
