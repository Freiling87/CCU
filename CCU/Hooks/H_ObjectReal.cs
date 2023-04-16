using RogueLibsCore;

namespace CCU.Hooks
{
	public class H_ObjectReal : HookBase<ObjectReal>
	{
		// Instance = host ObjectReal

		protected override void Initialize() { }

		public InvItem stashHint = null;
		public Agent stashHintHolder = null;
		public bool stashDiscovered = false;
	}
}
