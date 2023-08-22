using RogueLibsCore;

namespace CCU.Mutators.Followers
{
	class Homesickness_Disabled : M_Followers
	{
		public Homesickness_Disabled(string v1, bool v2) : base(v1, v2) { }

		[RLSetup]
		static void Start()
		{
			UnlockBuilder unlockBuilder = RogueLibs.CreateCustomUnlock(new Homesickness_Disabled(nameof(Homesickness_Disabled), true)) 
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Followers act as if their employer had Homesickness Killer. Homesickness Killer is removed from the trait choice pool.",
                    [LanguageCode.Spanish] = "Seguidores actuan como si tuvieras Siempre Acompañado.  Siempre Acompañado deja de aparecer en la lista de rasgos al subir de nivel.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Homesickness_Disabled)),
                    [LanguageCode.Spanish] = "Siempre Siempre Acompañado",
                });
		}
	}
}