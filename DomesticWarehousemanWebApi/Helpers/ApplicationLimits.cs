using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Helpers
{
	public class ApplicationLimits
	{
		public int MaxStoragesPerAccount { get; set; }
		public int MaxMembersPerStorage { get; set; }
		public int MaxItemsPerStorage { get; set; }
		public int MaxShoppingListsPerStorage { get; set; }
		public int MaxEntriesPerShoppingList { get; set; }
		public int MaxEntriesPerEssentialList { get; set; }
		public int MaxCommentsPerShoppingList { get; set; }
	}
}
