using FluentValidation;
using MeetingRoomBooking.Common.Models;

namespace MeetingRoomBooking.Common.Infrastructure.Validators
{
    public class BookingViewModelValidator : AbstractValidator<BookingViewModel>
    {
        public BookingViewModelValidator()
        {           
            RuleFor(b => b.Id)
                .Must(b => int.TryParse(b.ToString(),out int result) )
                .WithMessage("ID必須為整數");

            RuleFor(b => b.RoomId)
                .NotEmpty().WithMessage("RoomId不得為空白");

            RuleFor(b => b.Subject)
                .NotEmpty().WithMessage("申請事由不得為空白")
                .MaximumLength(50).WithMessage("申請事由不得超過50字");

            RuleFor(b => b.BookingUserId)
                .NotNull().WithMessage("申請人不得為空白")
                .NotEmpty().WithMessage("申請人不得為空白");


            RuleFor(b => b.StartDate)
                .NotNull().WithMessage("申請日期不得為空白")
                .NotEmpty().WithMessage("申請日期不得為空白")
                .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("日期格式錯誤");

            RuleFor(b => b.StartTime)
                .NotNull().WithMessage("起始時間不得為空白")
                .NotEmpty().WithMessage("起始時間不得為空白")
                .Matches(@"^\d{2}:\d{2}$").WithMessage("起始時間格式錯誤");

            RuleFor(b => b.EndTime)
                .NotNull().WithMessage("結束時間不得為空白")
                .NotEmpty().WithMessage("結束時間不得為空白")
                .Matches(@"^\d{2}:\d{2}$").WithMessage("結束時間格式錯誤");
        }
    }
}
