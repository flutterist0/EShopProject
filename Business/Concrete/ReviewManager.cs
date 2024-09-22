using Business.Abstract;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReviewManager(IReviewDal reviewDal) : IReviewService
	{
		private readonly IReviewDal _reviewDal = reviewDal;
		public IResult Add(ReviewAddDto reviewAddDto)
		{
			if (reviewAddDto.Rating < 1 || reviewAddDto.Rating > 5)
			{
				return new ErrorResult("Rating must be between 1 and 5.");
			}
			var review = new Review
			{
				Name = reviewAddDto.Name,	
				Email = reviewAddDto.Email,
				Comment = reviewAddDto.Comment,
				ReviewDate = reviewAddDto.ReviewDate,
				ProductId = reviewAddDto.ProductId,
				Rating = reviewAddDto.Rating,
			};
			_reviewDal.Add(review);
			return new SuccessResult("Review successfully added.");
		}

		public IDataResult<List<ReviewGetAllDto>> GetAllByProductId(int productId)
		{
			var reviews = _reviewDal.GetAll(r => r.ProductId == productId && r.IsDelete==false)
							.Select(r => new ReviewGetAllDto
							{
								Name = r.Name,
								Comment = r.Comment,
								ReviewDate = r.ReviewDate,
								Rating = r.Rating
							}).ToList();

			if (reviews.Count == 0)
			{
				return new ErrorDataResult<List<ReviewGetAllDto>>("No reviews found for this product.");
			}
			return new SuccessDataResult<List<ReviewGetAllDto>>(reviews);
		}
	}
}
