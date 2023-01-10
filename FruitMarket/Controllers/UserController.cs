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
    public class UserController : ControllerBase
    {
        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpPost]
        public ActionResult Post(userDetail userdetails)
        {
            try
            {
                SqlConnection con = new SqlConnection(Configuration.GetConnectionString("MyConStr"));
                string query = "insert into userdetails values ('" + userdetails.firstname + "','" + userdetails.lastname + "','" + userdetails.email + "','" + userdetails.password + "','" 
                    + userdetails.address1 + "','" + userdetails.city + "','" + userdetails.district + "','" + userdetails.statename + "'," + userdetails.pincode + ")";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return CreatedAtAction(nameof(Post), new { id = userdetails.userid}, userdetails);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("login")]
        public ActionResult GetUser(LoginDetail logindetails)
        {
            try
            {
                //List<userDetail> userDetails = new List<userDetail>();
                SqlConnection con = new SqlConnection(Configuration.GetConnectionString("MyConStr"));
                string query = "select * from userdetails where email = '" + logindetails.email + "' and password = '" + logindetails.password + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                
                
                    userDetail obj = new userDetail();
                    obj.userid = int.Parse(dt.Rows[0]["userid"].ToString());
                    obj.firstname = dt.Rows[0]["firstname"].ToString();
                    obj.lastname = dt.Rows[0]["lastname"].ToString();
                    obj.email = dt.Rows[0]["email"].ToString();
                    obj.password = dt.Rows[0]["password"].ToString();
                    obj.address1 = dt.Rows[0]["address1"].ToString();
                    obj.city = dt.Rows[0]["city"].ToString();
                    obj.district = dt.Rows[0]["district"].ToString();
                    obj.statename = dt.Rows[0]["statename"].ToString();
                    obj.pincode = int.Parse(dt.Rows[0]["pincode"].ToString());
                    //userDetails.Add(obj);
                


                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //    private AppDbContext _context;

        //    public UserController(AppDbContext context)
        //    {
        //        _context = context;
        //    }
        //    [HttpPost]
        //    public async Task<ActionResult> addUser(userDetail userdetails)
        //    {
        //        try
        //        {
        //            await _context.userdetails.AddAsync(userdetails);
        //            await _context.SaveChangesAsync();
        //            return Ok();
        //        }catch (Exception ex)
        //        {
        //            return StatusCode(500, ex.Message);
        //        }
        //    }

        //    [HttpPost("login")]
        //    [Route("api/user/login")]
        //    public async Task<ActionResult> login(LoginDetail logindetails)
        //    { 
        //        var user  = await _context.userdetails.FirstOrDefaultAsync(u => (u.email == logindetails.email && u.password == logindetails.password));
        //        return Ok(user);
        //    }
    }
}
