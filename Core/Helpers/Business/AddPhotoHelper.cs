﻿//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Core.Helpers.Business
//{
//	public class AddPhotoHelper(IWebHostEnvironment webHostEnvironment) : IAddPhotoHelperService
//	{
//		private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
//		// NuGet\Install-Package Microsoft.AspNetCore.Hosting.WindowsServices -Version 8.0.8 packageManager console-da yuklenmelidir
//		public void AddImage(IFormFile formFile, string guid)
//		{
//			var fileName = guid;
//			var wwwFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
//			var imageFolder = Path.Combine(wwwFolder, fileName);
//			using var fileStream = new FileStream(imageFolder, FileMode.Create);
//			formFile.CopyTo(fileStream);
//		}
//	}
//}

using Core.Helpers.Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public class AddPhotoHelper : IAddPhotoHelperService
{
	private readonly IWebHostEnvironment _webHostEnvironment;

	public AddPhotoHelper(IWebHostEnvironment webHostEnvironment)
	{
		_webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
	}

	public void AddImage(IFormFile formFile, string guid)
	{
		if (formFile == null)
		{
			throw new ArgumentNullException(nameof(formFile), "Form file cannot be null.");
		}

		if (string.IsNullOrEmpty(_webHostEnvironment.WebRootPath))
		{
			throw new InvalidOperationException("WebRootPath is not configured.");
		}

		var fileName = guid;
		var wwwFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

		if (!Directory.Exists(wwwFolder))
		{
			Directory.CreateDirectory(wwwFolder);
		}

		var imageFolder = Path.Combine(wwwFolder, fileName);
		using var fileStream = new FileStream(imageFolder, FileMode.Create);
		formFile.CopyTo(fileStream);
	}
}
