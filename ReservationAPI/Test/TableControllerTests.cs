using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReservationAPI.Controller;
using ReservationAPI.Handler.Model;
using ReservationAPI.Model;
using ReservationAPI.Service.Abstract;
using ReservationAPI.Service.Concrete;

[TestClass]
public class TableControllerTests
{
    [TestMethod]
    public async Task GetAll_ReturnsOkResultWithData()
    {
        var tableServiceMock = new Mock<ITableService>();
        tableServiceMock.Setup(service => service.GetTablesAsync())
            .ReturnsAsync(new List<Table>()); // You can customize this based on your needs

        var controller = new TableController();

        var result = await controller.GetAll() as OkObjectResult;

        var response = (ResponseModel<List<Table>>)((OkObjectResult)result).Value;

        Assert.IsNotNull(response.Data);
        Assert.IsFalse(response.HasError);
    }

    [TestMethod]
    public async Task Save_WithValidTable_ReturnsOkResultWithData()
    {
        var tableServiceMock = new Mock<ITableService>();
        tableServiceMock.Setup(service => service.GetTableByNumberAsync(It.IsAny<string>()))
            .ReturnsAsync((Table)null);

        var controller = new TableController();

        var validTable = new Table { Number = "A-5", Capacity = 12 };

        var result = await controller.Save(validTable) as OkObjectResult;

        var response = (ResponseModel<bool>)((OkObjectResult)result).Value;

        Assert.IsTrue(response.Data);
        Assert.IsFalse(response.HasError);
    }

}
