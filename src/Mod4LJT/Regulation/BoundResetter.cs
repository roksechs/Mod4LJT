using Selectors;
using Modding.Blocks;

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
        int waterCannonCount;

        public void Start()
        {
            this.refresh = true;
        }

        public void Update()
        {
            if (BlockMapper.IsOpen)
            {
                if (BlockMapper.CurrentInstance != this.blockMapper)
                {
                    this.blockMapper = BlockMapper.CurrentInstance;
                    this.block = null;
                    this.refresh = true;
                }
                if (this.cannonCount != PlayerMachine.GetLocal().GetBlocksOfType((int)BlockType.Cannon).Count)
                {
                    this.cannonCount = PlayerMachine.GetLocal().GetBlocksOfType((int)BlockType.Cannon).Count;
                    this.refresh = true;
                }
                else if (this.shrapnelCannonCount != PlayerMachine.GetLocal().GetBlocksOfType((int)BlockType.ShrapnelCannon).Count)
                {
                    this.shrapnelCannonCount = PlayerMachine.GetLocal().GetBlocksOfType((int)BlockType.ShrapnelCannon).Count;
                    this.refresh = true;
                }
                else if (this.waterCannonCount != PlayerMachine.GetLocal().GetBlocksOfType((int)BlockType.WaterCannon).Count)
                {
                    this.waterCannonCount = PlayerMachine.GetLocal().GetBlocksOfType((int)BlockType.WaterCannon).Count;
                    this.refresh = true;
                }
                if (!this.refresh) return;
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
                        float value = slider.Value;
                        if (cannonCount <= blockRestriction.maxNum && cannonCount > 0)
                        {
                            sliderSelector.Slider = new MSlider(name, key, value, 0.1f, max, null, null, true, false);
                            sliderSelector.Value = value;
                            sliderHolder.SetValue(value);
                        }
                    }
                    else if (this.block.Prefab.Type == BlockType.ShrapnelCannon)
                    {
                        this.machineInspector.regulation.ChildBlockRestriction.TryGetValue((int)BlockType.ShrapnelCannon, out BlockRestriction blockRestriction);
                        SliderSelector sliderSelector = this.blockMapper.GetComponentInChildren<SliderSelector>();
                        SliderHolder sliderHolder = this.blockMapper.GetComponentInChildren<SliderHolder>();
                        MSlider slider = sliderSelector.Slider;
                        string name = slider.DisplayName;
                        string key = slider.Key;
                        float max = blockRestriction.maxPowers[0];
                        float value = slider.Value;
                        if (shrapnelCannonCount <= blockRestriction.maxNum && shrapnelCannonCount > 0)
                        {
                            sliderSelector.Slider = new MSlider(name, key, value, 0.1f, max, null, null, true, false);
                            sliderSelector.Value = value;
                            sliderHolder.SetValue(value);
                        }
                    }
                    else if (this.block.Prefab.Type == BlockType.WaterCannon)
                    {
                        this.machineInspector.regulation.ChildBlockRestriction.TryGetValue((int)BlockType.WaterCannon, out BlockRestriction blockRestriction);
                        SliderSelector sliderSelector = this.blockMapper.GetComponentInChildren<SliderSelector>();
                        SliderHolder sliderHolder = this.blockMapper.GetComponentInChildren<SliderHolder>();
                        MSlider slider = sliderSelector.Slider;
                        string name = slider.DisplayName;
                        string key = slider.Key;
                        float max = blockRestriction.maxPowers[0];
                        float value = slider.Value;
                        if (waterCannonCount <= blockRestriction.maxNum && waterCannonCount > 0)
                        {
                            sliderSelector.Slider = new MSlider(name, key, value, 0.1f, max, null, null, true, false);
                            sliderSelector.Value = value;
                            sliderHolder.SetValue(value);
                        }
                    }
                    else if (this.block.Prefab.Type == BlockType.CogMediumPowered || this.block.Prefab.Type == BlockType.Wheel || this.block.Prefab.Type == BlockType.LargeWheel)
                    {
                        this.machineInspector.regulation.ChildBlockRestriction.TryGetValue((int)BlockType.CogMediumPowered, out BlockRestriction blockRestriction);
                        SliderSelector sliderSelector = this.blockMapper.GetComponentInChildren<SliderSelector>();
                        SliderHolder sliderHolder = this.blockMapper.GetComponentInChildren<SliderHolder>();
                        MSlider slider = sliderSelector.Slider;
                        string name = slider.DisplayName;
                        string key = slider.Key;
                        float max = blockRestriction.maxPowers[0];
                        float value = slider.Value;
                        sliderSelector.Slider = new MSlider(name, key, value, 0.0f, max, null, null, true, false);
                        sliderSelector.Value = value;
                        sliderHolder.SetValue(value);
                    }
                    else if (this.block.Prefab.Type == BlockType.Rocket)
                    {
                        SliderSelector[] selectors = this.blockMapper.GetComponentsInChildren<SliderSelector>();
                        SliderSelector sliderSelector = selectors[1];
                        SliderHolder[] holders = this.blockMapper.GetComponentsInChildren<SliderHolder>();
                        SliderHolder sliderHolder = holders[1];
                        MSlider slider = sliderSelector.Slider;
                        string name = slider.DisplayName;
                        string key = slider.Key;
                        float max = 4f;
                        float value = slider.Value;
                        sliderSelector.Slider = new MSlider(name, key, value, 0.0f, max, null, null, true, false);
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