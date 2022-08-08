using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Challenges
{
    public static class MutatorManager
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static string ActiveMutatorNameFromList(List<Type> mutators)
		{
			foreach (Type mutator in mutators)
			{
				string name = MutatorName(mutator);

				if (GC.challenges.Contains(name))
					return name;
			}

			logger.LogError("GetActiveMutatorFromList: Null return not expected");
			return null;
		}
		public static Type ActiveMutatorFromList(List<Type> mutators)
		{
			foreach (Type mutator in mutators)
			{
				string name = MutatorName(mutator);

				if (GC.challenges.Contains(name))
					return mutator;
			}

			logger.LogError("GetActiveMutatorFromList: Null return not expected");
			return null;
		}
		public static bool IsMutatorFromListActive(List<Type> list)
		{
			foreach (Type challenge in list)
				if (GC.challenges.Contains(challenge.Name))
					return true;

			return false;
		}
		public static string MutatorName(Type mutator)
		{
			CustomNameProvider provider = RogueFramework.NameProviders.OfType<CustomNameProvider>().First();
			return provider.CustomNames[NameTypes.StatusEffect][mutator.Name].GetCurrent();
		}
		public static List<string> MutatorNames(List<Type> mutators)
		{
			CustomNameProvider provider = RogueFramework.NameProviders.OfType<CustomNameProvider>().First();
			List<string> result = new List<string>();

			foreach (Type mutator in mutators)
				result.Add(provider.CustomNames[NameTypes.StatusEffect][mutator.Name].GetCurrent());

			return result;
		}
	}
}
