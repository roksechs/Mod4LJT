using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Modding;
using Mod4LJT.Blocks;

namespace Mod4LJT
{
    class LJTPlayerLabelManager : SingleInstance<LJTPlayerLabelManager>
    {
        GameObject playerLabels;
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

        void Awake()
        {
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
            ModResource.OnAllResourcesLoaded += () =>
            {
                for(int i = 0; i < this.typeIconList.Count; i++)
                {
                    Texture texture = ModResource.GetTexture(this.typeIconList[i]);
                    this.typeIconDic.Add(i, texture);
                }
            };
        }

        void LateUpdate()
        {
            if (StatMaster.isMP)
            {
                foreach(var kvp in LJTMachine.MachineDic)
                {
                    this.ChangeTeamIcon(kvp.Key.Player.InternalObject, kvp.Value.TankTypeInt);
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

        public override string Name => "Name Plate Manager";
    }
}
