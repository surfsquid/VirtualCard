using Moq;
using Shouldly;
using System;
using VirtualCard.Interfaces;
using Xunit;

namespace VirtualCard.Tests
{
    public class VirtualCardTests
    {
        private const decimal _startingBalance = 100M;
        private const int _pin = 1001;
        private readonly Mock<ITransactionResponseFactory> _mockResponseFactory;
        private VirtualCard _target;

        public VirtualCardTests()
        {
            _mockResponseFactory = new Mock<ITransactionResponseFactory>();
            _mockResponseFactory.Setup(f => f.Create(It.IsAny<bool>(), It.IsAny<decimal>())).Returns((bool success, decimal newBalance) => new TransactionResponse(success, newBalance));
            _target = new VirtualCard(_startingBalance, _pin, _mockResponseFactory.Object);
        }

        [Fact]
        public void VirtualCard_Constructor_ReturnsInstanceOfClass()
        {
            _target.ShouldBeAssignableTo<VirtualCard>();
            _target.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-100000)]
        public void VirtualCard_GivenNegativeAmount_TopUpCard_ThrowsException(decimal amount)
        {
            Should.Throw<ArgumentException>(() => _target.TopUpCard(amount));
        }

        [Theory]
        [InlineData(10000)]
        [InlineData(2000000)]
        public void VirtualCard_GivenExcessiveAmount_TopUpCard_ThrowsException(decimal amount)
        {
            Should.Throw<ArgumentException>(() => _target.TopUpCard(amount));
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-100000)]
        public void VirtualCard_GivenNegativeAmount_Withdraw_ThrowsException(decimal amount)
        {
            Should.Throw<ArgumentException>(() => _target.WithdrawFunds(_pin, amount));
        }

        [Theory]
        [InlineData(10000)]
        [InlineData(20000000)]
        public void VirtualCard_GivenExcessiveAmount_Withdraw_ThrowsException(decimal amount)
        {
            Should.Throw<ArgumentException>(() => _target.WithdrawFunds(_pin, amount));
        }

        [Theory]
        [InlineData(1234)]
        [InlineData(8)]
        public void VirtualCard_GivenInvalidPin_Withdraw_ThrowsException(int invalidPin)
        {
            Should.Throw<ArgumentException>(() => _target.WithdrawFunds(invalidPin, 100M));
        }

        [Theory]
        [InlineData(10)]
        [InlineData(45)]
        public void VirtualCard_GivenValidAmount_Withdraw_ReturnsValidResponse(decimal amount)
        {
            var result = _target.WithdrawFunds(_pin, amount);

            result.NewBalance.ShouldBe(_startingBalance - amount);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(45)]
        public void VirtualCard_GivenValidAmount_TopUpCard_ReturnsValidResponse(decimal amount)
        {
            var result = _target.TopUpCard(amount);

            result.NewBalance.ShouldBe(_startingBalance + amount);
        }
    }
}
