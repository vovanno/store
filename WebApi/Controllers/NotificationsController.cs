using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.VIewDto;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/notifications")]
    public class NotificationsController : ControllerBase
    {
        private readonly IDynamoDBContext _context;

        public NotificationsController(IDynamoDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task CreateNotification(Notifications request)
        {
            try
            {
                await _context.SaveAsync(request);
            }
            catch (Exception e)
            {   
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult<Notifications>> GetItems(string notificationType)
        {
            return await _context.LoadAsync<Notifications>(notificationType);
        }
    }
}
