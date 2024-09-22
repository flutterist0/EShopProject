using Business.Abstract;
using Core.Helpers.Business;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class SocialNetworkManager(ISocialNetworkDal socialNetworkDal, IAddPhotoHelperService addPhotoHelperService) :ISocialNetworkService
	{
		private readonly ISocialNetworkDal _socialNetworkDal = socialNetworkDal;
		private readonly IAddPhotoHelperService _addPhotoHelperService = addPhotoHelperService;
		public IResult Add(SocialNetworkAddDto socialNetworkDto)
		{

			var guid = Guid.NewGuid() + "-" + socialNetworkDto.Image.FileName;
			_addPhotoHelperService.AddImage(socialNetworkDto.Image, guid);
			SocialNetwork socialNetwork = new()
			{
				Name = socialNetworkDto.Name,
				ImageUrl = "/images/" + guid,
			};
			_socialNetworkDal.Add(socialNetwork);
			return new SuccessResult("Elave olundu");
		}

		public IResult Delete(int id)
		{
			SocialNetwork deleteSocialNetwork = null;
			SocialNetwork result = _socialNetworkDal.Get(a => a.Id == id && a.IsDelete == false);
			if (result != null)
			{
				deleteSocialNetwork = result;

				_socialNetworkDal.Delete(deleteSocialNetwork);
				return new SuccessResult("deleted");
			}
			else

				return new ErrorResult("silinmedi");
		}

		public IDataResult<SocialNetwork> Get(int id)
		{
			var result = _socialNetworkDal.Get(t => t.Id == id && t.IsDelete == false);
			if (result != null)
				return new SuccessDataResult<SocialNetwork>(result, "loaded");
			else return new ErrorDataResult<SocialNetwork>(result, "tapilmadi");
		}

		public IDataResult<List<SocialNetwork>> GetAll()
		{
			var result = _socialNetworkDal.GetAll(s => s.IsDelete == false).ToList();
			if (result.Count > 0)
				return new SuccessDataResult<List<SocialNetwork>>(result);
			else return new ErrorDataResult<List<SocialNetwork>>("xeta baş verdi");
		}

		public IResult Update(SocialNetwork socialNetwork, int id)
		{
			var updateSocialNetwork = _socialNetworkDal.Get(a => a.Id == id && a.IsDelete == false);
			updateSocialNetwork.Name = socialNetwork.Name;
			updateSocialNetwork.ImageUrl = socialNetwork.ImageUrl;
			_socialNetworkDal.Update(socialNetwork);
			return new SuccessResult();
		}
	}
}
