using NameSorting.Models;

namespace NameSorting.Services
{
    public sealed class NameSortingService : INameSortingService
    {
        private readonly INameParser _parser;
        private readonly INameSorter _sorter;

        public NameSortingService(INameParser parser, INameSorter sorter)
        {
            _parser = parser;
            _sorter = sorter;
        }

        public IReadOnlyList<PersonName> SortNames(IEnumerable<string> rawNames)
        {
            ArgumentNullException.ThrowIfNull(rawNames);

            var parsed = rawNames
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(_parser.Parse)
                .ToList();

            return _sorter.Sort(parsed);
        }
    }
}
