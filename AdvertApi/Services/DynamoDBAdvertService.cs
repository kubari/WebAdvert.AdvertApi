using AdvertApi.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertApi.Services
{
	public class DynamoDBAdvertService : IAdvertStorageService
	{
		private readonly IMapper _mapper;

		public DynamoDBAdvertService(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<string> Add(AdvertModel model)
		{
			var dbModel = _mapper.Map<AdvertDBModel>(model);
			using (var client = new AmazonDynamoDBClient())
			{
				dbModel.Id = new Guid().ToString();
				dbModel.CreationDateTime = DateTime.UtcNow;
				dbModel.Status = AdvertStatus.Pending;
				using (var context = new DynamoDBContext(client))
				{
					await context.SaveAsync(dbModel);

				}
			}

			return dbModel.Id;
		}

		public async Task Confirm(ConfirmAdvertModel model)
		{
			using (var client = new AmazonDynamoDBClient())
			{
				using (var context = new DynamoDBContext(client))
				{
					var advertDbModel = await context.LoadAsync<AdvertDBModel>(model.Id);
					if(advertDbModel == null)
					{
						throw new KeyNotFoundException($"A recored with ID={model.Id} is not found");
					}

					if (model.Status == AdvertStatus.Active)
					{
						advertDbModel.Status = AdvertStatus.Active;
						await context.SaveAsync(advertDbModel);
					}
					else
					{
						await context.DeleteAsync(advertDbModel);
					}
				
				}

				dbModel.Id = new Guid().ToString();
				dbModel.CreationDateTime = DateTime.UtcNow;
				dbModel.Status = AdvertStatus.Pending;
				using (var context = new DynamoDBContext(client))
				{
					await context.SaveAsync(dbModel);

				}
			}
		}
	}
}
