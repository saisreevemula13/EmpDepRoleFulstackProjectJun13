using System;

namespace EmpDepRoleFulstackProjectJun13.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message)
        {
        }
    }
}

