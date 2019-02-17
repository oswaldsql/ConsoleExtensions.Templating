// ReSharper disable ExceptionNotDocumented
namespace ConsoleExtensions.Templating.Tests
{
  using ConsoleExtensions.Proxy.TestHelpers;

  using Xunit;

  public class WithRenderTests
  {
    [Fact]
    public void GivenATemplateContainingWith_WhenGivenKnownValue_ThenRenderValue()
    {
      // Arrange
      var proxy = new TestProxy();

      // Act
      var actual = proxy.WriteTemplate("start|[with:Test]{Length}[/]|end", new { Test = "test" });

      // Assert
      Assert.Equal("start|4|end", actual.ToString());
    }

    [Fact]
    public void GivenATemplateContainingWith_WhenGivenNullValue_ThenNoRender()
    {
      // Arrange
      var proxy = new TestProxy();

      // Act
      var actual = proxy.WriteTemplate("start|[with]T[/]|end", null);

      // Assert
      Assert.Equal("start||end", actual.ToString());
    }

    [Fact]
    public void GivenATemplateContainingWith_WhenGivenUnknownValue_ThenNoRender()
    {
      // Arrange
      var proxy = new TestProxy();

      // Act
      var actual = proxy.WriteTemplate("start|[with:unknown]T[/]|end", new { });

      // Assert
      Assert.Equal("start||end", actual.ToString());
    }
  }

  public class SubTemplateTests
  {
	  [Fact]
	  public void GivenAObjectWithNoSubTemplate_WhenRegisteringATemplate_ThenTheRegisteredTemplateShouldBeUsed()
	  {
		  // Arrange
		  var parser = new TemplateParser();
		  var template = parser.Parse("{}");

		  // Act
		  var testProxy = new TestProxy();
		  testProxy.WriteTemplate(template, this);
		  var preTemplateResult = testProxy.ToString();

		  parser.AddSubTemplate<SubTemplateTests>("Replaced with template");
		  var testProxy2 = new TestProxy();
		  testProxy2.WriteTemplate(template, this);
		  var postTemplateResult = testProxy2.ToString();

		  // Assert
		  Assert.Equal("ConsoleExtensions.Templating.Tests.SubTemplateTests", preTemplateResult);
		  Assert.Equal("Replaced with template", postTemplateResult);
	  }

  }
}