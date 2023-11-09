using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReservationAPI.Controller;
using ReservationAPI.Handler.Abstract;
using ReservationAPI.Handler.Model;
using ReservationAPI.Model;
using ReservationAPI.Service.Abstract;

namespace ReservationAPI.Test
{
    [TestClass]
    public class ReservationControllerTests
    {
        [TestMethod]
        public async Task GetByDate_ValidDate_ReturnsOkResult()
        {
            var controller = new ReservationController(new Mock<IMailHandler>().Object);
            var date = DateTime.Now;

            var result = await controller.GetByDate(date) as OkObjectResult;
            var response = result.Value as ResponseModel<List<Reservation>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.IsNotNull(response.Data);
        }

        [TestMethod]
        public async Task Save_ValidReservation_ReturnsOkResult()
        {
            var mailHandlerMock = new Mock<IMailHandler>();
            var reservationServiceMock = new Mock<IReservationService>();
            var tableServiceMock = new Mock<ITableService>();
            var controller = new ReservationController(mailHandlerMock.Object)
 ;

            var validReservation = new Reservation
            {
                CustomerName = "Mert Demirkıran",
                CustomerEmail = "mertdmkrn@example.com",
                Date = DateTime.Now.AddDays(1),
                Description = "Test reservation",
                GuestCount = 3
            };

            reservationServiceMock.Setup(x => x.SaveReservationAsync(It.IsAny<Reservation>())).Verifiable();
            tableServiceMock.Setup(x => x.GetOptimalTableAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(new Table { Id = Guid.NewGuid(), Number = "A1" });

            var result = await controller.Save(validReservation) as OkObjectResult;
            var response = result.Value as ResponseModel<bool>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.IsTrue(response.Data);
            reservationServiceMock.Verify(x => x.SaveReservationAsync(validReservation), Times.Once);
            mailHandlerMock.Verify(x => x.SendEmailAsync(It.IsAny<MailRequest>()), Times.Once);
        }
    }
}
