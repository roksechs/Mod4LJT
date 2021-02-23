
namespace Mod4LJT.Regulation
{
    class BlockRestriction
    {
        public readonly int minNum;
        public readonly int maxNum;
        public readonly float maxPower;
        public readonly float[] maxPowers;

        public BlockRestriction(int minNum, int maxNum)
        {
            this.minNum = minNum;
            this.maxNum = maxNum;
        }

        public BlockRestriction(int minNum, int maxNum, float maxPower)
        {
            this.minNum = minNum;
            this.maxNum = maxNum;
            this.maxPower = maxPower;
        }

        public BlockRestriction(int minNum, int maxNum, float[] maxPowers)
        {
            this.minNum = minNum;
            this.maxNum = maxNum;
            this.maxPowers = maxPowers;
        }
    }
}
