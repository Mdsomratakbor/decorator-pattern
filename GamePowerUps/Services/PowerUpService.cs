using GamePowerUps.Characters;
using GamePowerUps.Decorators;
using GamePowerUps.Models;
using GamePowerUps.Services;
namespace GamePowerUps.Services;

public class PowerUpService : IPowerUpService
{
    public ICharacter ApplyPowerUps(ICharacter character, List<PowerUpType> powerUps)
    {
           foreach (var powerUp in powerUps)
        {
            character = powerUp switch
            {
                PowerUpType.Shield => new ShieldDecorator(character),
                PowerUpType.Speed => new SpeedDecorator(character),
                PowerUpType.FireDamage => new FireDamageDecorator(character),
                PowerUpType.Poison => new PoisonDecorator(character),
                _ => character
            };
        }

        return character;
    }
}