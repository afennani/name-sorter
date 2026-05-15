using NameSorting.Models;

namespace NameSorting.Services
{
    public sealed class NameSorter : INameSorter
    {
        public IReadOnlyList<PersonName> Sort(
            IEnumerable<PersonName> names)
        {
            ArgumentNullException.ThrowIfNull(names);

            return names
                .OrderBy(n => n.LastName)
                .ThenBy(n => n.GivenName)
                .ToList();
        }
    }
}
