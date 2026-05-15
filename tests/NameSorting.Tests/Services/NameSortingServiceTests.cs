using FluentAssertions;
using Moq;
using NameSorting.Models;
using NameSorting.Services;
using Xunit;

namespace NameSorting.Tests.Services;

public class NameSortingServiceTests
{
    [Fact]
    public void SortNames_Should_Parse_Input_And_Return_Sorted_Result()
    {
        // Arrange
        var parser = new Mock<INameParser>();
        var sorter = new Mock<INameSorter>();

        var input = new[]
        {            
            "Janet Parsons",
            "Shelby Nathan Yoder",
            "Ann Alvarez",
        };

        var ann = new PersonName(new[] { "Ann" }, "Alvarez");
        var janet = new PersonName(new[] { "Janet" }, "Parsons");

        var shelby = new PersonName(new[] { "Shelby" , "Nathan" }, "Yoder");

        var parsed = new List<PersonName> { janet, shelby , ann };

        var sorted = new List<PersonName> { ann, janet, shelby };

        parser.Setup(p => p.Parse("Janet Parsons")).Returns(janet);
        parser.Setup(p => p.Parse("Shelby Nathan Yoder")).Returns(shelby);
        parser.Setup(p => p.Parse("Ann Alvarez")).Returns(ann);

        sorter.Setup(s => s.Sort(It.IsAny<IEnumerable<PersonName>>()))
              .Returns(sorted);

        var service = new NameSortingService(parser.Object, sorter.Object);

        // Act
        var result = service.SortNames(input);

        // Assert
        result.Should().BeEquivalentTo(sorted, options => options.WithStrictOrdering());

        parser.Verify(p => p.Parse(It.IsAny<string>()), Times.Exactly(3));

        sorter.Verify(s => s.Sort(It.IsAny<IEnumerable<PersonName>>()), Times.Once);
    }
}