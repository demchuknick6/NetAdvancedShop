namespace NetAdvancedShop.Tests.Common.Extensions;

public static class MediatorMockExtensions
{
    public static void SetupSendReturns<TResponse>(this Mock<IMediator> mediatorMock, TResponse response)
    {
        mediatorMock.Setup(x => x.Send(It.IsAny<IRequest<TResponse>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
    }

    public static void SetupSendReturns<TResponse>(this Mock<IMediator> mediatorMock, Func<TResponse> responseFunc)
    {
        mediatorMock.Setup(x => x.Send(It.IsAny<IRequest<TResponse>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(responseFunc);
    }
}