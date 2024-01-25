using Microsoft.EntityFrameworkCore;
using NeveraPortal.BLL.Services.Interfaces;
using NeveraPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeveraPortal.BLL.Services.Repositories
{
	public class CityRepository : GenericRepository<City>, ICityRepository
	{

		public void Update(City city)
		{
			var existingCity = GetById(city.Id);

			if (existingCity != null)
			{
				existingCity.Name = city.Name;
				existingCity.CountryId = city.CountryId;


				context.Entry(existingCity).State = EntityState.Modified;
				context.SaveChanges();
			}
		}

		
	}
}
