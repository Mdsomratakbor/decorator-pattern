using GamePowerUps.Characters;
namespace GamePowerUps.Decorators;
public class SpeedDecorator : CharacterDecorator
{
    public SpeedDecorator(ICharacter character) : base(character)
    {
    }

    public override int Speed => base.Speed + 10;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} + Speed Boost: Speed increased by 10";
    }
}