﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VFoody.Application.Common.Services;
using VFoody.Application.UseCases.Product.Commands;
using VFoody.Application.UseCases.Product.Queries;
using VFoody.Application.UseCases.Product.Queries.DetailToOrder;
using VFoody.Application.UseCases.Product.Queries.ShopProduct;
using VFoody.Application.UseCases.Shop.Queries.ShopInfo;

namespace VFoody.API.Controllers;

[Route("/api/v1/")]
public class ProductController : BaseApiController
{
    private readonly ICurrentPrincipalService _currentPrincipalService;
    private readonly IMapper _mapper;

    public ProductController(IMapper mapper, ICurrentPrincipalService currentPrincipalService)
    {
        _mapper = mapper;
        _currentPrincipalService = currentPrincipalService;
    }

    [HttpGet("customer/product/top")]
    public async Task<IActionResult> GetTopProduct(int pageIndex, int pageSize)
    {
        return this.HandleResult(await this.Mediator.Send(new GetTopProductQuery
        {
            PageIndex = pageIndex,
            PageSize = pageSize
        }));
    }

    [HttpGet("customer/product/recent")]
    public async Task<IActionResult> GetRecentOrderedProductQuery(int pageIndex, int pageSize)
    {
        string email = _currentPrincipalService.CurrentPrincipal;
        return this.HandleResult(await this.Mediator.Send(new GetRecentOrderedProductQuery
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Email = email
        }));
    }

    [HttpGet("shop/info")]
    public async Task<IActionResult> GetShopInfo(int shopId)
    {
        return HandleResult(await Mediator.Send(new GetShopInfoQuery(shopId)));
    }

    [HttpGet("shop/product")]
    public async Task<IActionResult> GetShopProduct(int shopId, int pageNum, int pageSize)
    {
        return HandleResult(await Mediator.Send(new GetShopProductQuery
        {
            ShopId = shopId,
            PageIndex = pageNum,
            PageSize = pageSize
        }));
    }


    [HttpGet("shop/product/detail")]
    public async Task<IActionResult> GetProductDetailToOrder(int productId)
    {
        return HandleResult(await Mediator.Send(new GetProductDetailToOrderQuery(productId)));
    }

    [HttpPost("shop/product/create")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest createProductRequest)
    {
        return HandleResult(await Mediator.Send(new CreateProductCommand
        {
            CreateProductRequest = createProductRequest
        }));
    }
}