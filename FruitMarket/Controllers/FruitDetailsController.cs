using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitMarket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FruitMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitDetailsController : ControllerBase
    {
        public FruitDetailsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                SqlConnection con = new SqlConnection(Configuration.GetConnectionString("myConStr"));
                string query = "select * from fruitdetails";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<FruitDetail> fruitList = new List<FruitDetail>();

                for (int i=0; i<dt.Rows.Count; i++)
                {
                    FruitDetail obj = new FruitDetail();
                    obj.fruitid = int.Parse(dt.Rows[i]["fruitid"].ToString());
                    obj.fruitname = dt.Rows[i]["fruitname"].ToString();
                    obj.fruitimg = dt.Rows[i]["fruitimg"].ToString();
                    obj.fruitprice = decimal.Parse(dt.Rows[i]["fruitprice"].ToString());
                    obj.fruitdes = dt.Rows[i]["fruitdes"].ToString();

                    fruitList.Add(obj);
                }

                return Ok(fruitList);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AddFruit(FruitDetail fruitdetails)
        {
            try
            {
                SqlConnection con = new SqlConnection(Configuration.GetConnectionString("MyConStr"));
                string query = "insert into fruitdetails values ('" + fruitdetails.fruitname + "','" + fruitdetails.fruitimg + "'," + fruitdetails.fruitprice + ",'" + fruitdetails.fruitdes + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //return Ok();
                return CreatedAtAction(nameof(AddFruit), new { id = fruitdetails.fruitid} , fruitdetails);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //private AppDbContext _context;

        //public FruitDetailsController(AppDbContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<FruitDetail>>> GetFruits()
        //{
        //    try
        //    {
        //        var fruitList = await _context.fruitdetails.ToListAsync();
        //        return Ok(fruitList);
        //    }catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> addFruit(FruitDetail fruitdetail)
        //{
        //    try
        //    {
        //        await _context.fruitdetails.AddAsync(fruitdetail);
        //        await _context.SaveChangesAsync();
        //        return StatusCode(201, "Fruit added Successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
