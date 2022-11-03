
namespace Mod4LJT.Regulation
{
    class BlockRestriction
    {
        public readonly int minCount;
        public readonly int maxCount;
        public readonly float minPower;
        public readonly float[] maxPowers;

        public BlockRestriction(int minNum, int maxNum)
        {
            this.minCount = minNum;
            this.maxCount = maxNum;
            this.minPower = 0;
            this.maxPowers = new float[1] { 0 };
        }

        public BlockRestriction(int minNum, int maxNum, float minPower, params float[] maxPowers)
        {
            this.minCount = minNum;
            this.maxCount = maxNum;
            this.minPower = minPower;
            this.maxPowers = maxPowers;
        }
    }
}
