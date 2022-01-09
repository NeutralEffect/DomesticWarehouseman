using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Exceptions
{
	[Serializable]
	public class ForbiddenException : Exception
	{
		public ForbiddenException() : base()
		{
		}

		public ForbiddenException(string message) : base(message)
		{
		}

		public ForbiddenException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
