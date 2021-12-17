using System;
using Shared.Sources.CustomDrawers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class ConstantStringKvpAttribute : ConstantStringAttribute
{
        
    public ConstantStringKvpAttribute(Type source, bool flatten = false) : base(source, flatten)
    {
    }
}