using Modding;
using Selectors;
using System;
using UnityEngine;

namespace Mod4LJT
{
    public class BoundResetter : SingleInstance<BoundResetter>
    {
        private BlockMapper blockMapper;
        private BlockBehaviour block;
        public int Refresh;
        bool reset;
        public static float max;

        public void Start()
        {
            this.Refresh = -1;
            this.reset = false;
            BlockMapper.onMapperClose += () => this.reset = false;
        }

        public void Update()
        {
            if (StatMaster.SimulationState >= SimulationState.GlobalSimulation) return;
            if (this.reset) return;
            if (this.blockMapper == null)
            {
                this.blockMapper = BlockMapper.CurrentInstance;
                this.block = null;
            }
            else
            {
                if (this.blockMapper.IsBlock)
                {
                    this.block = this.blockMapper.Block;
                    if (this.block.Prefab.Type == BlockType.Cannon || this.block.Prefab.Type == BlockType.ShrapnelCannon)
                    {
                        SliderSelector sliderSelector = this.blockMapper.GetComponentInChildren<SliderSelector>();
                        MSlider slider = sliderSelector.Slider;
                        string name = slider.DisplayName;
                        string key = slider.Key;
                        float value = slider.Value;
                        sliderSelector.Slider = new MSlider(name, key, value, 0.1f, max, null, null, true, false);
                        this.reset = true;
                        Mod.Log("Prosses3");
                    }
                }
            }
        }

        public override string Name => "ResetBound";
    }
}