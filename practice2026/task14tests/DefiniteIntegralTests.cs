using System;
using Xunit;

public class DefiniteIntegralTests
{
    [Fact]
    public void LinearFunction1()
    {
        var x = (double x) => x;

        var result = DefiniteIntegral.Solve(-1, 1, x, 1e-4, 2);

        Assert.Equal(0, result, 1e-4);
    }

    [Fact]
    public void SinFunction()
    {
        var sin = (double x) => Math.Sin(x);

        var result = DefiniteIntegral.Solve(-1, 1, sin, 1e-5, 8);

        Assert.Equal(0, result, 1e-4);
    }

    [Fact]
    public void LinearFunction2()
    {
        var x = (double x) => x;

        var result = DefiniteIntegral.Solve(0, 5, x, 1e-6, 8);

        Assert.Equal(10, result, 1e-5);
    }
}
