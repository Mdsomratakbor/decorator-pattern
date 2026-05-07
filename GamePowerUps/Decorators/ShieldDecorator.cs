using GamePowerUps.Characters;
namespace GamePowerUps.Decorators;
public class ShieldDecorator : CharacterDecorator
{
    public ShieldDecorator(ICharacter character) : base(character)
    {
    }

    public override int Health => base.Health + 50;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} + Shield: Health increased by 50";
    }
}