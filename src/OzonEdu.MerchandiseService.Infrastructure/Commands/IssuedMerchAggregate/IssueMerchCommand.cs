﻿using FluentValidation;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.IssuedMerchAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Extensions;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands.IssuedMerchAggregate
{
	/// <summary>
	/// Command для выдачи мерча сотруднику.
	/// </summary>
	public class IssueMerchCommand : IRequest<IssuedMerch>
	{
		/// <summary>
		/// Идентификатор сотрудника.
		/// </summary>
		public int EmployeeId { get; set; }

		/// <summary>
		/// SKU.
		/// </summary>
		public long Sku { get; set; }

		/// <summary>
		/// Количество мерча для выдачи.
		/// </summary>
		public int Quantity { get; set; }
	}

	/// <summary>
	/// Валидатор <see cref="AbstractValidator{T}"/>
	/// для <see cref="IssueMerchCommand"/>.
	/// </summary>
	public class IssueMerchCommandValidator : AbstractValidator<IssueMerchCommand>
	{
		/// <summary>
		/// .ctor
		/// </summary>
		public IssueMerchCommandValidator()
		{
			RuleFor(_ => _.EmployeeId).GreaterThan(0).WithMessage("EmployeeId should be more than 0");
			RuleFor(_ => _.Sku).MustBeValidObject(Sku.Create);
			RuleFor(_ => _.Quantity).MustBeValidObject(Quantity.Create);
		}
	}
}