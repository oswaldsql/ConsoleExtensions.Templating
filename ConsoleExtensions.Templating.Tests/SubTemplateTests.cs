// <copyright company="Danfoss A/S">
//   2015 - 2019 Danfoss A/S. All rights reserved.
// </copyright>
namespace ConsoleExtensions.Templating.Tests
{
	using System.Linq;

	using ConsoleExtensions.Proxy.TestHelpers;

	using Xunit;

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

			parser.AddTypeTemplate<SubTemplateTests>("Replaced with template");
			var testProxy2 = new TestProxy();
			testProxy2.WriteTemplate(template, this);
			var postTemplateResult = testProxy2.ToString();

			// Assert
			Assert.Equal("ConsoleExtensions.Templating.Tests.SubTemplateTests", preTemplateResult);
			Assert.Equal("Replaced with template", postTemplateResult);
		}

		[Fact]
		public void GivenATypeTemplateWithTypeConverter_WhenRenderingAValue_ThenTheValueIsConverted()
		{
			// Arrange
			var parser = new TemplateParser();
			var template = parser.Parse("{}");

			// Act
			var testProxy = new TestProxy();
			testProxy.WriteTemplate(template, this);
			var preTemplateResult = testProxy.ToString();

			parser.AddTypeTemplate<SubTemplateTests>("{}", tests => new string(tests.GetType().Name.Reverse().ToArray()));
			var testProxy2 = new TestProxy();
			testProxy2.WriteTemplate(template, this);
			var postTemplateResult = testProxy2.ToString();

			// Assert
			Assert.Equal("ConsoleExtensions.Templating.Tests.SubTemplateTests", preTemplateResult);
			Assert.Equal("stseTetalpmeTbuS", postTemplateResult);
		}
	}
}