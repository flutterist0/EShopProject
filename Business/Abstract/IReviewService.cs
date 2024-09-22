using Core.Helpers.Results.Abstract;
using Entities.Dto.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReviewService
	{
		IResult Add(ReviewAddDto reviewAddDto);
		IDataResult<List<ReviewGetAllDto>> GetAllByProductId(int productId);
	}
}
