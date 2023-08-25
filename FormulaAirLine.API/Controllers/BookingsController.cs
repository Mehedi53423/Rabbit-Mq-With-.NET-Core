using FormulaAirLine.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirLine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly IMessageProducer _messageProducer;

        public BookingsController(
            ILogger<BookingsController> logger,
            IMessageProducer messageProducer
            )
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }
    }
}
