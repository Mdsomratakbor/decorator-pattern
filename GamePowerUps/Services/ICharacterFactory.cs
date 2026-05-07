using GamePowerUps.Characters;
namespace GamePowerUps.Services;
public interface ICharacterFactory
{
    ICharacter CreateCharacter(string characterType);
}
