﻿using Core.Helpers.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ISocialNetworkService
	{
		IResult Add(SocialNetwork socialNetwork);
		IResult Update(SocialNetwork socialNetwork, int id);
		IResult Delete(int id);
		IDataResult<List<SocialNetwork>> GetAll();
		IDataResult<SocialNetwork> Get(int id);
	}
}
