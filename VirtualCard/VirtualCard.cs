using System;
using VirtualCard.Interfaces;

namespace VirtualCard
{
    /// <summary>
    /// An implementation of <see cref="ICard"/> for a virtual payment card
    /// </summary>
    public class VirtualCard : ICard
    {
        private decimal _currentBalance;
        private readonly ITransactionResponseFactory _transactionResponseFactory;
        private readonly int _pin;
        private const decimal _minimumTopUp = 0M;
        private const decimal _maximumTopUp = 100M;
        private const decimal _minimumExpense = 0.01M;
        private const decimal _maximumExpense = 100000M;

        /// <summary>
        /// Gets the current card balance
        /// </summary>
        public decimal CurrentBalance { get { return _currentBalance; } }

        /// <summary>
        /// Instantiates a new instance of the <see cref="VirtualCard"/> class.
        /// </summary>
        /// <param name="balance">The initial card balance</param>
        /// <param name="pin">The card pin</param>
        /// <param name="transactionResponseFactory">The transaction response factory</param>
        public VirtualCard(decimal balance, int pin, ITransactionResponseFactory transactionResponseFactory)
        {
            _currentBalance = balance;
            _pin = pin;
            _transactionResponseFactory = transactionResponseFactory;
        }


        /// <summary>
        /// Tops up the card with the amount specified
        /// </summary>
        /// <param name="amount">The amount to top up</param>
        /// <returns>An instance of <see cref="ITransactionResponse"/> containing information about the transaction</returns>
        public ITransactionResponse TopUpCard(decimal amount)
        {
            if (amount <= _minimumTopUp)
            {
                throw new ArgumentException($"{nameof(amount)} must exceed minimum top up of {_minimumTopUp}");
            }

            if (amount > _maximumTopUp)
            {
                throw new ArgumentException($"{nameof(amount)} must not exceed maximum top up of {_maximumTopUp}");
            }

            _currentBalance += amount;
            return _transactionResponseFactory.Create(true, CurrentBalance);
        }

        /// <summary>
        /// Withdraws funds from the card
        /// </summary>
        /// <param name="pin">The personal identification number for the card</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <returns>An instance of <see cref="ITransactionResponse"/> containing information about the transaction</returns>
        public ITransactionResponse WithdrawFunds(int pin, decimal amount)
        {
            if (amount <= _minimumExpense)
            {
                throw new ArgumentException($"{nameof(amount)} must exceed minimum expense of {_minimumExpense}");
            }

            if (amount > _maximumTopUp)
            {
                throw new ArgumentException($"{nameof(amount)} must not exceed maximum expense of {_maximumExpense}");
            }

            if (pin != _pin)
            {
                throw new ArgumentException($"Incorrect PIN supplied");
            }

            if (amount > _currentBalance)
            {
                throw new ArgumentException($"Withdrawal amount exceeds current card balance");
            }

            _currentBalance -= amount;
            return _transactionResponseFactory.Create(true, CurrentBalance);
        }
    }
}
