using RogueLibsCore;

namespace CCU.Traits
{
	public abstract class T_PlayerTrait : T_CCU
	{
		public T_PlayerTrait() : base() { }

		//	IRefreshPerLevel
		public bool AlwaysApply => false;

		public static new TraitBuilder PostProcess
		{
			// This is applied after each trait is created. I don't fully get how it works, but see where PP's value is assigned.
			set
			{
				// Examples from parent: 
				//value.Unlock.Unlock.cantLose = true;
				//value.Unlock.Unlock.cantSwap = true;
				//value.Unlock.Unlock.upgrade = null;
			}
		}
	}
}
