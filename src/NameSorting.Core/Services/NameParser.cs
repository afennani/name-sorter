using NameSorting.Models;

namespace NameSorting.Services
{
    public sealed class NameParser : INameParser
    {
        public PersonName Parse(string fullName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fullName);

            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                throw new ArgumentException(
                    "A name must contain at least a given name and a last name.",
                    nameof(fullName));
            }

            var givenNames = parts.Take(parts.Length - 1).ToList();
            var lastName = parts.Last();

            return new PersonName(givenNames, lastName);
        }
    }
}
