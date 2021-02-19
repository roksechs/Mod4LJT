using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mod4LJT.Regulation
{
    class MachineInspector : SingleInstance<MachineInspector>
    {
        private TankType tankType;
        private Regulation regulation;
        private Dictionary<int, Regulation> regulations = new Dictionary<int, Regulation>()
        {
            { (int) TankType.LightTank, LightTank.Instance },
            { (int) TankType.MediumTank, MediumTank.Instance },
            { (int) TankType.HeavyTank, HeavyTank.Instance },
            { (int) TankType.Destroyer, Destroyer.Instance },
            { (int) TankType.Artillery, Mod4LJT.Regulation.Artillery.Instance },
        };

        public void SetTankType(TankType tankType)
        {
            this.tankType = tankType;
            this.regulations.TryGetValue((int)this.tankType, out this.regulation);
            foreach (var kvp in this.regulation.blockRestrictions)
            {
                Mod.Log(((BlockType)kvp.Key).ToString());
            }
        }

        private Rect windowRect = new Rect(0.0f, 80f, 300f, 100f);

        public int TotalBlock => Machine.Active().DisplayBlockCount;

        public void OnGUI()
        {
            if (StatMaster.SimulationState >= SimulationState.GlobalSimulation) return;
            this.windowRect = GUILayout.Window(213887, this.windowRect, new GUI.WindowFunction(this.Mapper), "LJT Regulation");
        }

        public void Mapper(int windowId)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Total Block Number");
            GUILayout.FlexibleSpace();
            GUILayout.Label(this.TotalBlock.ToString() + "/" + 251.ToString() + (this.TotalBlock <= 251 ? " OK" : " NO"));
            GUILayout.EndHorizontal();
            this.RegulationLabels();
            GUI.DragWindow();
        }

        public void RegulationLabels()
        {
            foreach(var kvp in this.regulation.ChildBlockRestriction)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(((BlockType)kvp.Key).ToString());
                GUILayout.FlexibleSpace();
                GUILayout.Label(string.Format("MaxNum: {0}", kvp.Value.maxNum));
                GUILayout.EndHorizontal();
            }
        }

        public int NumOfBlock(int BlockId)
        {
            int num = 0;
            foreach (BlockBehaviour buildingBlock in Machine.Active().BuildingBlocks)
            {
                if (buildingBlock.BlockID == BlockId)
                    ++num;
            }
            return num;
        }

        public override string Name => "MachineInspector";
    }
}
