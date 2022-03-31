using Microsoft.AspNetCore.Mvc;
using Order.Dtos;
using Order.Models;

namespace Order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderEntity>>> Get()
        {
            return Array.Empty<OrderEntity>();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderEntity>>> GetOrder(int id)
        {
            return Array.Empty<OrderEntity>();
        }
        [HttpPost]
        public async Task<ActionResult<OrderEntity>> NewOrder(OrderEntity order)
        {
            throw new NotImplementedException();
        }
        [HttpGet("{idOrder}/actions")]
        public async Task<ActionResult<IEnumerable<OrderCmdDto>>> GetOrderActions(int id, OrderEntity order)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{idOrder}/actions")]
        public async Task<ActionResult<OrderCmdDto>> CreateOrderAction(int idOrder, OrderCmdDto cmd)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{idOrder}/actions/{idAction}")]
        public async Task<ActionResult<OrderCmdDto>> GetOrderActions(int idOrder, int idAction)
        {
            throw new NotImplementedException();
        }
    }
}