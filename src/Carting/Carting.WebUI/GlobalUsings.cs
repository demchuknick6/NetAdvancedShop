﻿global using System.IdentityModel.Tokens.Jwt;
global using System.Text.Json.Serialization;
global using MediatR;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authorization.Policy;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;
global using NetAdvancedShop.Carting;
global using NetAdvancedShop.Carting.Application.Commands.AddCartItem;
global using NetAdvancedShop.Carting.Application.Commands.RemoveCartItem;
global using NetAdvancedShop.Carting.Application.Commands.UpdateCartItem;
global using NetAdvancedShop.Carting.Application.Queries;
global using NetAdvancedShop.Carting.WebUI.ApplicationEvents.EventHandling;
global using NetAdvancedShop.Carting.WebUI.Extensions;
global using NetAdvancedShop.Carting.WebUI.Filters;
global using NetAdvancedShop.Carting.WebUI.Middleware;
global using NetAdvancedShop.Carting.WebUI.Models.Cart;
global using NetAdvancedShop.Carting.WebUI.Models.CartItem;
global using NetAdvancedShop.Catalog.Application.ApplicationEvents;
global using NetAdvancedShop.Common.Infrastructure.EventBus;
global using NetAdvancedShop.Common.Infrastructure.EventBus.Abstractions;
global using NetAdvancedShop.Common.Infrastructure.EventBus.RabbitMQ;
global using NetAdvancedShop.Common.Rbac;
global using NetAdvancedShop.Common.WebUI.Controllers;
global using NetAdvancedShop.Common.WebUI.Extensions;
global using RabbitMQ.Client;
global using Swashbuckle.AspNetCore.SwaggerGen;
