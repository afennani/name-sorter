using NameSorting.Models;

namespace NameSorting.Services
{
    public interface INameSortingService
    {
        IReadOnlyList<PersonName> SortNames(IEnumerable<string> rawNames);
    }
}
