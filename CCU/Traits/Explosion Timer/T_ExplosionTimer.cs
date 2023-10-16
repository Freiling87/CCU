using RogueLibsCore;
using System;

namespace CCU.Traits.Explosion_Modifier
{
	public abstract class T_ExplosionTimer : T_CCU
	{
		public T_ExplosionTimer() : base() { }

		public abstract float TimeFactor { get; }

		public static float ExplosionTimerDuration(Agent agent)
		{
			float duration = 1.5f;

			foreach (T_ExplosionTimer trait in agent.GetTraits<T_ExplosionTimer>())
				duration *= trait.TimeFactor;

			return Math.Max(0.01f, duration);
		}
	}
}