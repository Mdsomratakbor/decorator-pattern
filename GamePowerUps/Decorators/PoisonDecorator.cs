using GamePowerUps.Characters;
namespace GamePowerUps.Decorators;
public class PoisonDecorator : CharacterDecorator
{
    public PoisonDecorator(ICharacter character) : base(character)
    {
    }

    public override int Attack => base.Attack + 15;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} + Poison: Attack increased by 15";
    }
}