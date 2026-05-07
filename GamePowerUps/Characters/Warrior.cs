namespace GamePowerUps.Characters;
public class Warrior : ICharacter
{
    public string Name { get; } = "Warrior";
    public int Health { get; } = 150;
    public int Attack { get; } = 20;
    public int Speed { get; } = 10;

    public string GetDescription()
    {
        return $"Base Warrior: Health={Health}, Attack={Attack}, Speed={Speed} \n";
    }
    
}