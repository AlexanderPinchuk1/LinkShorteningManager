using System.ComponentModel.DataAnnotations;

namespace LinkShorteningManager.Domain.Attributes
{
    [AttributeUsage(
       AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
       AllowMultiple = false)]
    public class NotInFutureAttribute : ValidationAttribute
    {
        public NotInFutureAttribute()
            : base()
        {
        }



        public override bool IsValid(object? value)
        {
            if(value == null)
            {
                return false;
            }

            return Convert.ToDateTime(value) <= DateTime.Now;
        }
    }
}
