##			Bugs
Except crickets. Crickets are fine.
###			T	Exempt Scene Setter NPCs from possession
Attempted
###			C	Z-Infected Appearance
Custom agent with random appearance, Z-Infected
When multiple of them die, they all use the same rolls for their appearance.
Relationships.CopyLooks appears to be the culprit

My main issue is that I've been sloppy and ad hoc with storing appearance data.
To clarify:
  - Let's see what happens if we never mess with SaveCharacterData. This might only have been used to make it appear dynamically in character select, but that's not necessary.
  - All ROLLED appearance can be stored in agent.AgentHitboxScript, as proven in Relationships.CopyLooks.
  - Eliminate appearance hooks if possible.
###			C	0-Count items in Shop
https://discord.com/channels/187414758536773632/1003391847902740561/1126283884959629312
Demo Depot/ Mining Gear has 0-qty Bomb processor
###			C	Concealed Carry Sitting
CL https://discord.com/channels/187414758536773632/1003391847902740561/1126306577008316508
	Concealed carrier still doesn't work with sitting NPCs until something briefly updates their behavior (like a noise making them stand up and look, then sit back down).
###			C	Meat Chunks
Shows up on char sheet, NOT playing from loaded save but YES playing from continue