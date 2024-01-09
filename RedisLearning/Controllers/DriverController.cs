using Microsoft.AspNetCore.Mvc;
using Services.Caching;
using EFCore;
using Microsoft.EntityFrameworkCore;
using EFCore.DbModels;

namespace RedisLearning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {        
        private readonly ILogger<DriversController> _logger;
        private readonly ICacheService _cacheService;
        private readonly DataContext _context;
        public DriversController(ILogger<DriversController> logger, ICacheService cacheService, DataContext context)
        {
            _logger = logger;
            _cacheService = cacheService;
            _context = context;
        }
        [HttpGet("drivers")]
        public async Task<IActionResult> Get()
        {
            var cacheData = _cacheService.GetData<IEnumerable<Driver>>("drivers");

            if (cacheData != null && cacheData.Count() > 0)
                return Ok(cacheData);

            cacheData = await _context.Drivers.Take(10000).Skip(0).ToListAsync();

            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<IEnumerable<Driver>>("drivers", cacheData, expiryTime);
            return Ok(cacheData);
        }

        [HttpGet("DriversId")]
        public async Task<IActionResult> GetById(int id)
        {
            var exist = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
            if (exist is not null) 
                return Ok(exist);
            else
                return NotFound();
        }

        [HttpGet("CacheDrivers")]
        public async Task<IActionResult> GetByIdCache(int id)
        {
            var cacheData = _cacheService.GetData<IEnumerable<Driver>>($"drivers{id}");
            if (cacheData != null && cacheData.Count() > 0)
                return Ok(cacheData);
            else
                return NotFound();
        }

        [HttpPost("AddDriver")]
        public async Task<IActionResult> Post(Driver value)
        {
            var addedObj = await _context.Drivers.AddAsync(value);
            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<Driver>($"driver{value.Id}", addedObj.Entity, expiryTime);

            await _context.SaveChangesAsync();

            return Ok(addedObj.Entity);
        }

        [HttpDelete("DeleteDriver")]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);

            if(exist is not null)
            {
                _context.Remove(exist);
                _cacheService.RemoveData($"driver{id}");
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return NotFound();
        }
    }
}