using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.DTO.Category;
using DomesticWarehousemanWebApi.DTO.Provider;
using DomesticWarehousemanWebApi.DTO.Resource;
using DomesticWarehousemanWebApi.Exceptions;
using DomesticWarehousemanWebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Services
{
	public class ResourcesService : IResourcesService
	{
		private readonly DomesticWarehousemanDbContext _dbContext;

		public ResourcesService(DomesticWarehousemanDbContext dbContext): base()
		{
			_dbContext = dbContext;
		}

		public Task<int> CreateResource(ResourceCreateDto dto, int storageId)
		{
			throw new NotImplementedException();
		}

		public ResourceDetailsDto GetResourceDetails(int resourceId)
		{
			var resource = _dbContext.Resources
				.FirstOrDefault(resource => resource.Id == resourceId);

			if (resource == null)
			{
				throw new NotFoundException("Resource not found");
			}

			var category = resource.IdCategoryNavigation;

			var provider = resource.IdProviderNavigation;

			var categoryDto = category != null ?
				new CategoryDetailsDto()
				{
					Id = category.Id,
					Name = category.Name
				} : 
				null;

			var providerDto = provider != null ?
				new ProviderDetailsDto()
				{
					Id = provider.Id,
					Name = provider.Name
				} :
				null;

			return new ResourceDetailsDto()
			{
				Id = resource.Id,
				Description = resource.Description,
				Category = categoryDto,
				CreationDate = resource.CreatedOn,
				Image = resource.Image,
				Name = resource.Name,
				Provider = providerDto
			};
		}

		public IEnumerable<ResourceIndexDto> IndexResources()
		{
			var result = _dbContext.Resources
				.Select
				(
					resource => new ResourceIndexDto()
					{
						Id = resource.Id,
						Name = resource.Name,
						CategoryName = resource.IdCategory != null ? resource.IdCategoryNavigation.Name : null,
						ProviderName = resource.IdProvider != null ? resource.IdProviderNavigation.Name : null
					}
				);

			return result;
		}

		public async Task MergeResources(ResourceMergeDto dto)
		{
			Resource resource1 = _dbContext.Resources
				.FirstOrDefault(resource => resource.Id == dto.ResourceParentId);

			if (resource1 is null)
			{
				throw new BadRequestException("Parent resource not found");
			}

			Resource resource2 = _dbContext.Resources
				.FirstOrDefault(resource => resource.Id == dto.ResourceChildId);

			if (resource2 is null)
			{
				throw new BadRequestException("Parent resource not found");
			}

			await _dbContext.Items
				.Where(item => item.IdResource == resource2.Id)
				.ForEachAsync
				(
					item =>
					{
						item.IdResource = resource1.Id;
						item.UpdatedOn = DateTime.Now;
						_dbContext.Entry(item).State = EntityState.Modified;
					}
				);

			await _dbContext.ShoppingListEntries
				.Where(shoppingListEntry => shoppingListEntry.IdResource == resource2.Id)
				.ForEachAsync
				(
					shoppingListEntry =>
					{
						shoppingListEntry.IdResource = resource1.Id;
						shoppingListEntry.UpdatedOn = DateTime.Now;
						_dbContext.Entry(shoppingListEntry).State = EntityState.Modified;
					}
				);

			await _dbContext.EssentialLists
				.Where(essentialList => essentialList.IdResource == resource2.Id)
				.ForEachAsync
				(
					essentialList =>
					{
						essentialList.IdResource = resource1.Id;
						essentialList.UpdatedOn = DateTime.Now;
						_dbContext.Entry(essentialList).State = EntityState.Modified;
					}
				);

			_dbContext.Resources.Remove(resource2);

			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateResource(ResourceUpdateDto dto, int resourceId)
		{
			Resource resource = await _dbContext.Resources
				.FirstOrDefaultAsync(resource => resource.Id == resourceId);

			if (resource == null)
			{
				throw new NotFoundException("Resource not found");
			}

			bool changed = false;

			if (dto.NewCategoryId is int newCategoryId && _dbContext.Categories.Any(category => category.Id == newCategoryId))
			{
				resource.IdCategory = newCategoryId;
				changed = true;
			}
			else
			{
				throw new BadRequestException("Category not found");
			}

			if (dto.NewProviderId is int newProviderId && _dbContext.Providers.Any(provider => provider.Id == newProviderId))
			{
				resource.IdProvider = newProviderId;
				changed = true;
			}
			else
			{
				throw new BadRequestException("Provider not found");
			}

			if (dto.NewImage != null)
			{
				resource.Image = dto.NewImage;
				changed = true;
			}

			if (dto.NewName != null)
			{
				resource.Name = dto.NewName;
				changed = true;
			}

			if (dto.NewDescription != null)
			{
				resource.Description = dto.NewDescription;
				changed = true;
			}

			if (changed)
			{
				resource.UpdatedOn = DateTime.Now;
				_dbContext.Entry(resource).State = EntityState.Modified;
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}
