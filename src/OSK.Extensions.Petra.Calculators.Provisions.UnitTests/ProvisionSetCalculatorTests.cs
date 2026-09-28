using Moq;
using OSK.Petra.Calculators.Inflation.Ports;
using OSK.Petra.Provisions.Models;

namespace OSK.Extensions.Petra.Calculators.Provisions.UnitTests;

public class ProvisionSetCalculatorTests
{
    #region Variables

    private readonly Mock<IInflationCalculator> _mockInflationCalculator;

    private readonly ProvisionInflationCalculator _calculator;

    #endregion

    #region Constructors

    public ProvisionSetCalculatorTests()
    {
        _mockInflationCalculator = new Mock<IInflationCalculator>();

        _calculator = new(_mockInflationCalculator.Object);
    }

    #endregion

    #region Constructors Tests

    [Fact]
    public void Constructor_WithNullDefaultCalculator_ThrowsArgumentNullException()
    {
        // Arrange/Act/Assert
        Assert.Throws<ArgumentNullException>(() => new ProvisionInflationCalculator((IInflationCalculator)null!));
    }

    [Fact]
    public void Constructor_WithNullStrategiesCollection_ThrowsArgumentNullException()
    {
        // Arrange/Act/Assert
        Assert.Throws<ArgumentNullException>(() => new ProvisionInflationCalculator((IEnumerable<ProvisionInflationStrategy>)null!));
    }

    #endregion

    #region Inflate

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Inflate_WithNullOrEmptyBaseProvisions_ReturnsEmpty(bool useNull)
    {
        // Arrange/Act
        var result = _calculator.Inflate(useNull ? null! : [], 1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Inflate_UsingDefaultCalculator_WhenNoSpecificStrategyExists_AppliesDefaultCalculator()
    {
        // Arrange
        var provisionId = Guid.NewGuid();
        var baseProvisions = new[] { new Provision(provisionId, 50) };

        _mockInflationCalculator.Setup(c => c.Inflate(50.0, 3)).Returns(75.5);

        // Act
        var result = _calculator.Inflate(baseProvisions, 3).ToList();

        // Assert
        Assert.Single(result);

        Assert.Equal(provisionId, result[0].Id);
        Assert.Equal(75.5d, result[0].Amount);

        _mockInflationCalculator.Verify(c => c.Inflate(50.0, 3), Times.Once);
    }

    [Fact]
    public void Inflate_UsingSpecificProvisionStrategy_WhenStrategyExists_AppliesSpecificCalculator()
    {
        // Arrange
        var goldId = Guid.NewGuid();
        var lumberId = Guid.NewGuid();

        var mockGoldCalculator = new Mock<IInflationCalculator>();
        mockGoldCalculator.Setup(c => c.Inflate(100.0, 2)).Returns(120.0);

        var strategies = new List<ProvisionInflationStrategy>
        {
            new(goldId, mockGoldCalculator.Object)
        };

        var baseProvisions = new[]
        {
            new Provision(goldId, 100),
            new Provision(lumberId, 50)
        };

        var calculator = new ProvisionInflationCalculator(strategies);

        // Act
        var result = calculator.Inflate(baseProvisions, 2).ToList();

        // Assert
        Assert.Equal(2, result.Count);

        // Gold used specific calculator
        Assert.Equal(goldId, result[0].Id);
        Assert.Equal(120, result[0].Amount);;

        // Lumber had no calculator (default is null), so amount remains uninflated
        Assert.Equal(lumberId, result[1].Id);
        Assert.Equal(50, result[1].Amount);

        mockGoldCalculator.Verify(c => c.Inflate(100.0, 2), Times.Once);
    }

    [Fact]
    public void Constructor_WithStrategiesContainingDefaultAndSpecific_ConfiguresCorrectly()
    {
        // Arrange
        var manaId = Guid.NewGuid();
        var mockDefault = new Mock<IInflationCalculator>();
        var mockManaCalculator = new Mock<IInflationCalculator>();

        mockDefault.Setup(c => c.Inflate(10.0, 1)).Returns(15.0);
        mockManaCalculator.Setup(c => c.Inflate(20.0, 1)).Returns(30.0);

        var strategies = new List<ProvisionInflationStrategy>
        {
            // Null ProvisionDefinitionId sets the default calculator in this constructor overload
            new(mockDefault.Object),
            new(manaId, mockManaCalculator.Object)
        };

        var baseProvisions = new[]
        {
            new Provision(Guid.NewGuid(), 10),
            new Provision(manaId, 20)
        };

        var calculator = new ProvisionInflationCalculator(strategies);

        // Act
        var result = calculator.Inflate(baseProvisions, 1).ToList();

        // Assert
        Assert.Equal(2, result.Count);

        Assert.Equal(15, result[0].Amount);
        Assert.Equal(30, result[1].Amount);
    }

    #endregion
}
