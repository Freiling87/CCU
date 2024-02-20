namespace CCU.Systems.Language
{
	public abstract class T_Language : T_PlayerTrait
	{
		public T_Language() : base() { }
		public abstract string[] VanillaSpeakers { get; }
		public abstract string[] LanguageNames { get; }
	}
}