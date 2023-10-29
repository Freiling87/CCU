using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CCU.Traits.Rel_Faction
{
	public abstract class T_Rel_Faction : T_Relationships
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();

		public T_Rel_Faction() : base() { }

		public abstract int Faction { get; }
		public abstract Alignment FactionAlignment { get; }

		public enum Alignment
		{
			Aligned,
			Annoyed,
			Friendly,
			Hateful,
			Loyal,
			Neutral,
			Submissive
		}

		public static string GetFactionRelationship(Agent agent, int faction) =>
			agent.GetTraits<T_Rel_Faction>().FirstOrDefault(t => t.Faction == faction)?.FactionAlignment.ToString() ?? VRelationship.Neutral;

		public static Alignment? GetFactionAlignment(Agent agent, int faction) =>
			agent.GetTraits<T_Rel_Faction>().FirstOrDefault(t => t.Faction == faction)?.FactionAlignment ?? Alignment.Neutral;

		public static class AlignmentUtils
		{
			public static bool CountsAsBlahd(Agent agent) =>
				agent.agentName == VanillaAgents.GangsterBlahd
				|| agent.HasTrait<Blahd_Aligned>();

			public static bool CountsAsCrepe(Agent agent) =>
				agent.agentName == VanillaAgents.GangsterCrepe
				|| agent.HasTrait<Crepe_Aligned>();

			public static Alignment FromString(string alignmentString) =>
				Enum.TryParse(alignmentString, true, out Alignment alignment)
					? alignment
					: Alignment.Neutral;

			// i.e. how much do I care about your opinion, if my opinion is {X}
			public static Dictionary<Alignment, float> alignmentWeights = new Dictionary<Alignment, float>() {

					{ Alignment.Hateful,    -1.00f },
					{ Alignment.Annoyed,    -0.25f },
					{ Alignment.Neutral,     0.00f },
					{ Alignment.Friendly,    0.25f },
					{ Alignment.Loyal,       0.50f },
					{ Alignment.Aligned,     1.00f },
				};

			/// <summary>
			/// 
			/// </summary>
			/// <param name="self"></param>
			/// <param name="other"></param>
			/// <returns>
			/// Float between [-1, +1] | -1 being strong disagreement, +1 strong agreement
			/// </returns>
			public static float GetAgreementStrength(Alignment self, Alignment other)
			{
				//  TryGet returns only a bool. The out is assigning the actual dict value to self/otherWeight.
				float selfWeight = alignmentWeights.TryGetValue(self, out selfWeight) ? selfWeight : 0;
				float otherWeight = alignmentWeights.TryGetValue(other, out otherWeight) ? otherWeight : 0;

				if (selfWeight < 0 && otherWeight < 0)
					return 0;

				return selfWeight * otherWeight;
			}

			public static Alignment GetAlignmentFromAgreement(float agreement)
			{
				float alignmentStrength = Mathf.Clamp01(Mathf.Abs(agreement));
				Alignment[] alignmentsOrderedAscending;

				if (agreement > 0)
					alignmentsOrderedAscending = new[]
					{
						Alignment.Neutral,
						Alignment.Friendly,
						Alignment.Loyal,
						Alignment.Aligned
					};
				else
					alignmentsOrderedAscending = new[]
					{
						Alignment.Neutral,
						Alignment.Annoyed,
						Alignment.Hateful
					};

				int alignmentIndex = Mathf.RoundToInt((alignmentsOrderedAscending.Length - 1) * alignmentStrength);

				return alignmentsOrderedAscending[alignmentIndex];
			}

			public static Alignment GetAverageAlignment(Agent thisAgent, Agent otherAgent)
			{
				try
				{
					float averageAgreementStrength =
						Enumerable.Range(1, 4)
						.Select(faction => new
						{
							thisAlignment = GetFactionAlignment(thisAgent, faction),
							otherAlignment = GetFactionAlignment(otherAgent, faction)
						})
						.Where(alignments => alignments.thisAlignment != Alignment.Neutral || alignments.otherAlignment != Alignment.Neutral) // May be 0
						.Select(alignments => GetAgreementStrength(alignments.thisAlignment.Value, alignments.otherAlignment.Value))
						.Average();

					return GetAlignmentFromAgreement(averageAgreementStrength);
				}
				catch
				{
					return Alignment.Neutral;
				}
			}
		}
	}
}