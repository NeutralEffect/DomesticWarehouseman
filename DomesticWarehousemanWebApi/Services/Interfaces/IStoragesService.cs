using DomesticWarehousemanWebApi.DTO.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Services.Interfaces
{
	public interface IStoragesService
	{
		public Task<int> CreateStorage(StorageCreateDto dto, int accountId);
		public Task DeleteStorage(int storageId);
		public StorageDetailsDto GetStorageDetails(int storageId);
		public IEnumerable<StorageIndexDto> IndexStoragesForAccount(int accountId);
		public Task UpdateStorage(StorageUpdateDto dto, int storageId);
	}
}
