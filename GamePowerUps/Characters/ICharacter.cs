namespace GamePowerUps.Characters;
public interface ICharacter
{
    public string Name { get; }
    public int Health { get; }
    public int Attack {get; }
    public int Speed { get; }
    string GetDescription();
}

