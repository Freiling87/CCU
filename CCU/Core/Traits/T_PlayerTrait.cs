using RogueLibsCore;
using System;

namespace CCU
{
	public abstract class T_PlayerTrait : CustomTrait
	{
		public T_PlayerTrait() : base() { }

		public static TraitBuilder PostProcess_PlayerTrait
		{
			set
			{
				//	YAGNI most likely, but it will be more work to remove and possibly re-add it. Leave it for now.
			}
		}

		public static string PlayerName(Type type) =>
			Prettify(type.Name);

		public static string Prettify(string original) =>
			original
				.Replace('_', ' ')
				.Replace("2", "+")
				.Trim();

		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}