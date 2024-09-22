using Business.Abstract;
using Core.Entities.Concrete;
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
	public class ServiceManager(IServiceDal serviceDal, IAddPhotoHelperService addPhotoHelperService) : IServiceService
	{
		private readonly IServiceDal _serviceDal = serviceDal;
		private readonly IAddPhotoHelperService _addPhotoHelperService = addPhotoHelperService;
		public IResult Add(ServiceAddDto serviceDto)
		{

			var guid = Guid.NewGuid() + "-" + serviceDto.Image.FileName;
			_addPhotoHelperService.AddImage(serviceDto.Image, guid);
			Service service = new()
			{
				Title = serviceDto.Title,
				Description = serviceDto.Description,
				ImageUrl = "/images/" + guid,
			};
			_serviceDal.Add(service);
			return new SuccessResult("Elave olundu");
		}

		public IResult Delete(int id)
		{
			Service deleteService = null;
			Service result = _serviceDal.Get(a => a.Id == id&&a.IsDelete==false);
			if (result != null)
			{
				deleteService = result;

				_serviceDal.Delete(deleteService);
				return new SuccessResult("deleted");
			}
			else

				return new ErrorResult("silinmedi");
		}

		public IDataResult<Service> Get(int id)
		{
			var result = _serviceDal.Get(t => t.Id == id&&t.IsDelete==false);
			if (result != null)
				return new SuccessDataResult<Service>(result, "loaded");
			else return new ErrorDataResult<Service>(result, "tapilmadi");
		}

		public IDataResult<List<Service>> GetAll()
		{
			var result = _serviceDal.GetAll(s=> s.IsDelete==false).ToList();
			if (result.Count > 0)
				return new SuccessDataResult<List<Service>>(result);
			else return new ErrorDataResult<List<Service>>("xeta baş verdi");
		}

		public IResult Update(Service service, int id)
		{
			var updateService = _serviceDal.Get(a => a.Id == id&&a.IsDelete==false);
			updateService.Title= service.Title;
			updateService.Description= service.Description;
			updateService.ImageUrl= service.ImageUrl;

			_serviceDal.Update(service);
			return new SuccessResult();
		}
	}
}
