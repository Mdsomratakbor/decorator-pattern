using GamePowerUps.Services;
using GamePowerUps.Characters;
using GamePowerUps.Models;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddTransient<ICharacterFactory, CharacterFactory>();
services.AddTransient<IPowerUpService, PowerUpService>();

var serviceProvider = services.BuildServiceProvider();

var characterFactory = serviceProvider.GetRequiredService<ICharacterFactory>();
var powerUpService = serviceProvider.GetRequiredService<IPowerUpService>();

var character = characterFactory.CreateCharacter("Warrior");
var powerUps = new List<PowerUpType> { PowerUpType.Shield, PowerUpType.Speed, PowerUpType.FireDamage, PowerUpType.Shield }; // Example power-ups  };
var poweredUpCharacter = powerUpService.ApplyPowerUps(character, powerUps);
Console.WriteLine(poweredUpCharacter.GetDescription());

