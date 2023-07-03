namespace Calculator.Tests;

public class CalcTest
{
    [Theory]
    [InlineData(1,2,3)]
    [InlineData(11,12,23)]
    [InlineData(-8,2,-6)]
    public void TestAdd(int x, int y, int expected)
    {
        var math = new  CalcLib.Calculator();

        var result = math.Add(x, y);

        Assert.Equal(expected, result);
    }
}