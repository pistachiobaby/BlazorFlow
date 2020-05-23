using System;

namespace BlazorFlow.Models
{
    public partial class FlowCondition
    {
        private readonly Func<decimal, decimal, bool>? decimalOperation;
        private readonly decimal? requiredDecimal;

        // Second set of optional parameters used for ranges
        private readonly Func<decimal, decimal, bool>? optionalDecimalOperation;
        private readonly decimal? optionalDecimal;

        public FlowCondition(Func<decimal, decimal, bool> decimalOperation, decimal requiredDecimal, Func<decimal, decimal, bool>? optionalDecimalOperation = null, decimal? optionalDecimal = null)
        {
            this.decimalOperation = decimalOperation;
            this.requiredDecimal = requiredDecimal;
            this.optionalDecimalOperation = optionalDecimalOperation;
            this.optionalDecimal = optionalDecimal;
        }

        public bool Evaluate(decimal? userDecimal)
        {
            if (userDecimal is {} ud && decimalOperation is {} dop && requiredDecimal is {} rd)
            {
                if (optionalDecimalOperation is {} odop && optionalDecimal is {} od)
                {
                    return dop(ud, rd) && odop(ud, od);
                }
                else
                {
                    return dop(ud, rd);
                }
            }
            else
            {
                return false;
            }
        }
    }
}