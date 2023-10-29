using CCU.Traits.App_HS1;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.App_HS3
{
	public class Mask_Override : T_Hairstyle
	{
		public override string[] Rolls => new string[] { };

		public static List<string> IncompatibleAccessories = new List<string>() { "CopHat", "DoctorHeadLamp", "HatBlue", "HatRed", "ThiefHat", "Fedora", "FireHelmet", "Cop2Hat", "Headphones" };

		// TODO: Consider this system. These entries all have the full list though.
		public static Dictionary<string, string[]> IncompatiblePairs = new Dictionary<string, string[]>()
		{
			{ "CopHat",         new string[] { "AlienHead", "AssassinMask", "ButlerBotHead", "CopBotHead", "GorillaHead", "Hoodie", "RobotHead", "RobotPlayerHead", "SlavemasterMask", "WerewolfHead", } },
			{ "Cop2Hat",        new string[] { "AlienHead", "AssassinMask", "ButlerBotHead", "CopBotHead", "GorillaHead", "Hoodie", "RobotHead", "RobotPlayerHead", "SlavemasterMask", "WerewolfHead", } },
			{ "DoctorHeadLamp", new string[] { "AlienHead", "AssassinMask", "ButlerBotHead", "CopBotHead", "GorillaHead", "Hoodie", "RobotHead", "RobotPlayerHead", "SlavemasterMask", "WerewolfHead", } },
			{ "Fedora",         new string[] { "AlienHead", "AssassinMask", "ButlerBotHead", "CopBotHead", "GorillaHead", "Hoodie", "RobotHead", "RobotPlayerHead", "SlavemasterMask", "WerewolfHead", } },
			{ "FireHelmet",     new string[] { "AlienHead", "AssassinMask", "ButlerBotHead", "CopBotHead", "GorillaHead", "Hoodie", "RobotHead", "RobotPlayerHead", "SlavemasterMask", "WerewolfHead", } },
			{ "HatBlue",        new string[] { "AlienHead", "AssassinMask", "ButlerBotHead", "CopBotHead", "GorillaHead", "Hoodie", "RobotHead", "RobotPlayerHead", "SlavemasterMask", "WerewolfHead", } },
			{ "HatRed",         new string[] { "AlienHead", "AssassinMask", "ButlerBotHead", "CopBotHead", "GorillaHead", "Hoodie", "RobotHead", "RobotPlayerHead", "SlavemasterMask", "WerewolfHead", } },
			{ "Headphones",     new string[] { "AlienHead", "AssassinMask", "ButlerBotHead", "CopBotHead", "GorillaHead", "Hoodie", "RobotHead", "RobotPlayerHead", "SlavemasterMask", "WerewolfHead", } },
			{ "ThiefHat",       new string[] { "AlienHead", "AssassinMask", "ButlerBotHead", "CopBotHead", "GorillaHead", "Hoodie", "RobotHead", "RobotPlayerHead", "SlavemasterMask", "WerewolfHead", } },
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Mask_Override>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Normally, masks deactivate most accessories except eyewear. This disables that function.",
					[LanguageCode.Spanish] = "Normalmente la mayoria de no-peinados desactivan el uso de accesorios, con esto esta funcion se ignora.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Mask_Override)),
					[LanguageCode.Spanish] = "Máscaras Compatibles",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}