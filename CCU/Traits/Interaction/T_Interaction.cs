namespace CCU.Traits.Interaction
{
    public abstract class T_Interaction : T_CCU
    {
        public T_Interaction() : base() { }

        /// <summary>
        /// Allow interaction past threshold of Untrusting traits. 
        /// <para><b>True:</b> Leave Weapons, Offer Motivation, Pay Debt, Pay Entrance Fee</para>
        /// <para><b>False:</b> Most others</para>
        /// </summary>
        public abstract bool AllowUntrusted { get; }
        /// <summary>
        /// Back-end unique identifier for button.
        /// <para><b>Use:</b> CButtonText or VButtonText</para>
        /// </summary>
        public abstract string ButtonID { get; }
        /// <summary>
        /// Bypass cost display for free actions.
        /// <para><b>True:</b> Administer Blood Bag, Give Blood</para>
        /// <para><b>False:</b> Most others</para>
        /// </summary>
        public abstract bool HideCostInButton { get; }
        /// <summary>
        /// Special Strings for PlayfieldObject.DetermineMoneyCost
        /// <para><b>Use:</b> VDetermineMoneyCost</para>
        /// </summary>
        public abstract string DetermineMoneyCostID { get; }
    }
}