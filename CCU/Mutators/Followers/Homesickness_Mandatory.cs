using BunnyLibs;
using CCU.Traits.Hire_Duration;
using RogueLibsCore;

namespace CCU.Mutators.Followers
{
	class Homesickness_Mandatory : M_Followers, ISetupAgentStats
	{
		public Homesickness_Mandatory() : base(nameof(Homesickness_Mandatory), true) { }

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Homesickness_Mandatory())
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Followers will stay behind when the level is completed, overriding Homesickness Killer. Homesickness Killer is removed from the trait choice pool.",
					[LanguageCode.Spanish] = "Tus seguidores siempre te dejaran al subir de piso, Siempre Acompañado deja de aparecer tanto como rasgo inicial y opcion al subir de nivel.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Homesickness_Mandatory)),
					[LanguageCode.Spanish] = "Acompañamento Desactivado",
				});
		}

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			if (!agent.HasTrait<Homesickless>())
				agent.canGoBetweenLevels = false;
		}
	}
}