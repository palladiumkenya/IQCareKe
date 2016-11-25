using System;

namespace Application.Interface
{
    public interface IBusinessServerFactory
    {
        Object CreateInstance(string type);
    }
}
