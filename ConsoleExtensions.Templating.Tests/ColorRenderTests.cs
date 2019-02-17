// ReSharper disable ExceptionNotDocumented
namespace ConsoleExtensions.Templating.Tests
{
  using ConsoleExtensions.Proxy.TestHelpers;

  using Xunit;

  public class ColorRenderTests
  {
    [Theory]
    [InlineData("[c:white]value", "[SetColor:F=White]value[SetColor:F=Black]")]
    [InlineData("[c:white]value[/c]", "[SetColor:F=White]value[SetColor:F=Black]")]
    [InlineData("[c:white]value[/]", "[SetColor:F=White]value[SetColor:F=Black]")]
    [InlineData("[c:unknown]value[/]", "value")]
    [InlineData("[c:white]1[c:green]2[/]3[/]", "[SetColor:F=White]1[SetColor:F=Green]2[SetColor:F=White]3[SetColor:F=Black]")]
    [InlineData("Hello [c:Green]Tom[/C], how are you", "Hello [SetColor:F=Green]Tom[SetColor:F=Black], how are you")]
    [InlineData("t[c:Green]e[c:Black]s", "t[SetColor:F=Green]e[SetColor:F=Black]s[SetColor:F=Green][SetColor:F=Black]")]
    public void GivenATemplateColor_WhenRendering_ThenExpectedColorCommandShouldBeRendered(string source, string expected)
    {
      // Arrange
      var proxy = new TestProxy();

      // Act
      var actual = proxy.WriteTemplate(source, null).ToString();

      // Assert
      Assert.Equal(expected, actual);
    }
  }
}