using FluentAssertions;
using NameSorting.Services;
using Xunit;

namespace NameSorting.Tests.Services;

public sealed class NameParserTests
{
    private readonly NameParser _parser = new();

    [Fact]
    public void Parse_WithOneGivenName_ReturnsCorrectName()
    {
        var result = _parser.Parse("Janet Parsons");
        result.GivenName.Should().Be("Janet");
        result.LastName.Should().Be("Parsons");
    }

    [Fact]
    public void Parse_WithTwoGivenNames_ReturnsCorrectName()
    {
        var result = _parser.Parse("Adonis Julius Archer");
        result.GivenName.Should().Be("Adonis Julius");
        result.LastName.Should().Be("Archer");
    }

    [Fact]
    public void Parse_WithThreeGivenNames_ReturnsCorrectName()
    {
        var result = _parser.Parse("Hunter Uriah Mathew Clarke");
        result.GivenName.Should().Be("Hunter Uriah Mathew");
        result.LastName.Should().Be("Clarke");
    }

    [Fact]
    public void Parse_WithExtraWhitespace_TrimsCorrectly()
    {
        var result = _parser.Parse("  Janet   Parsons  ");
        result.GivenName.Should().Be("Janet");
        result.LastName.Should().Be("Parsons");
    }


    [Fact]
    public void Parse_WithOnlyLastName_ThrowsNameParseException()
    {
        var act = () => _parser.Parse("Parsons");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Parse_WithFourGivenNames_ThrowsNameParseException()
    {
        var act = () => _parser.Parse("A B C D Clarke");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Parse_WithEmptyString_ThrowsNameParseException()
    {
        var act = () => _parser.Parse("");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Parse_WithWhitespaceOnly_ThrowsNameParseException()
    {
        var act = () => _parser.Parse("   ");
        act.Should().Throw<ArgumentException>();
    }
}
