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
        public async Task<ActionResult<IEnumerable<OrderCmdDto>>> GetOrderActions(int idOrder)
        {
            OrderEntity order = context.Order.Find(idOrder);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order.Actions.Select(action => OrderCmdDto.FromOrderCmd(action)));
        }

        [HttpPost("{idOrder}/actions")]
        public async Task<ActionResult<OrderCmdDto>> CreateOrderAction(int idOrder, OrderCmdDto cmd)
        {
            OrderEntity order = context.Order.Find(idOrder);

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
            OrderEntity order = context.Order.Find(idOrder);

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