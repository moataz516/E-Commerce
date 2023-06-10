using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Website.Models;
using E_Website.Models.Data;
using E_Website.Models.ViewModel;

namespace E_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ordersController : ControllerBase
    {
        private readonly ModelContext _context;

        public ordersController(ModelContext context)
        {
            _context = context;
        }

        //[HttpPost("card")]
        //public async Task<IActionResult> CheckPayment([FromBody] userCardVM model)
        //{
        //    var userCard = await _context.userCards.Where(x => x.userId == model.userId).FirstOrDefaultAsync();
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Error");
        //    }
        //    if (userCard.userId != model.userId)
        //    {
        //        return BadRequest("userCard does not found");
        //    }

        //    if (userCard.cvv == model.cvv && userCard.cardName == model.cardName)
        //    {
        //        return Ok(userCard);
        //    }

        //    return BadRequest("Error occurs");
        //}

        [HttpPost("card/{id}")]
        public async Task<IActionResult> CheckPayment(string id ,  userCardVM model)
        {
            var userCard = await _context.userCards.Where(x=>x.userId == id).FirstOrDefaultAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest("Error Occccc");
            }
            if ( id != model.userId || userCard == null)
            {
                return BadRequest("userCard does not found");
            }

            if (userCard.cvv == model.cvv && userCard.cardName == model.cardName)
            {
                return Ok(userCard);
            }

            return BadRequest("Invalid Info!");


        }

        [HttpGet("orderuser/{id}")]
        public IActionResult getUsderOrders(string id)
        {
            var query = from userOrder in _context.orderDetails
                        join o in _context.orders
                        on userOrder.orderId equals o.orderId where o.userId == id
                        select new { userOrder ,o};

            return Ok(query);
        }

        [HttpPost("addOrder")]
        public async Task<IActionResult> AddOrder([FromBody] orderVM model)
        {
            order order = new order()
            {
                orderId = Guid.NewGuid().ToString(),
                userId = model.userId,
                userName = model.userName,
                dateTime = DateTime.Now,
                state = "undelevered",
                total = model.total,
            };
            _context.orders.Add(order);
            await _context.SaveChangesAsync();

            foreach(var cart in model.cart)
            {
                orderDetails details = new orderDetails()
                {
                    orderDetailsId = Guid.NewGuid().ToString(),
                    orderId = order.orderId,
                    productId = cart.productId,
                    name = cart.name,
                    price = cart.price,
                    quantity = cart.qty,
                    total = cart.price * (decimal)cart.qty
                };
                _context.orderDetails.Add(details);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("addOrderDetails")]
        public async Task<IActionResult> AddOrderDetails([FromBody] List<orderDetailsVM> model)
        {
            if(model.Count > 0)
            {
                foreach (var item in model)
                {
                    orderDetails order = new orderDetails()
                    {
                        orderDetailsId = Guid.NewGuid().ToString(),
                        orderId = item.orderId,
                        productId = item.productId,
                        name = item.name,
                        price = item.price,
                        quantity = item.quantity,
                        total = item.total,
                    };
                _context.orderDetails.Add(order);
                }
            }


            await _context.SaveChangesAsync();
            return Ok("Succeesful");
        }


        [HttpPost("addcard")]
        public async Task<IActionResult> AddCard([FromBody] userCardVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something Wrong");
            }
            var userCard = await _context.userCards.Where(x => x.userId == model.userId).FirstOrDefaultAsync();
            if (userCard != null && userCard.userId == model.userId)
            {
                return BadRequest("userCard Found");
            }
            userCard card = new userCard();
            card.userCardId = Guid.NewGuid().ToString();
            card.userId = model.userId;
            card.cardName = model.cardName;
            card.cardNumber = model.cardNumber;
            card.expiryYear = model.expiryYear;
            card.expiryMonth = model.expiryMonth;
            card.cvv = model.cvv;

            _context.userCards.Add(card);
            await _context.SaveChangesAsync();

            return Ok("Add Card Successfully");


        }

        [HttpGet("orders/{id}")]
        public async Task<IActionResult> GetOrders(string id)
        {
            var orders = await _context.orders.Where(x => x.userId == id).ToListAsync();
            return Ok(orders);
        }


        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<order>>> Getorders()
        {
            return await _context.orders.ToListAsync();
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<order>> Getorder(string id)
        {
            var order = await _context.orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putorder(string id, order order)
        {
            if (id != order.orderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!orderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<order>> Postorder(order order)
        {
            _context.orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (orderExists(order.orderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getorder", new { id = order.orderId }, order);
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteorder(string id)
        {
            var order = await _context.orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool orderExists(string id)
        {
            return _context.orders.Any(e => e.orderId == id);
        }
    }
}
