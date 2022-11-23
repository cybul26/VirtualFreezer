using VirtualFreezer.Shared.Abstractions.Exceptions;

namespace VirtualFreezer.Shared.Abstractions.Domain
{
    public class BusinessRuleValidationException : Exception
    {
        public IBusinessRule BrokenRule { get; }


        public BusinessRuleValidationException(IBusinessRule brokenRule)
            : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
        }
    }
}