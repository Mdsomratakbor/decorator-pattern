using GamePowerUps.Characters;
namespace GamePowerUps.Services;
public class CharacterFactory : ICharacterFactory
{
    public ICharacter CreateCharacter(string characterType)
    {
        return characterType.ToLower() switch
        {
            "warrior" => new Warrior(),
            "mage" => new Mage(),
            "archer" => new Archer(),
            _ => throw new ArgumentException("Invalid character type")
        };
    }
}