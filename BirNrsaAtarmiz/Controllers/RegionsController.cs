using BirNrsaAtarmiz.Data;
using BirNrsaAtarmiz.Models.Domain;
using BirNrsaAtarmiz.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirNrsaAtarmiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly BirNarsaAtarmizDbContext _context;

        public RegionsController(BirNarsaAtarmizDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain = _context.Regions.ToList();

            var regionsDto = new List<RegionDto>();
            foreach(var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    FullName = regionDomain.FullName,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });   
            }
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var regionDomain = _context.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                FullName = regionDomain.FullName,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRequestRegionDto addRequestRegionDto) 
        {
            var regionsDominModel = new Region
            {
                Code = addRequestRegionDto.Code,
                FullName = addRequestRegionDto.FullName,
                RegionImageUrl = addRequestRegionDto.RegionImageUrl
            };

            _context.Regions.Add(regionsDominModel);
            _context.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionsDominModel.Id,
                Code = regionsDominModel.Code,
                FullName = regionsDominModel.FullName,
                RegionImageUrl = regionsDominModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }
    }
}
