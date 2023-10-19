using BunnyLibs;
using CCU.Traits.Hire_Duration;
using RogueLibsCore;

namespace CCU.Mutators.Followers
{
	class Homesickness_Disabled : M_Followers, ISetupAgentStats
	{
		public Homesickness_Disabled() : base(nameof(Homesickness_Disabled), true) { }

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Homesickness_Disabled())
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

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			if (!agent.HasTrait<Homesickly>())
				agent.canGoBetweenLevels = true;
		}
	}
}