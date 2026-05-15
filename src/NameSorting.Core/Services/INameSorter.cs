using NameSorting.Models;

namespace NameSorting.Services
{
    public interface INameSorter
    {
        IReadOnlyList<PersonName> Sort(IEnumerable<PersonName> names);
    }
}
