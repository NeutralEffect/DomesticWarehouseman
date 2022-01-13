using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.DTO.Storage;
using DomesticWarehousemanWebApi.Exceptions;
using DomesticWarehousemanWebApi.Helpers;
using DomesticWarehousemanWebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Services
{
	public class StoragesService : IStoragesService
	{
		private readonly DomesticWarehousemanDbContext _dbContext;
		private readonly ApplicationLimits _limits;

		public StoragesService(DomesticWarehousemanDbContext dbContext, ApplicationLimits limits)
		{
			_dbContext = dbContext;
			_limits = limits;
		}

		public async Task<int> CreateStorage(StorageCreateDto dto, int accountId)
		{
			int accountOwnedStorages = _dbContext.Storages
				.Where(storage => storage.IdAccountOwner == accountId)
				.Count();

			if (accountOwnedStorages >= _limits.MaxStoragesPerAccount)
			{
				throw new BadRequestException("Owned storages limit reached");
			}

			var storage = new Storage()
			{
				DisplayName = dto.DisplayName,
				CreatedOn = DateTime.Now,
				UpdatedOn = DateTime.Now,
				IdAccountCreator = accountId,
				IdAccountOwner = accountId
			};

			int ownerRoleId = (await _dbContext.Roles
				.FirstOrDefaultAsync(role => role.Name == Constants.StorageOwnerRole)).Id;

			var member = new StorageMember()
			{
				CreatedOn = DateTime.Now,
				UpdatedOn = DateTime.Now,
				IdAccount = accountId,
				IdRole = ownerRoleId
			};

			storage.StorageMembers.Add(member);

			_dbContext.Storages
				.Add(storage);

			await _dbContext.SaveChangesAsync();

			return storage.Id;
		}

		public async Task DeleteStorage(int storageId)
		{
			var storage = await _dbContext.Storages.FirstOrDefaultAsync(storage => storage.Id == storageId);

			if (storage == null)
			{
				throw new BadRequestException("Storage does not exist");
			}

			_dbContext.StorageMembers.RemoveRange
			(
				_dbContext.StorageMembers
					.Where(storageMember => storageMember.IdStorage == storageId)
			);

			_dbContext.Items.RemoveRange
			(
				_dbContext.Items
					.Where(item => item.IdStorage == storageId)
			);

			_dbContext.RemoveRange
			(
				_dbContext.ArchivedItems
					.Where(archivedItem => archivedItem.IdStorage == storageId)
			);

			_dbContext.ShoppingListComments.RemoveRange
			(
				_dbContext.ShoppingListComments
					.Include(shoppingListComment => shoppingListComment.IdShoppingListNavigation)
					.Where(shoppingListComment => shoppingListComment.IdShoppingListNavigation.IdStorage == storageId)
			);

			_dbContext.ShoppingLists.RemoveRange
			(
				_dbContext.ShoppingLists
					.Where(shoppingList => shoppingList.IdStorage == storageId)
			);

			_dbContext.EssentialLists.RemoveRange
			(
				_dbContext.EssentialLists
					.Where(essentialList => essentialList.IdStorage == storageId)
			);

			await _dbContext.Resources
				.Where(resource => resource.IdStorage == storageId)
				.ForEachAsync(resource => resource.IdStorage = null);

			_dbContext.Remove(storage);

			await _dbContext.SaveChangesAsync();
		}

		public StorageDetailsDto GetStorageDetails(int storageId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<StorageIndexDto> IndexStoragesForAccount(int accountId)
		{
			var result = _dbContext.StorageMembers
				.Where(storageMember => storageMember.IdAccount == accountId)
				.Include(storageMember => storageMember.IdStorageNavigation)
				.Select
				(
					storageMember => new StorageIndexDto()
					{
						DisplayName = storageMember.IdStorageNavigation.DisplayName,
						Id = storageMember.IdStorage
					}
				);

			return result.AsEnumerable();
		}

		public async Task UpdateStorage(StorageUpdateDto dto, int storageId)
		{
			var storage = await _dbContext.Storages
				.FirstOrDefaultAsync(storage => storage.Id == storageId);

			if (storage == null)
			{
				throw new BadRequestException("Storage not found");
			}

			bool changed = false;

			if (dto.DisplayName != null)
			{
				storage.DisplayName = dto.DisplayName;
				changed = true;
			}

			// FIXME: If owner is changed, JWT tokens should be updated immediately
			if (dto.NewOwner != null)
			{
				storage.IdAccountOwner = (int)dto.NewOwner;

				var currentOwnerMember = await _dbContext.StorageMembers
					.FirstOrDefaultAsync
					(
						storageMember =>
							storageMember.IdRoleNavigation.Name == Constants.StorageOwnerRole &&
							storageMember.IdStorage == storageId
					);

				currentOwnerMember.IdRole = (await _dbContext.Roles
					.FirstOrDefaultAsync(role => role.Name == Constants.StorageEditorRole))
					.Id;

				var newOwnerMember = await _dbContext.StorageMembers
					.FirstOrDefaultAsync
					(
						storageMember =>
							storageMember.IdAccount == (int)dto.NewOwner &&
							storageMember.IdStorage == storageId
					);

				newOwnerMember.IdRole = (await _dbContext.Roles
					.FirstOrDefaultAsync(role => role.Name == Constants.StorageOwnerRole))
					.Id;

				currentOwnerMember.UpdatedOn = DateTime.Now;
				_dbContext.Entry(currentOwnerMember).State = EntityState.Modified;
				newOwnerMember.UpdatedOn = DateTime.Now;
				_dbContext.Entry(newOwnerMember).State = EntityState.Modified;

				changed = true;
			}

			if (changed)
			{
				storage.UpdatedOn = DateTime.Now;
				_dbContext.Entry(storage).State = EntityState.Modified;
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}
