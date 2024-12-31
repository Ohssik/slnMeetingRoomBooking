using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using NSubstitute;
using MeetingRoomBooking.Service.DTOs;
using MeetingRoomBooking.Repository.Interfaces;
using AutoMapper;
using MeetingRoomBooking.ServiceTests;
using System.Collections.Generic;
using MeetingRoomBooking.Repository.Models;

namespace MeetingRoomBooking.Service.Implements.Tests
{
    [TestClass()]
    public class MeetingBookingServiceTests
    {
        private IBookingRepository _bookingRepository;
        private IRoomRepository _roomRepository;
        private IMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _mapper = TestHook.MapperConfigurationProvider.CreateMapper();
            _bookingRepository = Substitute.For<IBookingRepository>();
            _roomRepository = Substitute.For<IRoomRepository>();
        }

        private BookingService GetSystemUnderTest()
        {
            var sut = new BookingService(_roomRepository, _bookingRepository, _mapper);
            return sut;
        }

        [TestMethod()]
        [TestCategory("MeetingBookingService")]
        [TestProperty("MeetingBookingService", "IsBooked")]
        public async Task IsBooked_驗證為True的情境()
        {
            //arrange
            CheckPeriodDto checkPeriodDto = new CheckPeriodDto()
            {
                TargetDay = new DateOnly(2023, 4, 15),
                StartTime = "10:00",
                EndTime = "12:00",
                RoomId = "201",

            };

            MeetingBookingModel booking1 = new MeetingBookingModel { StartTime = new DateTime(2023,4,15,9,0,0), EndTime = new DateTime(2023, 4, 15, 11, 0, 0), RoomId="201",Id=1 };
            MeetingBookingModel booking2 = new MeetingBookingModel { StartTime = new DateTime(2023, 4, 15, 14, 0, 0), EndTime = new DateTime(2023, 4, 15, 15, 0, 0), RoomId = "201", Id = 2 };
            MeetingBookingModel booking3 = new MeetingBookingModel { StartTime = new DateTime(2023, 4, 15, 15, 0, 0), EndTime = new DateTime(2023, 4, 15, 16, 0, 0), RoomId = "201", Id = 3 };

             _bookingRepository.GetBookingsForCheckAsync(checkPeriodDto.TargetDay, checkPeriodDto.RoomId, checkPeriodDto.Id.GetValueOrDefault())
            .Returns(new List<MeetingBookingModel> { booking1, booking2, booking3 }.AsEnumerable());           

            //act
            var actual = await GetSystemUnderTest().IsBooked(checkPeriodDto);

            //assert
            actual.Should().Be(true);
        }

        [TestMethod()]
        [TestCategory("MeetingBookingService")]
        [TestProperty("MeetingBookingService", "IsBooked")]
        public async Task IsBooked_驗證為False的情境()
        {
            //arrange
            CheckPeriodDto checkPeriodDto = new CheckPeriodDto()
            {
                TargetDay = new DateOnly(2023, 4, 15),
                StartTime = "16:00",
                EndTime = "17:00",
                RoomId = "201",

            };

            MeetingBookingModel booking1 = new MeetingBookingModel { StartTime = new DateTime(2023, 4, 15, 9, 0, 0), EndTime = new DateTime(2023, 4, 15, 11, 0, 0), RoomId = "201", Id = 1 };
            MeetingBookingModel booking2 = new MeetingBookingModel { StartTime = new DateTime(2023, 4, 15, 14, 0, 0), EndTime = new DateTime(2023, 4, 15, 15, 0, 0), RoomId = "201", Id = 2 };
            MeetingBookingModel booking3 = new MeetingBookingModel { StartTime = new DateTime(2023, 4, 15, 15, 0, 0), EndTime = new DateTime(2023, 4, 15, 16, 0, 0), RoomId = "201", Id = 3 };

            _bookingRepository.GetBookingsForCheckAsync(checkPeriodDto.TargetDay, checkPeriodDto.RoomId, checkPeriodDto.Id.GetValueOrDefault())
           .Returns(new List<MeetingBookingModel> { booking1, booking2, booking3 }.AsEnumerable());

            //act
            var actual = await GetSystemUnderTest().IsBooked(checkPeriodDto);

            //assert
            actual.Should().Be(false);
        }
    
    }
}