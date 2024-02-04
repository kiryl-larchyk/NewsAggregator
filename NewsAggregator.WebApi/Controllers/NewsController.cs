using Microsoft.AspNetCore.Mvc;
using NewsAggregator.BusinessLogic.Enums;
using NewsAggregator.BusinessLogic.Interfaces;

namespace NewsAggregator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public IActionResult GetNews()
        {
            return new JsonResult(_newsService.GetNews());
        }

        [HttpGet("title/{substring}")]
        public IActionResult GetNewsByTitle(string substring)
        {
            return new JsonResult(_newsService.GetNewsBySubstring(substring, NewsFilterType.Title));
        }

        [HttpGet("description/{substring}")]
        public IActionResult GetNewsByDescription(string substring)
        {
            return new JsonResult(_newsService.GetNewsBySubstring(substring, NewsFilterType.Description));
        }

        [HttpGet("titleAndDescription/{substring}")]
        public IActionResult GetNewsByTitleAndDescription(string substring)
        {
            return new JsonResult(_newsService.GetNewsBySubstring(substring, NewsFilterType.TitleAndDescription));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewsByRssKey(string rssKey)
        {
            await _newsService.AddNewsByRssKey(rssKey);
            return Created();
        }
    }
}
