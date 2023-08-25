using FormulaAirLine.API.Models;
using FormulaAirLine.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirLine.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly IMessageProducer _messageProducer;

        public static readonly List<Booking> _bookings = new();

        public BookingsController(
            ILogger<BookingsController> logger,
            IMessageProducer messageProducer
            )
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CreatingBooking(Booking newBooking)
        {
            if(!ModelState.IsValid) return BadRequest();

            _bookings.Add(newBooking);
            _messageProducer.SendingMessage(newBooking);

            return Ok();
        }
    }
}
