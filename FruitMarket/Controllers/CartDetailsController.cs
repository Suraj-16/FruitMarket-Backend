using FruitMarket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FruitMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailsController : ControllerBase
    {
        private AppDbContext _context;

        public CartDetailsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost()]
        [Route("addtocart")]
        public async Task<ActionResult> AddToCart(CartDetail cartItem)
        {
            try
            {
                var cartI = _context.cartdetails.FirstOrDefault(c => c.fruitid == cartItem.fruitid && c.userid == cartItem.userid);
                if (cartI == null)
                {
                    cartItem.isremoved = false;
                    await _context.cartdetails.AddAsync(cartItem);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(AddToCart), new { id = cartItem.cartid }, cartItem);
                }
                else
                {
                    cartI.qty = cartI.qty + cartItem.qty;
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(AddToCart), new { id = cartI.cartid }, cartI);
                }
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet()]
        [Route("displayitems/{userid}")]
        public ActionResult<IEnumerable<CartItem>> Get(int userid)
        {
            try
            {
                var list = _context.cartItems.FromSqlInterpolated($"displaycartitems {userid}");
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{cartid}")]
        public async Task<ActionResult> RemoveFromCart(int cartid)
        {
            try
            {
                var cartItem = await _context.cartdetails.FindAsync(cartid);
                _context.cartdetails.Remove(cartItem);
                await _context.SaveChangesAsync();
                return Ok();

            }catch (Exception Ex)
            {
                return StatusCode(500, Ex.Message);
            }
        }

        [Route("getitem/{cartid}")]
        [HttpGet()]
        public async Task<ActionResult> GetItem(int cartid)
        {
            try
            {
                var item = await _context.cartdetails.FindAsync(cartid);
                return Ok(item);
            }catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
