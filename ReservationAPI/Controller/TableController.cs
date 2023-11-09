using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Handler.Model;
using ReservationAPI.Model;
using ReservationAPI.Service.Abstract;
using ReservationAPI.Service.Concrete;
using ReservationAPI.Validators;

namespace ReservationAPI.Controller
{
    [ApiController]
    public class TableController : ControllerBase
    {
        private ITableService _tableService;

        public TableController()
        {
            _tableService = new TableService();
        }

        /// <summary>
        /// Get All Tables
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("table/getall")]
        public async Task<IActionResult> GetAll()
        {
            ResponseModel<List<Table>> response = new ResponseModel<List<Table>>();

            try
            {
                response.Data = await _tableService.GetTablesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = "Exception => " + ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// Save Table
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "Number": "A-5",
        ///        "Capacity": 12 
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("table/save")]
        public async Task<IActionResult> Save([FromBody]Table table)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                var tableValidator = new TableValidator();
                var validationResult = tableValidator.Validate(table);

                if (!validationResult.IsValid && validationResult.Errors.Any())
                {
                    response.HasError = true;
                    validationResult.Errors.ForEach(x => response.ValidationErrors.Add(new ValidationError(x.PropertyName, x.ErrorMessage)));
                    response.Message = "Masa eklenemedi.";
                    return BadRequest(response);
                }

                if (await _tableService.GetTableByNumberAsync(table.Number) != null)
                {
                    response.HasError = true;
                    response.Message = table.Number + " isimli masa zaten eklenmiş farklı bir isim kullanınız.";
                    return Ok(response);
                }

                await _tableService.SaveTableAsync(table);
                response.Data = true;
                response.Message = "Masa eklendi.";

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = "Exception => " + ex.Message;
                return Ok(response);
            }
        }
    }
}
