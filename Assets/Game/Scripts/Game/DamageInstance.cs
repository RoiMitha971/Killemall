public struct DamageInstance
{
    public int Amount;
    public DamageType Type;

    public DamageInstance(int amount, DamageType type)
    {
        Amount = amount;
        Type = type;
    }
}