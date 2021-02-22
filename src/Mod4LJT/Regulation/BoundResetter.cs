using Modding;
using Selectors;
using System;

namespace Mod4LJT.Regulation
{

    class BoundResetter : SingleInstance<BoundResetter>
    {
        BlockMapper blockMapper;
        BlockBehaviour block;
        internal bool refresh;
        MachineInspector machineInspector = MachineInspector.Instance;
        int cannonCount;
        int shrapnelCannonCount;

        public void Start()
        {
            this.refresh = true;
            machineInspector.OnCannonCountChange += x =>
            {
                this.cannonCount = x;
                this.refresh = true;
            };
            machineInspector.OnShrapnelCannonCountChange += x =>
            {
                this.shrapnelCannonCount = x;
                this.refresh = true;
            };
        }

        public void Update()
        {
            if (StatMaster.SimulationState >= SimulationState.GlobalSimulation) return;
            if (BlockMapper.CurrentInstance == null) return;
            if(this.blockMapper != null && BlockMapper.CurrentInstance != this.blockMapper)
            {
                    this.blockMapper = BlockMapper.CurrentInstance;
                    this.refresh = true;
            }
            if (!this.refresh) return;
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
                    if (this.block.Prefab.Type == BlockType.Cannon)
                    {
                        this.machineInspector.regulation.ChildBlockRestriction.TryGetValue((int)BlockType.Cannon, out BlockRestriction blockRestriction);
                        SliderSelector sliderSelector = this.blockMapper.GetComponentInChildren<SliderSelector>();
                        SliderHolder sliderHolder = this.blockMapper.GetComponentInChildren<SliderHolder>();
                        MSlider slider = sliderSelector.Slider;
                        string name = slider.DisplayName;
                        string key = slider.Key;
                        float max = blockRestriction.maxPowers[cannonCount - 1];
                        float value = Math.Min(slider.Value, max);
                        if (cannonCount <= blockRestriction.maxNum && cannonCount > 0)
                        {
                            sliderSelector.Slider = new MSlider(name, key, value, 0.1f, max, null, null, true, false);
                            sliderSelector.Value = value;
                            sliderHolder.SetValue(value);
                        }
                    }
                    else if(this.block.Prefab.Type == BlockType.ShrapnelCannon)
                    {
                        this.machineInspector.regulation.ChildBlockRestriction.TryGetValue((int)BlockType.ShrapnelCannon, out BlockRestriction blockRestriction);
                        SliderSelector sliderSelector = this.blockMapper.GetComponentInChildren<SliderSelector>();
                        SliderHolder sliderHolder = this.blockMapper.GetComponentInChildren<SliderHolder>();
                        MSlider slider = sliderSelector.Slider;
                        string name = slider.DisplayName;
                        string key = slider.Key;
                        float max = blockRestriction.maxPower;
                        float value = Math.Min(slider.Value, max);
                        if (shrapnelCannonCount <= blockRestriction.maxNum && shrapnelCannonCount > 0)
                        {
                            sliderSelector.Slider = new MSlider(name, key, value, 0.1f, max, null, null, true, false);
                            sliderSelector.Value = value;
                            sliderHolder.SetValue(value);
                        }
                    }
                    else if(this.block.Prefab.Type == BlockType.CogMediumPowered)
                    {
                        this.machineInspector.regulation.ChildBlockRestriction.TryGetValue((int)BlockType.CogMediumPowered, out BlockRestriction blockRestriction);
                        SliderSelector sliderSelector = this.blockMapper.GetComponentInChildren<SliderSelector>();
                        SliderHolder sliderHolder = this.blockMapper.GetComponentInChildren<SliderHolder>();
                        MSlider slider = sliderSelector.Slider;
                        string name = slider.DisplayName;
                        string key = slider.Key;
                        float max = blockRestriction.maxPower;
                        float value = Math.Min(slider.Value, max);
                        sliderSelector.Slider = new MSlider(name, key, value, 0.1f, max, null, null, true, false);
                        sliderSelector.Value = value;
                        sliderHolder.SetValue(value);
                    }
                }
                this.refresh = false;
            }
        }

        public override string Name => "ResetBound";
    }
}