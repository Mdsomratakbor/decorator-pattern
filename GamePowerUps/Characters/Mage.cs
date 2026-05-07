namespace GamePowerUps.Characters;
public class Mage : ICharacter
{
    public string Name => "Mage";
    public int Health => 90;
    public int Attack => 40;
    public int Speed => 15;

    public string GetDescription()
    {
        return $"Base Mage: Health={Health}, Attack={Attack}, Speed={Speed} \n";
    }
}