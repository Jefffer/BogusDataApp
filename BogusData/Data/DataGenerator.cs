using Bogus;

namespace BogusData.Data;

public class DataGenerator
{
    Faker<PersonModel> personModelFake;

    public DataGenerator()
    {
        // Seed is for telling the code that always start in the same "random" value
        Randomizer.Seed = new Random(123);

        personModelFake = new Faker<PersonModel>()
            .RuleFor(u => u.Id, f => f.Random.Int(1, 10000))
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.StreetAddress, f => f.Address.StreetAddress())
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.State, f => f.Address.StateAbbr())
            .RuleFor(u => u.ZipCode, f => f.Address.ZipCode())
            .RuleFor(u => u.Rating, f => f.PickRandom<CreditRating>());
    }

    /// <summary>
    /// Generates only one person at once
    /// </summary>
    /// <returns></returns>
    public PersonModel GeneratePerson()
    {
        return personModelFake.Generate();
    }

    /// <summary>
    /// Generates many people as we want (using .Take when calling)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<PersonModel> GeneratePeople()
    {
        return personModelFake.GenerateForever();
    }
}
