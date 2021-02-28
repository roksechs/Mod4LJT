using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Modding;
using Mod4LJT.Localisation;
using Mod4LJT.Blocks;

namespace Mod4LJT.Regulation
{
    //delegate void TypeChangeHandler(int index);
    delegate void CannonCountHandler(int count);
    delegate void ShrapnelCannonCountHandler(int count);

    class MachineInspector : SingleInstance<MachineInspector>
    {
        private TankType _tankType;
        private string[] typeNames = Enum.GetNames(typeof(TankType));
        public string[] translatedNames = new string[6];
        public CommonRegulation regulation;
        private Machine machine;
        private bool hasCompliance;
        public int weakPointCount = 0;
        public bool isJunkTank = false;
        private Dictionary<int, CommonRegulation> regulations = new Dictionary<int, CommonRegulation>()
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
        bool openURL = false;
        Rect windowRect = new Rect(25f, 125f, 550f, 10f);
        Rect windowRect2 = new Rect(700f, 400f, 400f, 10f);
        int cannonCount = 0;
        int shrapnelCannonCount = 0;
        //public event TypeChangeHandler OnTypeChangeFromGUI;
        public event CannonCountHandler OnCannonCountChange;
        public event ShrapnelCannonCountHandler OnShrapnelCannonCountChange;

        void Start()
        {
            this.SetLanguage();
            this.SetTankType(TankType.JunkTank);
            StatMaster.hudHiddenChanged += () => this.hudToggle = !this.hudToggle;
            //SceneManager.activeSceneChanged += (x, y) =>
            //{
            //    Mod.Log(y.buildIndex.ToString());
            //    if(y.buildIndex == 9)
            //    {
            //        BinButton binButton = GameObject.Find("HUD/Top/TopBar/Middle/Icons/BinTool").GetComponent<BinButton>();
            //        binButton.OnClicked();
            //    }
            //};
            StartCoroutine(CheckVersion());
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
            if (InputManager.ToggleHUDKey())
            {
                this.hudToggle = !this.hudToggle;
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
                this.hasCompliance = true;
                this.windowRect = GUILayout.Window(32575339, this.windowRect, new GUI.WindowFunction(this.Mapper), LocalisationFile.GetTranslatedString("Title"));
                if (this.uf)
                {
                    this.windowRect2 = GUILayout.Window(32575340, this.windowRect2, new GUI.WindowFunction(this.UsageAndFunction), LocalisationFile.GetTranslatedString("UF"));
                }
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

        public void Mapper(int windowId)
        {
            if (!this.minimise)
            {
                this.RegulationLabels();
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

        public void RegulationLabels()
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
            //this._tankTypeInt = GUILayout.SelectionGrid(_tankTypeInt, translatedNames, 3);
            //if (this._tankTypeInt != (int)this._tankType)
            //    this.OnTypeChangeFromGUI(this._tankTypeInt);
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal();
            GUILayout.Label(LocalisationFile.GetTranslatedString("Block"), GUILayout.Width(150f));
            GUILayout.Label(LocalisationFile.GetTranslatedString("Minimum"), GUILayout.Width(100f));
            GUILayout.Label(LocalisationFile.GetTranslatedString("Maximum"), GUILayout.Width(100f));
            GUILayout.Label(LocalisationFile.GetTranslatedString("Current"), GUILayout.Width(100f));
            GUILayout.Label(LocalisationFile.GetTranslatedString("Judgement"), GUILayout.Width(100f));
            GUILayout.EndHorizontal();
            foreach (var kvp in this.regulation.ChildBlockRestriction)
            {
                if (kvp.Key == (int)BlockType.Propeller)
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
                else if (kvp.Key == (int)BlockType.SmallPropeller || kvp.Key == (int)BlockType.BuildEdge || kvp.Key == (int)BlockType.BuildNode)
                {
                    continue;
                }
                else
                {
                    int num2 = this.GetNumOfBlock(kvp.Key);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label((ReferenceMaster.TranslateBlockName((BlockType)kvp.Key).ToUpperInvariant()), GUILayout.Width(150f));
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
                    else if (kvp.Key == (int)BlockType.Bomb)
                    {
                        this.weakPointCount = this.GetNumOfWeakPoint();
                    }
                }
            }
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal();
            GUILayout.Label(LocalisationFile.GetTranslatedString("WeakPointBomb"), GUILayout.Width(150f));
            GUILayout.Label(1.ToString(), GUILayout.Width(100f));
            GUILayout.Label(1.ToString(), GUILayout.Width(100f));
            GUILayout.Label(this.weakPointCount.ToString(), GUILayout.Width(100f));
            bool flag4 = this.weakPointCount == 1;
            this.hasCompliance &= flag4;
            GUILayout.Label(flag4 ? "OK" : "NO", GUILayout.Width(100f));
            GUILayout.EndHorizontal();
            GUILayout.Space(5f);
            GUILayout.BeginHorizontal();
            GUILayout.Label(LocalisationFile.GetTranslatedString("Machine(All)"), GUILayout.Width(150f));
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

        public int GetNumOfWeakPoint()
        {
            int index = 0;
            this.machine.BuildingBlocks.ForEach(BB =>
            {
                if (BB.BlockID == (int)BlockType.Bomb)
                    if (BB.gameObject.GetComponent<WeakPointBomb>())
                        index++;
            });
            return index;
        }

        public override string Name => "MachineInspector";
    }
}
