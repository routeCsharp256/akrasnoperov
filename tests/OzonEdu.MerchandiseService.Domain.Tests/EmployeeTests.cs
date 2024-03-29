﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.IssuedMerchAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
	/// <summary>
	/// Тесты для <see cref="Employee"/>.
	/// </summary>
	[Trait("Category", "Unit")]
	public class EmployeeTests
	{
		#region IsMerchFit

		[Fact]
		public void IsMerchFitWithMerchNoClothingSize()
		{
			var sut = new Employee(ClothingSize.L, Email.Create("test@ozon.ru"));
			var merch = new Merch(
				Sku.Create(123),
				Name.Create("Name"),
				MerchType.Bag);

			var result = sut.IsMerchFit(merch);

			result.Should().BeTrue("Подходит по размеру, так как мерч не имеет размера.");
		}

		[Fact]
		public void IsMerchFitWithMerchSameClothingSize()
		{
			var sut = new Employee(ClothingSize.L, Email.Create("test@ozon.ru"));
			var merch = new Merch(
				Sku.Create(123),
				Name.Create("Name"),
				MerchType.Sweatshirt,
				ClothingSize.L);

			var result = sut.IsMerchFit(merch);

			result.Should().BeTrue("Подходит по размеру, так как размер мерча совпадает с размером одежды сотрудника");
		}

		[Fact]
		public void IsMerchFitWithMerchNotSameClothingSize()
		{
			var sut = new Employee(ClothingSize.L, Email.Create("test@ozon.ru"));
			var merch = new Merch(
				Sku.Create(123),
				Name.Create("Name"),
				MerchType.Sweatshirt,
				ClothingSize.M);

			var result = sut.IsMerchFit(merch);

			result.Should().BeFalse("Не подходит по размеру, так как размер мерча не совпадает с размером одежды сотрудника");
		}

		#endregion

		#region HasIssuedMerch

		[Fact]
		public void HasIssuedMerchToEmployeeWithEmptyIssuedMerch()
		{
			var sut = new Employee(ClothingSize.L, Email.Create("test@ozon.ru"));
			var merch = new Merch(
				Sku.Create(123),
				Name.Create("Name"),
				MerchType.Bag);

			var result = sut.HasIssuedMerch(merch, DateTimeOffset.UtcNow);

			result.Should().BeFalse();
		}

		[Fact]
		public void HasIssuedMerchToEmployeeWithNoEmptyIssuedMerch()
		{
			var sut = new Employee(ClothingSize.L, Email.Create("test@ozon.ru"));
			var issuedMerch = new Merch(
				1,
				Sku.Create(123),
				Name.Create("Name"),
				MerchType.Bag);
			sut.IssuedMerches = new HashSet<IssuedMerch>
			{
				new (DateTimeOffset.UtcNow, Quantity.Create(1), Status.Issued, issuedMerch, sut)
			};
			var merch = new Merch(
				2,
				Sku.Create(1234),
				Name.Create("Name"),
				MerchType.Notepad);

			var result = sut.HasIssuedMerch(merch, DateTimeOffset.UtcNow);

			result.Should().BeFalse();
		}

		[Theory]
		[InlineData(365)]
		[InlineData(366)]
		[InlineData(400)]
		public void HasIssuedMerchToEmployeeWithIssuedMerch1YearAndMoreAgo365DaysInYear(int daysCount)
		{
			var sut = new Employee(ClothingSize.L, Email.Create("test@ozon.ru"));
			var merchIssuedDate = new DateTimeOffset(new DateTime(2021, 01, 01), TimeSpan.Zero);
			var issuedMerch = new Merch(
				Sku.Create(123),
				Name.Create("Name"),
				MerchType.Notepad);
			sut.IssuedMerches = new HashSet<IssuedMerch>
			{
				new (merchIssuedDate, Quantity.Create(1), Status.Issued, issuedMerch, sut)
			};

			var result = sut.HasIssuedMerch(issuedMerch, merchIssuedDate.AddDays(daysCount));

			result.Should().BeFalse();
		}

		[Theory]
		[InlineData(366)]
		[InlineData(400)]
		public void HasIssuedMerchToEmployeeWithIssuedMerch1YearAndMoreAgo366DaysInYear(int daysCount)
		{
			var sut = new Employee(ClothingSize.L, Email.Create("test@ozon.ru"));
			var merchIssuedDate = new DateTimeOffset(new DateTime(2020, 01, 01), TimeSpan.Zero);
			var issuedMerch = new Merch(
				Sku.Create(123),
				Name.Create("Name"),
				MerchType.Notepad);
			sut.IssuedMerches = new HashSet<IssuedMerch>
			{
				new (merchIssuedDate, Quantity.Create(1), Status.Issued, issuedMerch, sut)
			};

			var result = sut.HasIssuedMerch(issuedMerch, merchIssuedDate.AddDays(daysCount));

			result.Should().BeFalse();
		}

		[Theory]
		[InlineData(364)]
		[InlineData(363)]
		[InlineData(300)]
		public void AddIssuedMerchToEmployeeWithIssuedMerchLessThan1YearAgo365DaysInYear(int daysCount)
		{
			var sut = new Employee(ClothingSize.L, Email.Create("test@ozon.ru"));
			var merchIssuedDate = new DateTimeOffset(new DateTime(2021, 01, 01), TimeSpan.Zero);
			var issuedMerch = new Merch(
				Sku.Create(123),
				Name.Create("Name"),
				MerchType.Notepad);
			sut.IssuedMerches = new HashSet<IssuedMerch>
			{
				new (merchIssuedDate, Quantity.Create(1), Status.Issued, issuedMerch, sut)
			};

			var result = sut.HasIssuedMerch(issuedMerch, merchIssuedDate.AddDays(daysCount));

			result.Should().BeTrue();
		}

		[Theory]
		[InlineData(365)]
		[InlineData(364)]
		[InlineData(363)]
		[InlineData(300)]
		public void HasIssuedMerchToEmployeeWithIssuedMerchLessThan1YearAgo366DaysInYear(int daysCount)
		{
			var sut = new Employee(ClothingSize.L, Email.Create("test@ozon.ru"));
			var merchIssuedDate = new DateTimeOffset(new DateTime(2020, 01, 01), TimeSpan.Zero);
			var issuedMerch = new Merch(
				Sku.Create(123),
				Name.Create("Name"),
				MerchType.Notepad);
			sut.IssuedMerches = new HashSet<IssuedMerch>
			{
				new (merchIssuedDate, Quantity.Create(1), Status.Issued, issuedMerch, sut)
			};

			var result = sut.HasIssuedMerch(issuedMerch, merchIssuedDate.AddDays(daysCount));

			result.Should().BeTrue();
		}

		#endregion
	}
}