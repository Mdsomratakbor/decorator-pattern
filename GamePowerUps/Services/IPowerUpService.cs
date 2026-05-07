using GamePowerUps.Characters;
using GamePowerUps.Models;
namespace GamePowerUps.Services;
public interface IPowerUpService
{
    ICharacter ApplyPowerUps(ICharacter character, List<PowerUpType> powerUps);
}