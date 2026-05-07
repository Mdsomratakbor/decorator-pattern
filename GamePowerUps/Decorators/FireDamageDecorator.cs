using GamePowerUps.Characters;
namespace GamePowerUps.Decorators;
public class FireDamageDecorator : CharacterDecorator
{
    public FireDamageDecorator(ICharacter character) : base(character)
    {
    }

    public override int Attack => base.Attack + 20;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} + Fire Damage: Attack increased by 20";
    }
}