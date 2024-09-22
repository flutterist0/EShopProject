using Business.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(IBrandService brandService) : ControllerBase
    {
        private readonly IBrandService _brandService = brandService;

        [HttpPost]
        public IActionResult Add(BrandAddDto brand) 
        {
            var result = _brandService.Add(brand);
            if (result.Success)
            {
                return Ok(result);
            }else
                return BadRequest(result);
        }

        [HttpGet("getAllBrands")]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

        [HttpGet("get{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            var result = _brandService.Get(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

        [HttpPut]
        public IActionResult Update(Brand brand, int id)
        {
            var result = _brandService.Update(brand, id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}

        [HttpDelete("delete:{id:int:min(1)}")]
        public IActionResult Delete(int id)
        {
            var result = _brandService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			else
				return BadRequest(result);
		}
    }
}
