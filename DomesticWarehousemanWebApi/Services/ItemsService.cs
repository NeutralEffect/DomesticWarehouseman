using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.DTO.Category;
using DomesticWarehousemanWebApi.DTO.Item;
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
	public class ItemsService : IItemsService
	{
		private readonly DomesticWarehousemanDbContext _dbContext;

		public ItemsService(DomesticWarehousemanDbContext dbContext): base()
		{
			_dbContext = dbContext;
		}

		public async Task<int> CreateItem(ItemCreateDto dto, int storageId)
		{
			if (null == _dbContext.Resources.FirstOrDefault(resource => resource.Id == dto.ResourceId))
			{
				throw new BadRequestException("Referenced resource does not exist");
			}

			var item = new Item()
			{
				CreatedOn = DateTime.Now,
				UpdatedOn = DateTime.Now,
				ExpiresOn = dto.ExpirationDate,
				IdResource = dto.ResourceId,
				IdStorage = storageId
			};

			_dbContext.Items.Add(item);

			await _dbContext.SaveChangesAsync();

			return item.Id;
		}

		public async Task DeleteItem(ItemDeleteDto dto, int storageId, int itemId)
		{
			var item = _dbContext.Items.FirstOrDefault(item => item.Id == itemId && item.IdStorage == storageId);

			if (item == null)
			{
				throw new NotFoundException("Item does not exist");
			}

			var archivedItem = new ArchivedItem()
			{
				ArchivedOn = DateTime.Now,
				CreatedOn = item.CreatedOn,
				UpdatedOn = item.UpdatedOn,
				IdArchivingReason = dto.ArchivingReasonId,
				ExpiresOn = item.ExpiresOn,
				IdResource = item.IdResource,
				IdStorage = item.IdStorage
			};

			_dbContext.ArchivedItems.Add(archivedItem);

			_dbContext.Items.Remove(item);

			await _dbContext.SaveChangesAsync();
		}

		public ItemDetailsDto GetItemDetails(int storageId, int itemId)
		{
			var item = _dbContext.Items
				.FirstOrDefault(item => item.Id == itemId && item.IdStorage == storageId);

			if (item is null)
			{
				throw new NotFoundException("Item not found");
			}

			var resource = item.IdResourceNavigation;

			var category = item.IdResourceNavigation.IdCategoryNavigation;

			var provider = item.IdResourceNavigation.IdProviderNavigation;

			var categoryDto = category != null ? new CategoryDetailsDto()
			{
				Id = category.Id,
				Name = category.Name
			} : null;

			var providerDto = provider != null ? new ProviderDetailsDto()
			{
				Id = provider.Id,
				Name = provider.Name
			} : null;

			var itemDto = new ItemDetailsDto()
			{
				Id = item.Id,
				ExpirationDate = item.ExpiresOn,
				Resource = new ResourceDetailsDto()
				{
					Id = resource.Id,
					Description = resource.Description,
					CreationDate = resource.CreatedOn,
					Name = resource.Name,
					Image = resource.Image,
					Category = categoryDto,
					Provider = providerDto
				}
			};

			return itemDto;
		}

		public IEnumerable<ItemIndexDto> IndexExpiringItems(int storageId, TimeSpan maxTimeToExpiration)
		{
			var now = DateTime.Now;

			var result = _dbContext.Items
				.Where
				(
					item =>
					item.IdStorage == storageId &&
					item.ExpiresOn != null &&
					now.Add(maxTimeToExpiration) >= item.ExpiresOn
				)
				.Select
				(
					item => new ItemIndexDto()
					{
						Id = item.Id,
						ResourceId = item.IdResource,
						ExpirationDate = (DateTime)item.ExpiresOn,
						ResourceName = item.IdResourceNavigation.Name
					}
				);

			return result;
		}

		public IEnumerable<ItemIndexDto> IndexItems(int storageId)
		{
			var result = _dbContext.Items
				.Where(item => item.IdStorage == storageId)
				.Select
				(
					item => new ItemIndexDto()
					{
						Id = item.Id,
						ResourceId = item.IdResource,
						ExpirationDate = (DateTime)item.ExpiresOn,
						ResourceName = item.IdResourceNavigation.Name
					}
				);

			return result;
		}

		public async Task UpdateItem(ItemUpdateDto dto, int storageId, int itemId)
		{
			var item = _dbContext.Items
				.FirstOrDefault(item => item.IdStorage == storageId && item.Id == itemId);

			if (item is null)
			{
				throw new NotFoundException("Item not found");
			}

			bool changed = false;

			if (dto.NewExpirationDate != null)
			{
				item.ExpiresOn = (DateTime)dto.NewExpirationDate;
				changed = true;
			}

			if (dto.NewResourceId != null)
			{
				item.IdResource = (int)dto.NewResourceId;
				changed = true;
			}

			if (changed)
			{
				item.UpdatedOn = DateTime.Now;
				_dbContext.Entry(item).State = EntityState.Modified;
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}
