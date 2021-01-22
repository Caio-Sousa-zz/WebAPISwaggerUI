using System;

namespace Codtran.Api.Helpers.Attribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class HideSwaggerDocAttribute : System.Attribute
    {
    }
}