﻿global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;
global using System.Text.Json.Serialization;
global using MediatR;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.OpenApi.Models;
global using NetAdvancedShop.Catalog.Application;
global using NetAdvancedShop.Catalog.Application.ApplicationEvents;
global using NetAdvancedShop.Catalog.Application.Categories.Commands.CreateCategory;
global using NetAdvancedShop.Catalog.Application.Categories.Commands.DeleteCategory;
global using NetAdvancedShop.Catalog.Application.Categories.Commands.UpdateCategory;
global using NetAdvancedShop.Catalog.Application.Categories.Queries;
global using NetAdvancedShop.Catalog.Application.Common.Models;
global using NetAdvancedShop.Catalog.Application.Items.Commands.CreateItem;
global using NetAdvancedShop.Catalog.Application.Items.Commands.DeleteItem;
global using NetAdvancedShop.Catalog.Application.Items.Commands.UpdateItem;
global using NetAdvancedShop.Catalog.Application.Items.Queries;
global using NetAdvancedShop.Catalog.Infrastructure;
global using NetAdvancedShop.Catalog.WebUI.ApplicationEvents;
global using NetAdvancedShop.Catalog.WebUI.Extensions;
global using NetAdvancedShop.Catalog.WebUI.Filters;
global using NetAdvancedShop.Catalog.WebUI.Models.Category;
global using NetAdvancedShop.Catalog.WebUI.Models.Item;
global using NetAdvancedShop.Catalog.WebUI.Models.Link;
global using NetAdvancedShop.Common.Infrastructure.EventBus;
global using NetAdvancedShop.Common.Infrastructure.EventBus.Abstractions;
global using NetAdvancedShop.Common.Infrastructure.EventBus.Events;
global using NetAdvancedShop.Common.Infrastructure.EventBus.RabbitMQ;
global using NetAdvancedShop.Common.Rbac;
global using NetAdvancedShop.Common.WebUI.Controllers;
global using NetAdvancedShop.Common.WebUI.Extensions;
global using RabbitMQ.Client;
global using Swashbuckle.AspNetCore.SwaggerGen;
