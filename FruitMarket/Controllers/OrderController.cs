using FruitMarket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FruitMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly IConfiguration Configuration;

        private List<OrderDetail> LoadListFromDB()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            SqlConnection con = new SqlConnection(Configuration.GetConnectionString("MyConStr"));

            SqlCommand cmd = new SqlCommand("select * from orderdetails", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for(int i=0; i<dt.Rows.Count; i++)
            {
                OrderDetail obj = new OrderDetail();
                obj.orderid = int.Parse(dt.Rows[i]["orderid"].ToString());
                obj.userid = int.Parse(dt.Rows[i]["userid"].ToString());
                obj.fruitid = int.Parse(dt.Rows[i]["fruitid"].ToString());
                obj.qty = int.Parse(dt.Rows[i]["qty"].ToString());
                obj.isremoved = bool.Parse(dt.Rows[i]["isremoved"].ToString());

                orderDetails.Add(obj);
            }
            
            return orderDetails;
        }

        [HttpGet]
        [Route("getOrders")]
        public ActionResult Get()
        {
            return Ok(LoadListFromDB());
        }

        [HttpGet]
        [Route("getOrders/{orderid}")]
        public ActionResult GetOrder(int orderid)
        {

            return Ok(LoadListFromDB().Where(e => e.orderid == orderid));
        }


        [HttpPost]
        public ActionResult Post(OrderDetail orderdetails)
        {
            try
            {
                SqlConnection con = new SqlConnection(Configuration.GetConnectionString("MyConStr"));
                SqlCommand cmd = new SqlCommand("INSERT INTO orderdetails VALUES ("+ orderdetails.userid +" , " + orderdetails.fruitid +", "+orderdetails.qty+", '"+orderdetails.isremoved+"')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("{userid}")]
        public ActionResult<IEnumerable<OrderItem>> Get(int userid)
        {
            try
            {
                List<OrderItem> orderDetails = new List<OrderItem>();

                SqlConnection con = new SqlConnection(Configuration.GetConnectionString("MyConStr"));
                SqlCommand cmd = new SqlCommand("exec displayordereditems "+userid+"", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    OrderItem obj = new OrderItem();
                    obj.orderid = int.Parse(dt.Rows[i]["orderid"].ToString());
                    obj.fruitname = dt.Rows[i]["fruitname"].ToString();
                    obj.fruitimg = dt.Rows[i]["fruitimg"].ToString();
                    obj.qty = int.Parse(dt.Rows[i]["qty"].ToString());
                    obj.fruitdes = dt.Rows[i]["fruitdes"].ToString();
                    obj.fruitprice= decimal.Parse(dt.Rows[i]["fruitprice"].ToString());

                    orderDetails.Add(obj);
                }

                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{orderid}")]
        public ActionResult Delete(int orderid)
        {
            try
            {
                SqlConnection con = new SqlConnection(Configuration.GetConnectionString("MyConStr"));
                SqlCommand cmd = new SqlCommand("delete from orderdetails where orderid = "+ orderid +"", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //private AppDbContext _context;

        //public OrderController(AppDbContext context)
        //{
        //    _context = context;
        //}

        //[HttpPost]
        //public async Task<ActionResult> Post(OrderDetail orderdetails)
        //{
        //    try
        //    {
        //        await _context.orderdetails.AddAsync(orderdetails);
        //        await _context.SaveChangesAsync();
        //        return Ok();
        //    }catch(Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("{userid}")]
        //public ActionResult<IEnumerable<OrderItem>> Get(int userid)
        //{
        //    try
        //    {
        //        var list = _context.orderItems.FromSqlInterpolated($"displayordereditems {userid}");
        //        return Ok(list);
        //    }catch(Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpDelete]
        //[Route("{orderid}")]
        //public ActionResult Delete(int orderid)
        //{
        //    try
        //    {
        //        Console.WriteLine(orderid);
        //        var item = _context.orderdetails.Find(orderid);
        //        item.isremoved = true;
        //        _context.SaveChanges();
        //        return Ok(item);
        //    }catch(Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
