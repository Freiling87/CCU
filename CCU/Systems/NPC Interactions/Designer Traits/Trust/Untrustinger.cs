﻿using RogueLibsCore;
using System;

namespace CCU.Interactions.Interaction_Gate
{
	public class Untrustinger : T_Trust
	{
		public override int MinimumRelationship => 4;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Untrustinger>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will only interact with Loyal or better.\n\n" +
					"Exceptions: \n" +
					"- Leave Weapons Behind\n" +
					"- Offer Motivation\n" +
					"- Pay Debt"),
					[LanguageCode.Spanish] = String.Format("Este NPC solo interactura con quienes sean Leales o mejor.\n\n" +
					"Excepciónes: \n" +
					"- Dejar Armas\n" +
					"- Ofrecer Motivacion\n" +
					"- Pagar Deuda"),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Untrustinger)),
					[LanguageCode.Spanish] = "Muy Desconfiado",
				})
				.WithUnlock(new TU_DesignerUnlock{});
		}		
	}
}