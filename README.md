# Name Sorter

this program allows sorting list of names:
1- receives list of names in the form of text file where every line represent a name. 
2- parses 1 line to identify last name and 1 or more given names. (every line must include between 1 and 3 given names followed by last name)
3- sorts all names by last name then by given names
4- displays sorted names on screen and saves the result in a file `sorted-names-list.txt`.

[![CI](https://github.com/afennani/name-sorter/actions/workflows/ci.yml/badge.svg)](https://github.com/afennani/name-sorter/actions/workflows/ci.yml)

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

---

## Build

```bash
dotnet build --configuration Release
```

---

## Run

```bash
dotnet run --project src/NameSorter -- ./unsorted-names-list.txt
```

Or, after publishing:

```bash
dotnet publish src/NameSorter -c Release -o ./out
./out/name-sorter ./unsorted-names-list.txt
```

Given `unsorted-names-list.txt`:

```
Janet Parsons
Vaughn Lewis
Adonis Julius Archer
...
```

Outputs to screen and writes `sorted-names-list.txt`:

```
Marin Alvarez
Adonis Julius Archer
Beau Tristan Bentley
...
```

---

## Test

```bash
dotnet test
```

---


## Design notes

**Name parsing** — a name line is split on whitespace; the last token is the last name
and all preceding tokens (1–3) are given names. Blank lines are silently skipped;
lines with fewer than 2 or more than 4 tokens raise a `ArgumentException` with the
offending line attached.

**Sort order** — last name, then given names positionally.

**SOLID at a glance**

| Principle | Where |
|---|---|
| Single Responsibility | `INameParser` parses,  `INameSorter` sorts — each class has one reason to change |
| Open / Closed | New sort strategy → new class implementing `INameSorter`; `NameSortingService` unchanged |
| Liskov Substitution | `INameSorter` can be implemented in different strategies so they remain drop-in substitutable |
| Interface Segregation | `INameParse` and `INameSorter` are separate contracts; nothing is forced to implement both |
| Dependency Inversion | `NameSortingService` depends only on interfaces; concrete types are composed in `Program.cs` |

---

## Project layout

```
src/NameSorting.App/
  	Program.cs    — entry point + DI composition root
src/NameSorting.Core/
	Services/     — INameParser, INameSorter, INameSortingService 
	Models/      	— PersonName  

tests/NameSorting.Tests/
   	Services/     — NameParserTests, NameSorterTests, NameSortingServiceTest
```
