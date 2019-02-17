namespace ConsoleExtensions.Templating.Tests
{
  using ConsoleExtensions.Proxy.TestHelpers;

  using Xunit;

  public class ClearLineRenderTests
  {
    [Fact]
    public void GivenTemplateWithClearLine_WhenRender_ThenClearRemandingIsRendered()
    {
      // Arrange
      var proxy = new TestProxy();

      // Act
      var actual = proxy.WriteTemplate("[ClearLine/]", new { Test = "test" });

      // Assert
      Assert.Equal($"{new string(' ', 80)}", actual.ToString());
    }

    [Fact]
    public void GivenTemplateWithClearLineWithContent_WhenRender_ThenContentIsIgnored()
    {
      // Arrange
      var proxy = new TestProxy();

      // Act
      var actual = proxy.WriteTemplate("start|[ClearLine]Tester[/]|end", new { Test = "test" });

      // Assert
      Assert.Equal($"start|{new string(' ', 74)}|end", actual.ToString());
    }
  }
}