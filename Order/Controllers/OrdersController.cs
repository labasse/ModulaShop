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
        private OrderDbContext _context;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderEntity>> Get()
        {
            return _context.Orders;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderEntity>> GetOrder(int id)
        {
            var order = await _context.GetOrderByIdAsync(id);

            return order==null ? NotFound() : Ok(order);
        }
        [HttpPost]
        public async Task<ActionResult<OrderEntity>> NewOrder(OrderEntity order)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{idOrder}/actions")]
        public async Task<ActionResult<IEnumerable<OrderCmdDto>>> GetOrderActions(int idOrder)
        {
            var order = await _context.GetOrderByIdAsync(idOrder);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order.Actions.Select(action => OrderCmdDto.FromOrderCmd(action)));
        }

        [HttpPost("{idOrder}/actions")]
        public async Task<ActionResult<OrderCmdDto>> CreateOrderAction(int idOrder, OrderCmdDto cmd)
        {
            var order = await _context.GetOrderByIdAsync(idOrder);

            if(order == null)
            {
                return NotFound();
            }
            try
            {
                var orderCmd = cmd.ToOrderCmd();
                int index = order.Actions.Count();

                order.AddAction(orderCmd);
                return Created($"api/orders/{idOrder}/actions/{index}", orderCmd);
            }
            catch(InvalidOperationException e)
            {
                return UnprocessableEntity(e.Message);
            }
        }

        [HttpGet("{idOrder}/actions/{idAction}")]
        public async Task<ActionResult<OrderCmdDto>> GetOrderActions(int idOrder, int idAction)
        {
            var order = await _context.GetOrderByIdAsync(idOrder);

            if (order == null)
            {
                return NotFound("Bad order Id");
            }
            if(idAction >= order.Actions.Count())
            {
                return NotFound("Bad action Id");
            }
            return Ok(OrderCmdDto.FromOrderCmd(order.Actions.ElementAt(idAction)));
        }
    }
}