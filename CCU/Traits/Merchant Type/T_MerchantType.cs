namespace CCU.Traits.Merchant_Type
{
    public abstract class T_MerchantType : T_CCU
    {
        public T_MerchantType() : base() { }

        public string DisplayName => DesignerName(GetType());
    }
}