using NameSorting.Models;

namespace NameSorting.Services
{
    public interface INameParser
    {
        PersonName Parse(string fullName);
    }
}
