using Modding.Blocks;
using Selectors;

namespace Mod4LJT.Regulation
{

    static class BoundResetter
    {
        internal static void ResetBound()
        {
            if (BlockMapper.CurrentInstance.IsBlock)
            {
                int blockCount = PlayerMachine.GetLocal().GetBlocksOfType((int)BlockMapper.CurrentInstance.Block.Prefab.Type).Count;
                if (Mod.machineInspectorUI.regulation.BlockRestriction.TryGetValue((int)BlockMapper.CurrentInstance.Block.Prefab.Type, out BlockRestriction blockRestriction))
                {
                    SliderSelector[] selectors = BlockMapper.CurrentInstance.GetComponentsInChildren<SliderSelector>();
                    SliderHolder[] holders = BlockMapper.CurrentInstance.GetComponentsInChildren<SliderHolder>();
                    SliderSelector sliderSelector;
                    SliderHolder sliderHolder;
                    float min;
                    float max;
                    switch (BlockMapper.CurrentInstance.Block.Prefab.Type)
                    {
                        case BlockType.Cannon:
                            min = blockRestriction.minPower;
                            max = blockRestriction.maxPowers[blockCount - 1];
                            sliderSelector = selectors[0];
                            sliderHolder = holders[0];
                            break;
                        case BlockType.ShrapnelCannon:
                        case BlockType.WaterCannon:
                            min = blockRestriction.minPower;
                            max = blockRestriction.maxPowers[0];
                            sliderSelector = selectors[0];
                            sliderHolder = holders[0];
                            break;
                        case BlockType.CogMediumPowered:
                        case BlockType.Wheel:
                        case BlockType.LargeWheel:
                            min = blockRestriction.minPower;
                            max = blockRestriction.maxPowers[0];
                            sliderSelector = selectors[0];
                            sliderHolder = holders[0];
                            break;
                        case BlockType.Rocket:
                            min = blockRestriction.minPower;
                            max = blockRestriction.maxPowers[0];
                            sliderSelector = selectors[1];
                            sliderHolder = holders[1];
                            break;
                        default:
                            return;
                    }
                    MSlider mSlider = sliderSelector.Slider;
                    if (blockCount <= blockRestriction.maxCount && blockCount > 0)
                    {
                        sliderSelector.Slider = new MSlider(mSlider.DisplayName, mSlider.Key, mSlider.Value, min, max, null, null, true, false);
                        sliderSelector.Value = mSlider.Value;
                        sliderHolder.SetValue(mSlider.Value);
                    }
                }
            }
        }
    }
}