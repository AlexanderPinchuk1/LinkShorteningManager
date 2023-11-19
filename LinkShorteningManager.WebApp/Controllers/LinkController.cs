using AutoMapper;
using LinkShorteningManager.Foundation.Interfaces;
using LinkShorteningManager.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkShorteningManager.Controllers
{
    public class LinkController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILinkService _linkService;
        private readonly IRandomGenerator _randomGenerator;



        public LinkController(
            IMapper mapper,
            ILinkService linkService,
            IRandomGenerator randomGenerator)
        {
            _linkService = linkService;
            _mapper = mapper;
            _randomGenerator = randomGenerator;
        }



        [HttpGet]
        public async Task<IActionResult> Index(string key)
        {
            if (key == "Default")
            {
                return View(_mapper
                    .Map<IEnumerable<LinkViewModel>>(await _linkService.AllAsync()));
            }

            var url = await _linkService.GetLongUrlByKey(key) ;
            if (url != null)
            {
                await _linkService.IncreaseClickCount(key);

                return Redirect(url);
            }
            
            return BadRequest();
        }

        [HttpGet]
        public IActionResult Addition()
        {
            return View(new LinkViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var link = await _linkService.GetByIdOrReturnNullAsync(id);
            if (link == null)
            {
                return NotFound();
            }

            return View("Addition", _mapper.Map<LinkViewModel>(link));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]LinkViewModel linkViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _linkService.AddAsync(_mapper.Map<Link>(linkViewModel));

            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LinkViewModel linkViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _linkService.UpdateAsync(_mapper.Map<Link>(linkViewModel));

            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await _linkService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetShortKey()
        {
            return Json(_randomGenerator.RandomString(6));
        }
    }
}
