namespace GamePowerUps.Characters;

public class Archer : ICharacter
{
    public string Name => "Archer";
    public int Health => 80;
    public int Attack => 30;
    public int Speed => 20;

    public string GetDescription()
    {
        return $"Base Archer: Health={Health}, Attack={Attack}, Speed={Speed} \n";
    }
}