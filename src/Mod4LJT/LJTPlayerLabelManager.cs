using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Modding;

namespace Mod4LJT
{
    class LJTPlayerLabelManager : SingleInstance<LJTPlayerLabelManager>
    {
        GameObject playerLabels;
        readonly Dictionary<PlayerData, int> playerTankTypeDic = new Dictionary<PlayerData, int>();
        readonly List<string> typeIconList = new List<string>() 
        {
            "LightTankIcon",
            "MediumTankIcon",
            "HeavyTankIcon",
            "DestroyerIcon",
            "ArtilleryIcon",
            "JunkTankIcon",
        };
        readonly Dictionary<int, Texture> typeIconDic = new Dictionary<int, Texture>();
        bool isEmpty = true;

        void Awake()
        {
            if (StatMaster.isMP)
            {
                this.playerLabels = GameObject.Find("HUD/MULTIPLAYER/PLAYER_LABELS");
                this.DepthDisplayChange(8);
            }
            SceneManager.activeSceneChanged += (x, y) =>
            {
                this.playerTankTypeDic.Clear();
                this.isEmpty = true;
                if (StatMaster.isMP)
                {
                    this.playerLabels = GameObject.Find("HUD/MULTIPLAYER/PLAYER_LABELS");
                    this.DepthDisplayChange(8);
                }
            };
            StatMaster.hudHiddenChanged += () => 
            {
                if (StatMaster.isMP)
                {
                    this.playerLabels.SetActive(!this.playerLabels.activeSelf);
                }
            };
            ModResource.OnAllResourcesLoaded += () =>
            {
                for(int i = 0; i < this.typeIconList.Count; i++)
                {
                    Texture texture = ModResource.GetTexture(this.typeIconList[i]);
                    this.typeIconDic.Add(i, texture);
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
            if (StatMaster.isMP && !this.isEmpty)
            {
                foreach (var kvp in this.playerTankTypeDic)
                {
                    this.ChangeTeamIcon(kvp.Key, kvp.Value);
                }
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
                    if(this.typeIconDic.TryGetValue(machineTypeInt, out Texture iconTexture))
                    {
                        teamIcon.GetComponent<MeshRenderer>().material.mainTexture = iconTexture;
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
                this.isEmpty = false;
            }
            this.DepthDisplayChange(tankType != 5 ? 10 : 23);
        }

        public override string Name => "Name Plate Manager";
    }
}
