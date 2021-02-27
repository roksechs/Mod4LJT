using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Modding;

namespace Mod4LJT
{
    class LJTPlayerLabelManager : SingleInstance<LJTPlayerLabelManager>
    {
        GameObject playerLabels;
        List<PlayerLabel> currentPlayerLabels = new List<PlayerLabel>();
        Dictionary<PlayerData, int> playerTankTypeDic = new Dictionary<PlayerData, int>();
        readonly Dictionary<int, string> typeIconDic = new Dictionary<int, string>() 
        {
            { 0, "LightTankIcon" },
            { 1, "MediumTankIcon" },
            { 2, "HeavyTankIcon" },
            { 3, "DestroyerIcon" },
            { 4, "ArtilleryIcon" },
        };
        SimulationState simulationState;

        void Awake()
        {
            this.simulationState = StatMaster.SimulationState;
            if (StatMaster.isMP)
            {
                this.playerLabels = GameObject.Find("HUD/MULTIPLAYER/PLAYER_LABELS");
                this.DepthDisplayChange(0);
            }
            SceneManager.activeSceneChanged += (x, y) =>
            {
                if (StatMaster.isMP)
                {
                    this.playerLabels = GameObject.Find("HUD/MULTIPLAYER/PLAYER_LABELS");
                    this.DepthDisplayChange(0);
                }
            };
            StatMaster.hudHiddenChanged += () => 
            {
                if (StatMaster.isMP)
                {
                    this.playerLabels.SetActive(!this.playerLabels.activeSelf);
                }
            };
        }

        //void Update()
        //{
        //    if (StatMaster.SimulationState != simulationState)
        //    {
        //        Mod.Log("Simulation State Changed.");
        //        this.simulationState = StatMaster.SimulationState;
        //        foreach(var kvp in this.playerTankTypeDic)
        //        {
        //            this.ChangeTeamIcon(kvp.Key, kvp.Value);
        //        }
        //    }
        //}

        void LateUpdate()
        {
            foreach (var kvp in this.playerTankTypeDic)
            {
                this.ChangeTeamIcon(kvp.Key, kvp.Value);
            }
        }

        public void DepthDisplayChange(int layer)
        {
            foreach (UnityEngine.Transform transform1 in playerLabels.transform)
            {
                UnityEngine.Transform transform2 = transform1.Find("Background");
                transform2.gameObject.layer = layer;
                transform2.Find("BG_Health").gameObject.layer = layer;
                transform2.Find("HealthBar").Find("BarGraphic").gameObject.layer = layer;
                UnityEngine.Transform transform3 = transform1.Find("Content");
                transform3.Find("r_NameText").gameObject.layer = layer;
                transform3.Find("TeamIcon").gameObject.layer = layer;
                UnityEngine.Transform transform4 = transform3.Find("CopyButton");
                transform4.gameObject.layer = layer;
                transform4.Find("CopyIcon").gameObject.layer = layer;
                transform1.Find("triangle").gameObject.layer = layer;
            }
        }

        public void ChangeTeamIcon(PlayerData player, int machineTypeInt)
        {
            if (StatMaster.isMP)
            {
                if (NetworkScene.Instance.hud.playerLabelManager.Get(player, out PlayerLabel playerLabel))
                {
                    GameObject teamIcon = playerLabel.transform.Find("Content/TeamIcon").gameObject;
                    if(this.typeIconDic.TryGetValue(machineTypeInt, out string iconName))
                    {
                        Mod.Log("playerLabelIndex: " + playerLabel.transform.GetSiblingIndex().ToString());
                        Mod.Log("iconName: " + iconName);
                        teamIcon.GetComponent<MeshRenderer>().material.mainTexture = ModResource.GetTexture(iconName);
                        Mod.Log("Team Icon Changed");
                    }
                }
            }
        }

        public void SetPlayerTankType(PlayerData player, int tankType)
        {
            if (this.playerTankTypeDic.ContainsKey(player))
            {
                this.playerTankTypeDic[player] = tankType;
            }
            else
            {
                this.playerTankTypeDic.Add(player, tankType);
            }
        }

        public override string Name => "Name Plate Manager";
    }
}
