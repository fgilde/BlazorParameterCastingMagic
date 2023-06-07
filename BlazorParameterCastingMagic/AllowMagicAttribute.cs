namespace BlazorParameterCastingMagic;

public class AllowMagicCastingAttribute : Attribute
{
    public Type[]? AllowedTypes { get; set; }

    public AllowMagicCastingAttribute(params Type[] allowedTypes)
    {
        AllowedTypes = allowedTypes;
    }

    public bool IsAllowed(Type type)
    {
        return AllowedTypes == null
               || AllowedTypes.Length == 0
               || AllowedTypes.Contains(type);
    }
}