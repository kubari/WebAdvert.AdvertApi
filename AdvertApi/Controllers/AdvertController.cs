using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertApi.Controllers
{
	[ApiController]
	[Route("adverts/v1")]
    public class AdvertController: ControllerBase
    {
		private readonly IAdvertStorageService _advertStorageService;

		public AdvertController(IAdvertStorageService advertStorageService)
		{
			_advertStorageService = advertStorageService;
		}

		[HttpPost]
		[Route("Create")]
		[ProducesResponseType(400)]
		[ProducesResponseType(201, Type=typeof(CreateAdvertResponse))]
		public async Task<IActionResult> Create(AdvertModel model)
		{
			string id;
			try
			{
				id = await _advertStorageService.Add(model);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
			 	return StatusCode(500, ex.Message);
			}

			return StatusCode(201, new CreateAdvertResponse { Id = id });
		}

		[HttpPut]
		[Route("confirm")]
		[ProducesResponseType(404)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
		{
			try
			{
				await _advertStorageService.Confirm(model);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}

			return Ok();
		}
    }
}
