using RogueLibsCore;

namespace CCU.Mutators.Quest_Gen
{
	public class Client_Network : M_QuestGen
	{
		public Client_Network() : base(nameof(Client_Network), true) { }

		[RLSetup]
		static void Start()
		{
			RogueLibs.CreateCustomUnlock(new Client_Network())
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