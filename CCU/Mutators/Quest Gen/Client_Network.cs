using RogueLibsCore;

namespace CCU.Mutators.Quest_Gen
{
	public class Client_Network : M_QuestGen
	{

		// MOVE TO RESISTANCE HR

		public Client_Network(string v1, bool v2) : base(v1, v2) { }

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Client_Network(nameof(Client_Network), true))
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "More NPC types are able to assign special quests.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Client_Network)),
				});
		}
	}
}