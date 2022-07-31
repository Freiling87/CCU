namespace CCU.Traits.Merchant_Type
{
    public abstract class T_MerchantType : T_CCU
    {
        public T_MerchantType() : base() { }

        new public string DisplayName => DesignerName(GetType());
    }
}