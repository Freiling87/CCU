namespace CCU.Traits.Merchant_Type
{
    public abstract class T_MerchantType : T_CCU, ISetupAgentStats
    {
        public T_MerchantType() : base() { }

        public string DisplayName => DesignerName(GetType());

        public void SetupAgentStats(Agent agent)
        {
            agent.SetupSpecialInvDatabase();
        }
    }
}