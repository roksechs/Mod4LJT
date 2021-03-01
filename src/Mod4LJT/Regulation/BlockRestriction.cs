
namespace Mod4LJT.Regulation
{
    class BlockRestriction
    {
        public readonly int minNum;
        public readonly int maxNum;
        public readonly float[] maxPowers;

        public BlockRestriction(int minNum, int maxNum)
        {
            this.minNum = minNum;
            this.maxNum = maxNum;
            this.maxPowers = new float[1] { 0 };
        }

        public BlockRestriction(int minNum, int maxNum, params float[] maxPowers)
        {
            this.minNum = minNum;
            this.maxNum = maxNum;
            this.maxPowers = maxPowers;
        }
    }
}
