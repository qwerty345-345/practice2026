using Xunit;
using task04;

namespace task04tests
{
    public class SpaceshipTests
    {
        [Fact]
        public void Cruiser_ShouldHaveCorrectStats()
        {
            ISpaceship cruiser = new Cruiser();

            Assert.Equal(50, cruiser.Speed);
            Assert.Equal(100, cruiser.FirePower);
        }

        [Fact]
        public void Fighter_ShouldHaveCorrectStats()
        {
            ISpaceship fighter = new Fighter();

            Assert.Equal(100, fighter.Speed);
            Assert.Equal(50, fighter.FirePower);
        }

        [Fact]
        public void Fighter_ShouldBeFasterThanCruiser()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();

            Assert.True(fighter.Speed > cruiser.Speed);
        }

        [Fact]
        public void Cruiser_ShouldHaveMoreFirePowerThanFighter()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();

            Assert.True(cruiser.FirePower > fighter.FirePower);
        }

        [Fact]
        public void Cruiser_MoveForward_ShouldNotThrow()
        {
            var cruiser = new Cruiser();

            var ex = Record.Exception(() => cruiser.MoveForward());

            Assert.Null(ex);
        }

        [Fact]
        public void Fighter_MoveForward_ShouldNotThrow()
        {
            var fighter = new Fighter();

            var ex = Record.Exception(() => fighter.MoveForward());

            Assert.Null(ex);
        }

        [Fact]
        public void Cruiser_Fire_ShouldNotThrow()
        {
            var cruiser = new Cruiser();

            var ex = Record.Exception(() => cruiser.Fire());

            Assert.Null(ex);
        }

        [Fact]
        public void Fighter_Fire_ShouldNotThrow()
        {
            var fighter = new Fighter();

            var ex = Record.Exception(() => fighter.Fire());

            Assert.Null(ex);
        }
    }
}