using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetingRoomBooking.Service.Implements;
using FluentAssertions;
using MapsterMapper;
using MeetingRoomBooking.Repository.DataModels;
using MeetingRoomBooking.Repository.Interfaces;
using MeetingRoomBooking.Repository.ParameterModels;
using MeetingRoomBooking.Service.ParameterDtos;
using NSubstitute;
using Yungching.Web.Knowhow.Application.Test;

namespace MeetingRoomBooking.Service.Implements.Tests
{
    [TestClass()]
    public class BookingServiceTests
    {
        private IBookingRepository _bookingRepository;
        private IMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _mapper = TestHook.getConfig();
            _bookingRepository = Substitute.For<IBookingRepository>();
        }

        private BookingService GetSystemUnderTest()
        {
            var sut = new BookingService(_bookingRepository, _mapper);
            return sut;
        }

        [TestMethod()]
        public async Task IsBooked_驗證為True的情境()
        {
            //Assert.Fail();

            //arrange
            var parameter = new GetBookingParameterDto
            {
                FirstDay = new DateTime(2025, 1, 2),
                StartTime = "10:00",
                EndTime = "10:30",
                RoomId = "201",
            };

            var booking1 = new TMeetingBooking 
            { 
                StartTime = new DateTime(2025, 1, 2, 9, 0, 0), 
                EndTime = new DateTime(2025, 1, 2, 11, 0, 0), 
                RoomId = "201", 
                Id = 1 
            };
            var booking2 = new TMeetingBooking 
            { 
                StartTime = new DateTime(2025, 1, 2, 13, 0, 0), 
                EndTime = new DateTime(2025, 1, 2, 15, 0, 0), 
                RoomId = "201",
                Id = 2 
            };
            var booking3 = new TMeetingBooking
            { 
                StartTime = new DateTime(2025, 1, 2, 15, 0, 0), 
                EndTime = new DateTime(2025, 1, 2, 16, 0, 0), 
                RoomId = "201",
                Id = 3 
            };

            var mappedParameter = _mapper.Map<GetBookingParameterModel>(parameter);
            _bookingRepository.GetBookingsForCheckAsync(mappedParameter)
            .Returns(new List<TMeetingBooking> { booking1, booking2, booking3 });

            //act
            var actual = await GetSystemUnderTest().IsBooked(parameter);

            //assert
            actual.Should().BeTrue();
        }

        [TestMethod()]
        public async Task IsBooked_驗證為False的情境()
        {
            //arrange
            var parameter = new GetBookingParameterDto
            {
                FirstDay = new DateTime(2025, 1, 2),
                StartTime = "09:00",
                EndTime = "10:00",
                RoomId = "201",
            };

            var booking1 = new TMeetingBooking 
            { 
                StartTime = new DateTime(2025, 1, 2, 10, 0, 0), 
                EndTime = new DateTime(2025, 1, 2, 11, 0, 0), 
                RoomId = "201", 
                Id = 1 
            };
            var booking2 = new TMeetingBooking 
            { 
                StartTime = new DateTime(2025, 1, 2, 13, 0, 0), 
                EndTime = new DateTime(2025, 1, 2, 15, 0, 0),
                RoomId = "201", 
                Id = 2 
            };
            var booking3 = new TMeetingBooking 
            { 
                StartTime = new DateTime(2025, 1, 2, 15, 0, 0), 
                EndTime = new DateTime(2025, 1, 2, 16, 0, 0), 
                RoomId = "201", 
                Id = 3 
            };

            _bookingRepository.GetBookingsForCheckAsync(_mapper.Map<GetBookingParameterModel>(parameter))
            .Returns(new List<TMeetingBooking> { booking1, booking2, booking3 });

            //act
            var actual = await GetSystemUnderTest().IsBooked(parameter);

            //assert
            actual.Should().BeFalse();
        }
    }
}