using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cmp;
using ReservationAPI.Handler.Abstract;
using ReservationAPI.Handler.Model;
using ReservationAPI.Model;
using ReservationAPI.Service.Abstract;
using ReservationAPI.Service.Concrete;
using ReservationAPI.Validators;
using System.Xml.Linq;

namespace ReservationAPI.Controller
{
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservationService _reservationService;
        private ITableService _tableService;
        private readonly IMailHandler _mailHandler;  

        public ReservationController(IMailHandler mailHandler)
        {
            _reservationService = new ReservationService();
            _tableService = new TableService();
            _mailHandler = mailHandler;
        }

        /// <summary>
        /// Get Reservations By Date
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("reservation/getbydate")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            ResponseModel<List<Reservation>> response = new ResponseModel<List<Reservation>>();

            try
            {
                response.Data = await _reservationService.GetReservationsByDateAsync(date);
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
        /// Save Reservation
        /// </summary>
        /// <remarks>
        /// **Sample request body:**
        ///
        ///     { 
        ///        "CustomerName" : "Mert Demirkıran",
        ///        "CustomerEmail" : "mertdmkrn37@gmail.com",
        ///        "Date" : "2023-11-11",
        ///        "Description": "Kolay gelsin.",
        ///        "GuestCount": 4
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("reservation/save")]
        public async Task<IActionResult> Save([FromBody]Reservation reservation)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            try
            {
                var reservationValidator = new ReservationValidator();
                var validationResult = reservationValidator.Validate(reservation);

                if (!validationResult.IsValid && validationResult.Errors.Any())
                {
                    response.HasError = true;
                    validationResult.Errors.ForEach(x => response.ValidationErrors.Add(new ValidationError(x.PropertyName, x.ErrorMessage)));
                    response.Message = "Rezervasyon yapılamadı.";
                    return BadRequest(response);
                }

                if (reservation.Date < DateTime.Today)
                {
                    response.HasError = true;
                    response.Message = "Rezervasyon yapılamadı.";
                    return BadRequest(response);
                }

                var optimalTable = await _tableService.GetOptimalTableAsync(reservation.GuestCount, reservation.Date);

                if(optimalTable == null)
                {
                    response.HasError = true;
                    response.Message = "Üzgünüz, uygun masa bulunamadı.";
                    return Ok(response);
                }

                reservation.TableId = optimalTable.Id;
                await _reservationService.SaveReservationAsync(reservation);

                var email = string.Format("Sayın {0}, rezervasyonunuz başarıyla alındı.<br>Masa No: {1}<br>Tarih: {2}<br>Kişi Sayısı: {3}", reservation.CustomerName, optimalTable.Number, reservation.Date.ToString("dd.MM.yyyy"), reservation.GuestCount);
                
                await _mailHandler.SendEmailAsync(new MailRequest()
                {
                    ToEmailList = new List<string> { reservation.CustomerEmail },
                    Subject = "Rezervasyon Onayı",
                    Body = email
                });

                response.Message = "Rezervasyon yapıldı.";
                response.Data = true;

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
