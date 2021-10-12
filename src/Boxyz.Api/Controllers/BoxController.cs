using Boxyz.Proto.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boxyz.Proto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxController : ControllerBase
    {
        private readonly IBoxViewService _boxService;

        public BoxController(IBoxViewService boxService)
        {
            _boxService = boxService;
        }

        [HttpGet("[action]")]
        public async Task<BoxObjectModel> GetObject(long id, string culture)
        {
            return await _boxService.GetBoxObject(id, culture);
        }
    }
}
