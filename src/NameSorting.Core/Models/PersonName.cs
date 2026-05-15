namespace NameSorting.Models
{
    public class PersonName
    {
        public const int MinGivenNames = 1;
        public const int MaxGivenNames = 3;

        public IReadOnlyList<string> GivenNames { get; }

        public string LastName { get; }

        public string GivenName { 
            get
            {
                return string.Join(" ", GivenNames);
            }                
        }

        public PersonName(IEnumerable<string> givenNames, string lastName)
        {
            ArgumentNullException.ThrowIfNull(givenNames);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

            if (givenNames.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException(
                    "Given names cannot contain empty values.",
                    nameof(givenNames));
            }

            if (givenNames.Count() < MinGivenNames || givenNames.Count() > MaxGivenNames)
                throw new ArgumentException(
                    nameof(givenNames),
                    $"A name must have between {MinGivenNames} and {MaxGivenNames} given names. Got {givenNames.Count()}.");

            GivenNames = givenNames.ToList().AsReadOnly();
            LastName = lastName;
        }

        public override string ToString()
            => $"{GivenName} {LastName}";
    }
}
