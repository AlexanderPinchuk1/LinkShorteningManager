using System.ComponentModel.DataAnnotations;

namespace LinkShorteningManager.Domain.Attributes
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class NotEmptyAttribute : ValidationAttribute
    {
        public NotEmptyAttribute()
            : base()
        {
        }



        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            return value switch
            {
                Guid guid => guid != Guid.Empty,
                _ => true,
            };
        }
    }
}
