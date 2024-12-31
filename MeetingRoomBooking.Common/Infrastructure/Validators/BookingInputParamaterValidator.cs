using FluentValidation;
using MeetingRoomBooking.Common.Models;

namespace MeetingRoomBooking.Common.Infrastructure.Validators
{
    public class BookingInputParamaterValidator:AbstractValidator<BookingInputParamater>
    {
        public BookingInputParamaterValidator() 
        {
            
            this.RuleFor(b => b.Subject)                                
                .MaximumLength(50).WithMessage("申請事由不得超過50字");

            this.RuleFor(b => b.TargetDate)                
                .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("日期格式錯誤");

            this.RuleFor(b => b.SortBy)
                .NotEmpty().NotNull();

            this.RuleFor(b => b.PageIndex)                
                .GreaterThanOrEqualTo(1).WithMessage("PageIndex必須大於等於1");

            this.RuleFor(b => b.PageSize)                
                .GreaterThanOrEqualTo(1).WithMessage("PageSize必須大於等於1");
            

        }
    }
}
