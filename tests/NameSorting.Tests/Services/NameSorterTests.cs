using FluentAssertions;
using NameSorting.Models;
using NameSorting.Services;
using Xunit;

namespace NameSorting.Tests.Services;

public sealed class NameSorterTests
{
    private readonly NameSorter _sorter = new NameSorter();

    [Fact]
    public void Sort_DifferentLastNames_SortsByLastNameFirst()
    {
        var names = new List<PersonName>() {

            new PersonName(["Beau"], "Bentley"),
            new PersonName(["Marin"], "Alvarez")
        };

        var sorted = _sorter.Sort(names);
        sorted[0].LastName.Should().Be("Alvarez");
        sorted[1].LastName.Should().Be("Bentley");
    }

    [Fact]
    public void Sort_SameLastName_SortsByFirstGivenName()
    {
        var names = new List<PersonName>() {
            new PersonName(["Martin"], "Alvarez"),
            new PersonName(["Beau"], "Alvarez")
        };

        var sorted = _sorter.Sort(names);
        sorted[0].GivenName.Should().Be("Beau");
        sorted[1].GivenName.Should().Be("Martin");
    }

    [Fact]
    public void Sort_SameLastAndFirstGivenName_SortsBySecondGivenName()
    {
        var names = new List<PersonName>() {
            new PersonName(["Ann", "Zara"], "Alvarez"),
            new PersonName(["Ann", "Mary"], "Alvarez")
        };

        var sorted = _sorter.Sort(names);
        sorted[0].GivenName.Should().Be("Ann Mary");
        sorted[1].GivenName.Should().Be("Ann Zara");
    }

    [Fact]
    public void Sort_SameLastAndFirstGivenName_DiffGivenNameLength()
    {
        var names = new List<PersonName>() {
            new PersonName(["Ann"], "Alvarez"),
            new PersonName(["Ann", "Zara"], "Alvarez"),
            new PersonName(["Ann", "Zara", "Sonia"], "Alvarez"),
            new PersonName(["Ann", "Mary"], "Alvarez")
        };

        var sorted = _sorter.Sort(names);
        sorted[0].GivenName.Should().Be("Ann");
        sorted[1].GivenName.Should().Be("Ann Mary");
        sorted[2].GivenName.Should().Be("Ann Zara");
        sorted[3].GivenName.Should().Be("Ann Zara Sonia");
    }

}
