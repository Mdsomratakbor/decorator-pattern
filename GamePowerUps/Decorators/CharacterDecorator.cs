using GamePowerUps.Characters;
namespace GamePowerUps.Decorators;

public abstract class CharacterDecorator : ICharacter
{
    protected ICharacter _character;

    public CharacterDecorator(ICharacter character)
    {
        _character = character;
    }

    public virtual string Name => _character.Name;
    public virtual int Health => _character.Health;
    public virtual int Attack => _character.Attack;
    public virtual int Speed => _character.Speed;

    public virtual string GetDescription()
    {
        return _character.GetDescription();
    }
}