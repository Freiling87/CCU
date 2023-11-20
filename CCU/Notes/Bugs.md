##			Bugs
Except crickets. Crickets are fine.
###			C	Missing Mayor Badge Interaction
For vanilla clerk
This would be a good time to refactor those.
###			C	Concealed Carry Sitting
CL https://discord.com/channels/187414758536773632/1003391847902740561/1126306577008316508
	Concealed carrier still doesn't work with sitting NPCs until something briefly updates their behavior (like a noise making them stand up and look, then sit back down).
Attempted:
	AgentHitbox
		.UpdateAnim		Prefix
	GoalSleepReal
		.Activate		Prefix
		.Process		Prefix
	GoalSitReal
		.Activate		Prefix
###			C	Meat Chunks
Shows up on char sheet, NOT playing from loaded save but YES playing from continue