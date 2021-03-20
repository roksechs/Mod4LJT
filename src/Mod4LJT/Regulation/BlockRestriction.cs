
namespace Mod4LJT.Regulation
{
    class BlockRestriction
    {
        public readonly int minNum;
        public readonly int maxNum;
        public readonly float minPower;
        public readonly float[] maxPowers;

        public BlockRestriction(int minNum, int maxNum)
        {
            this.minNum = minNum;
            this.maxNum = maxNum;
            this.minPower = 0;
            this.maxPowers = new float[1] { 0 };
        }

        public BlockRestriction(int minNum, int maxNum, float minPower, params float[] maxPowers)
        {
            this.minNum = minNum;
            this.maxNum = maxNum;
            this.minPower = minPower;
            this.maxPowers = maxPowers;
        }
    }
}
