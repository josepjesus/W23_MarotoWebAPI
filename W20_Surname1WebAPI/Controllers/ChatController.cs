using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using W20_Surname1WebAPI.Models;

namespace W20_Surname1WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Chat")]
    public class ChatController : ApiController
    {
        // POST api/Chat/PostNewUser
        [HttpPost]
        [Route("PostNewUser")]
        public IHttpActionResult PostNewUser(ChatModel chatModel)
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {

                string sql = "INSERT INTO dbo.Chat(Id) " +
                    $"VALUES('{chatModel.Id}')";
                try
                {
                    cnn.Execute(sql);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error inserting the user in chat table: " + ex.Message);
                }

                return Ok();
            }
        }

        // POST api/Chat/UserIsLogued
        [HttpPost]
        [Route("UserIsLogued")]
        public IHttpActionResult UserIsLogued(ChatModel chatModel)
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {

                string sql = "UPDATE dbo.Chat " +
                    "SET LastMesage = '', IsLogued = 1 " + 
                    $"WHERE Id = '{chatModel.Id}'";
                try
                {
                    cnn.Execute(sql);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error: " + ex.Message);
                }

                return Ok();
            }
        }

        // POST api/Chat/PostNewMesage
        [HttpPost]
        [Route("PostNewMesage")]
        public IHttpActionResult PostNewMesage(ChatModel chatModel)
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                
                string sql = "UPDATE dbo.Chat " +
                    $"SET LastMesage = '{chatModel.LastMesage}' " +
                    $"WHERE Id = '{chatModel.Id}'";
                try
                {
                    cnn.Execute(sql);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error: " + ex.Message);
                }

                return Ok();
            }
        }

        //GET api/Chat/GetMesagesIfLogued
        [HttpGet]
        [Route("GetMesagesIfLogued")]
        public List<ChatModel> GetMesagesIfLogued()
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT Id, LastMesage FROM dbo.Chat WHERE IsLogued = 1";
                List<ChatModel> list = cnn.Query<ChatModel>(sql).ToList();
                return list;
            }
        }
    }
}
