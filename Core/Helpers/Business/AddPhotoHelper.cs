

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
