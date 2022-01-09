﻿using System;
using System.Runtime.Serialization;

namespace DomesticWarehousemanWebApi.Exceptions
{
	[Serializable]
	internal class BadRequestException : Exception
	{
		public BadRequestException() : base()
		{
		}

		public BadRequestException(string message) : base(message)
		{
		}

		public BadRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}