using AdvertApi.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertApi.Services
{
	public class AdvertPropfile : Profile
	{
		public AdvertPropfile()
		{
			CreateMap<AdvertModel, AdvertDBModel>();
		}
	}
}
