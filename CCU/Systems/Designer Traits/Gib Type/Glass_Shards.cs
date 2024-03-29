﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Gib_Type
{
	public class Glass_Shards : T_GibType
	{
		public override string audioClipName => VanillaAudio.WallDestroyGlass;
		public override DecalSpriteName gibDecal => DecalSpriteName.None;
		public override int gibQuantity => 12;
		public override int gibSpriteIteratorLimit => 5;
		public override GibSpriteNameStem gibType => GibSpriteNameStem.WindowWreckage;
		public override string particleEffect => "ObjectDestroyedSmoke";

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Glass_Shards>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character is some kind of crystalline lifeform. More of a carbon-based guy myself, but I respect their lifestyle choice of being born that way."),
					[LanguageCode.Spanish] = "Este NPC esta echo de vidrio por lo que no te recomiendo probarlo, recuerda que el plato no se come!!!",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Glass_Shards)),
					[LanguageCode.Spanish] = "Vidrio",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		
	}
}