namespace ConsoleExtensions.Templating.Tests
{
  using ConsoleExtensions.Proxy.TestHelpers;

  using Xunit;

  public class LineBreakRender
  {
    [Fact]
    public void GivenATemplateWithLineBreak_WhenRendering_ThenRenderLineBreak()
    {
      // Arrange
      var proxy = new TestProxy();

      // Act
      var actual = proxy.WriteTemplate("[br][/][br/]", null);

      // Assert
      Assert.Equal("\n\n", actual.ToString());
    }

    [Fact]
    public void GivenTextNestedWithinBr_WhenRendering_ThenNestedTextIsIgnored()
    {
      // Arrange
      var proxy = new TestProxy();

      // Act
      var actual = proxy.WriteTemplate("start|[br]Tester[/]|end", null);

      // Assert
      Assert.Equal("start|\n|end", actual.ToString());
    }
  }
}