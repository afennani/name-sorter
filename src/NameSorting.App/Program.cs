using NameSorting.Services;

namespace NameSorting.App
{
    internal class Program
    {
        /// <summary>
        /// Usage: NameSorter <input-file>"
        /// if file not provided as argument or argument file does not exist, user will be prompted to enter file path 
        /// until a valid file is provided. The file should contain one name per line.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool filePathProvided = false;
            var inputFile = string.Empty;

            if (args.Length == 1)
            {
                // try resolve file path from argument, 
                inputFile = args[0];
                filePathProvided = File.Exists(inputFile);
                if (!filePathProvided)
                {
                    Console.WriteLine($"File not found: {inputFile}");
                }
            }
            while (!filePathProvided)
            {
                Console.Clear();
                Console.WriteLine($"Enter the file path containing list of names:");
                inputFile = Console.ReadLine();

                filePathProvided = File.Exists(inputFile);
                if (!filePathProvided)
                {
                    Console.WriteLine($"File not found: {inputFile}: press Enter to try again");
                    Console.ReadLine();
                }

            }

            //input file exists at this point, proceed with reading and processing
            try
            {
                // Read all lines
                var rawNames = File.ReadAllLines(inputFile!);

                // Wire dependencies manually (no DI container needed for this console app)
                INameParser parser = new NameParser();
                INameSorter sorter = new NameSorter();  
                INameSortingService service = new NameSortingService(parser, sorter);

                var sorted = service.SortNames(rawNames);

                // Output to console
                foreach (var name in sorted)
                {
                    Console.WriteLine(name);
                }
                // Write to file
                File.WriteAllLines("sorted-names-list.txt", sorted.Select(x => x.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
