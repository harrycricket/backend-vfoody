﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VFoody.Application.Common.Services;
using VFoody.Application.UseCases.Accounts.Commands;
using VFoody.Application.UseCases.Accounts.Commands.ReVerify;
using VFoody.Application.UseCases.Accounts.Commands.Verify;
using VFoody.Application.UseCases.Promotion.Queries;
using VFoody.Application.UseCases.Promotion.Queries.Customer;
using VFoody.Application.UseCases.Promotion.Queries.Platform;
using VFoody.Application.UseCases.Promotion.Queries.Shop;

namespace VFoody.API.Controllers;

[Route("/api/v1/")]
public class PromotionController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly ICurrentPrincipalService _currentPrincipalService;

    public PromotionController(IMapper mapper, ICurrentPrincipalService currentPrincipalService)
    {
        _mapper = mapper;
        _currentPrincipalService = currentPrincipalService;
    }

    [HttpGet("customer/promotion")]
    public async  Task<IActionResult> GetCustomerPromotion(int pageIndex = 1, int pageSize = 20)
    {
        return this.HandleResult(await this.Mediator.Send(new GetCustomerPromotionListQuery
        {
            AccountId = 1,
            Status = 1,
            Available = true,
            PageIndex = pageIndex,
            PageSize = pageSize,
        }));
    }

    [HttpGet("customer/promotion/shop/{id}")]
    public async Task<IActionResult> GetShopPromotion(int id, int pageIndex = 1, int pageSize = 20)
    {
        return this.HandleResult(await this.Mediator.Send(new GetShopPromotionListQuery
        {
            ShopId = id,
            Status = 1,
            Available = true,
            PageIndex = pageIndex,
            PageSize = pageSize,
        }));
    }

    [HttpGet("customer/promotion/platform")]
    public async Task<IActionResult> GetPlatformPromotion(int pageIndex = 1, int pageSize = 20)
    {
        return this.HandleResult(await this.Mediator.Send(new GetPlatformPromotionListQuery
        {
            Status = 1,
            Available = true,
            PageIndex = pageIndex,
            PageSize = pageSize,
        }));
    }
}