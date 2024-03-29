﻿using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchAggregate
{
	/// <summary>
	/// Enumeration для размера одежды.
	/// </summary>
	public class ClothingSize : Enumeration
	{
		public static ClothingSize XS = new(1, nameof(XS));
		public static ClothingSize S = new(2, nameof(S));
		public static ClothingSize M = new(3, nameof(M));
		public static ClothingSize L = new(4, nameof(L));
		public static ClothingSize XL = new(5, nameof(XL));
		public static ClothingSize XXL = new(6, nameof(XXL));

		/// <summary>
		/// .ctor
		/// </summary>
		private ClothingSize(int id, string name) : base(id, name)
		{
		}
	}
}