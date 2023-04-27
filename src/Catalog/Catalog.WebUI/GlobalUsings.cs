﻿global using System.ComponentModel.DataAnnotations;
global using System.Text.Json.Serialization;
global using Catalog.Application;
global using Catalog.Application.Categories.Commands.CreateCategory;
global using Catalog.Application.Categories.Commands.DeleteCategory;
global using Catalog.Application.Categories.Commands.UpdateCategory;
global using Catalog.Application.Categories.Queries;
global using Catalog.Application.Common.Models;
global using Catalog.Application.Items.Commands.CreateItem;
global using Catalog.Application.Items.Commands.DeleteItem;
global using Catalog.Application.Items.Commands.UpdateItem;
global using Catalog.Application.Items.Queries;
global using Catalog.Infrastructure;
global using Catalog.WebUI.Extensions;
global using Catalog.WebUI.Models.Category;
global using Catalog.WebUI.Models.Item;
global using Catalog.WebUI.Models.Link;
global using Common.WebUI.Controllers;
global using Common.WebUI.Extensions;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
