using Modding.Blocks;
using Selectors;

namespace Mod4LJT.Regulation
{

    class BoundResetter : SingleInstance<BoundResetter>
    {
        BlockMapper blockMapper;
        BlockBehaviour block;
        SliderSelector[] selectors;
        SliderSelector sliderSelector;
        SliderHolder[] holders;
        SliderHolder sliderHolder;
        MSlider mSlider;
        string sliderName;
        string key;
        float min;
        float max;
        float value;
        readonly MachineInspector machineInspector = MachineInspector.Instance;
        int blockCount;
        public bool refresh;

        public void Update()
        {
            if (BlockMapper.IsOpen)
            {
                this.refresh |= BlockMapper.CurrentInstance != this.blockMapper;
                if (this.refresh)
                {
                    this.blockMapper = BlockMapper.CurrentInstance;
                    this.refresh &= this.blockMapper.IsBlock;
                    if (this.refresh)
                    {
                        this.block = this.blockMapper.Block;
                        this.blockCount = PlayerMachine.GetLocal().GetBlocksOfType((int)this.block.Prefab.Type).Count;
                        this.refresh &= this.machineInspector.regulation.ChildBlockRestriction.TryGetValue((int)this.block.Prefab.Type, out BlockRestriction blockRestriction);
                        if (this.refresh)
                        {
                            this.selectors = this.blockMapper.GetComponentsInChildren<SliderSelector>();
                            this.holders = this.blockMapper.GetComponentsInChildren<SliderHolder>();
                            switch (this.block.Prefab.Type)
                            {
                                case BlockType.Cannon:
                                    this.min = blockRestriction.minPower;
                                    this.max = blockRestriction.maxPowers[this.blockCount - 1];
                                    this.sliderSelector = this.selectors[0];
                                    this.sliderHolder = this.holders[0];
                                    break;
                                case BlockType.ShrapnelCannon:
                                case BlockType.WaterCannon:
                                    this.min = blockRestriction.minPower;
                                    this.max = blockRestriction.maxPowers[0];
                                    this.sliderSelector = this.selectors[0];
                                    this.sliderHolder = this.holders[0];
                                    break;
                                case BlockType.CogMediumPowered:
                                case BlockType.Wheel:
                                case BlockType.LargeWheel:
                                    this.min = blockRestriction.minPower;
                                    this.max = blockRestriction.maxPowers[0];
                                    this.sliderSelector = this.selectors[0];
                                    this.sliderHolder = this.holders[0];
                                    break;
                                case BlockType.Rocket:
                                    this.min = blockRestriction.minPower;
                                    this.max = blockRestriction.maxPowers[0];
                                    this.sliderSelector = this.selectors[1];
                                    this.sliderHolder = this.holders[1];
                                    break;
                                default:
                                    return;
                            }
                            this.mSlider = this.sliderSelector.Slider;
                            this.sliderName = this.mSlider.DisplayName;
                            this.key = this.mSlider.Key;
                            this.value = this.mSlider.Value;
                            if (this.blockCount <= blockRestriction.maxNum && this.blockCount > 0)
                            {
                                this.sliderSelector.Slider = new MSlider(this.sliderName, this.key, this.value, this.min, this.max, null, null, true, false);
                                this.sliderSelector.Value = value;
                                this.sliderHolder.SetValue(value);
                            }
                        }
                    }
                }
                this.refresh = false;
            }
        }

        public override string Name => "ResetBound";
    }
}