using System;
using UnityEngine;

namespace Shared.Sources.CustomDrawers
{
    public class ConstantStringAttribute : PropertyAttribute
    {
        private readonly Type _source;

        public Type Source => _source;

        public ConstantStringAttribute(Type source)
        {
            _source = source;
        }
    }
}
