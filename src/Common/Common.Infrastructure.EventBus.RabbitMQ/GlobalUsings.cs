﻿global using System.Net.Sockets;
global using System.Text;
global using System.Text.Json;
global using Common.Infrastructure.EventBus.Abstractions;
global using Common.Infrastructure.EventBus.Events;
global using Common.Infrastructure.EventBus.Extensions;
global using Microsoft.Extensions.Logging;
global using Polly;
global using Polly.Retry;
global using RabbitMQ.Client;
global using RabbitMQ.Client.Events;
global using RabbitMQ.Client.Exceptions;