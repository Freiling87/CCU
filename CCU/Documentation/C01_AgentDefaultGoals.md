<p align="left">
<img src="../Images/CCU_160x160.png" alt="CCU Logo" align="left">
<img src="../Images/CCU_160x160.png" alt="Yeah there are two, so what" align="right">
</p>

<h1 align="center">
<br>
Agent Default Goals
</h1>
<br><br>

Default Goals are where you define how an Agent placed in a chunk will act. E.g., "Wander In Chunk (Owner)." 

##		Scene Setters
Scene Setters are for ambience. They trigger immediately when the level loads. Note that these are compatible with Explode on Death, so you can use these to make a lot of adjustments to your level.

These may not work if the agent starts in a position that will cause them to panic (near danger or in un-owned space). Set their Owner ID to 99 to prevent at least some of those issues.

|Name								|Notes	|
|:----------------------------------|:------|
|Arrested							|- Agent starts out arrested
|Burned								|- Agent starts out dead & burning
|Dead								|- Agent starts out dead
|Gibbed								|- Agent starts out gibbed
|Knocked Out						|- Agent starts out unconscious
|Zombified							|- Agent starts out dead...