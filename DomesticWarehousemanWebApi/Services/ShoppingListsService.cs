using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.DTO.ShoppingList;
using DomesticWarehousemanWebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomesticWarehousemanWebApi.Exceptions;
using DomesticWarehousemanWebApi.DTO.Category;
using DomesticWarehousemanWebApi.DTO.Account;
using DomesticWarehousemanWebApi.DTO.ShoppingListEntry;

namespace DomesticWarehousemanWebApi.Services
{
	public class ShoppingListsService : IShoppingListsService
	{
		private readonly DomesticWarehousemanDbContext _dbContext;

		public ShoppingListsService(DomesticWarehousemanDbContext dbContext): base()
		{
			_dbContext = dbContext;
		}

		public async Task<int> CreateShoppingList(ShoppingListCreateDto dto, int storageId, int accountId)
		{
			ShoppingList list = new ShoppingList()
			{
				CreatedOn = DateTime.Now,
				UpdatedOn = DateTime.Now,
				IdAccountCreator = accountId,
				IdAccountOwner = accountId,
				IdStorage = storageId,
				Name = dto.Name,
				IdCategory = dto.CategoryId
			};

			foreach (var entryDto in dto.Entries)
			{
				ShoppingListEntry entry = new ShoppingListEntry()
				{
					Amount = entryDto.Amount,
					IdResource = entryDto.ResourceId,
					Checked = false,
					CreatedOn = DateTime.Now,
					UpdatedOn = DateTime.Now,
					IdShoppingListNavigation = list
				};

				list.ShoppingListEntries.Add(entry);
			}

			await _dbContext.SaveChangesAsync();

			return list.Id;
		}

		public async Task DeleteShoppingList(int storageId, int listId)
		{
			ShoppingList list = await _dbContext.ShoppingLists
				.FirstOrDefaultAsync(list => list.Id == listId && list.IdStorage == storageId);

			if (list is null)
			{
				throw new NotFoundException("List not found");
			}

			_dbContext.ShoppingListComments
				.RemoveRange(_dbContext.ShoppingListComments.Where(comment => comment.IdShoppingList == listId));

			_dbContext.ShoppingListEntries
				.RemoveRange(_dbContext.ShoppingListEntries.Where(entry => entry.IdShoppingList == listId));

			_dbContext.ShoppingLists
				.Remove(list);

			await _dbContext.SaveChangesAsync();
		}

		public async Task<ShoppingListDetailsDto> GetShoppingListDetails(int storageId, int listId)
		{
			ShoppingList list = await _dbContext.ShoppingLists
				.FirstOrDefaultAsync(list => list.Id == listId && list.IdStorage == storageId);

			Account creator = await _dbContext.Accounts
				.FirstOrDefaultAsync(account => account.Id == list.IdAccountCreator);

			Account owner = await _dbContext.Accounts
				.FirstOrDefaultAsync(account => account.Id == list.IdAccountOwner);

			var categoryDto = list.IdCategory != null ?
				new CategoryIndexDto()
				{
					Id = list.IdCategoryNavigation.Id,
					Name = list.IdCategoryNavigation.Name
				} :
				null;

			var entriesDto = new List<ShoppingListEntryDetailsDto>();

			entriesDto.AddRange
			(
				_dbContext.ShoppingListEntries
				.Where(entry => entry.IdShoppingList == list.Id)
				.Select
				(
					entry => new ShoppingListEntryDetailsDto()
					{
						ResourceId = entry.IdResource,
						Amount = entry.Amount,
						CreationDate = entry.CreatedOn,
						Checked = entry.Checked
					}
				)
			);

			var listDto = new ShoppingListDetailsDto()
			{
				CreationDate = list.CreatedOn,
				Category = categoryDto,
				Creator = new AccountIndexDto()
				{
					Id = creator.Id,
					DisplayName = creator.DisplayName,
					SystemAdministrator = creator.SystemAdministrator
				},
				Owner = new AccountIndexDto()
				{
					Id = owner.Id,
					DisplayName = owner.DisplayName,
					SystemAdministrator = owner.SystemAdministrator
				},
				Name = list.Name,
				Entries = entriesDto
			};

			return listDto;
		}

		public IEnumerable<ShoppingListIndexDto> IndexShoppingLists(int storageId)
		{
			var result = _dbContext.ShoppingLists
				.Where(list => list.IdStorage == storageId)
				.Select
				(
					list => new ShoppingListIndexDto()
					{
						Id = list.Id,
						Name = list.Name,
						Category = list.IdCategory != null ? 
							new CategoryIndexDto()
							{
								Name = list.IdCategoryNavigation.Name,
								Id = list.IdCategoryNavigation.Id
							} :
							null
					}
				);

			return result;
		}

		public async Task UpdateShoppingList(ShoppingListUpdateDto dto, int storageId, int listId)
		{
			ShoppingList list = await _dbContext.ShoppingLists
				.FirstOrDefaultAsync(list => list.IdStorage == storageId && list.Id == listId);

			if (list is null)
			{
				throw new NotFoundException("List not found");
			}

			if (dto.RemovedEntries != null)
			{
				foreach (var removedEntryId in dto.RemovedEntries)
				{
					_dbContext.ShoppingListEntries
						.Remove
						(
							await _dbContext.ShoppingListEntries
								.FirstOrDefaultAsync(entry => entry.Id == removedEntryId)
						);
				}
			}

			var now = DateTime.Now;

			await _dbContext.ShoppingListEntries
				.AddRangeAsync
				(
					dto.NewEntries
					.Select
					(
						entry => new ShoppingListEntry()
						{
							Amount = entry.Amount,
							IdResource = entry.ResourceId,
							Checked = false,
							CreatedOn = now,
							UpdatedOn = now,
							IdShoppingList = listId
						}
					)
				);

			bool changed = false;

			if (dto.NewCategoryId != null)
			{
				list.IdCategory = dto.NewCategoryId;
				changed = true;
			}

			if (dto.NewName != null)
			{
				list.Name = dto.NewName;
				changed = true;
			}

			if (dto.NewOwnerId != null)
			{
				list.IdAccountOwner = (int)dto.NewOwnerId;
				changed = true;
			}

			if (changed)
			{
				list.UpdatedOn = now;
				_dbContext.Entry(list).State = EntityState.Modified;
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}
