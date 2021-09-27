using Xunit;

namespace CalculatorApp.Tests
{
    public class Test
    {
        MathHelper mathHelper;

        public Test()
        {
            this.mathHelper = new MathHelper();
        }

        [Fact]
        public void Hi()
        {
            var aa = mathHelper.Sum(new int[] { 1, 2, 3 });
        }
    }
}
