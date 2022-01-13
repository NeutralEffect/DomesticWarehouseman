using DomesticWarehousemanWebApi.DTO.Resource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Services.Interfaces
{
	public interface IResourcesService
	{
		public Task<int> CreateResource(ResourceCreateDto dto, int storageId);
		public ResourceDetailsDto GetResourceDetails(int resourceId);
		public IEnumerable<ResourceIndexDto> IndexResources();
		public Task UpdateResource(ResourceUpdateDto dto, int resourceId);
		public Task MergeResources(ResourceMergeDto dto);
	}
}
