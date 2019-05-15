namespace VirtualCard.Interfaces
{
    /// <summary>
    /// Defines the available methods and properties for all cards
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Gets the current card balance
        /// </summary>
        decimal CurrentBalance { get; }

        /// <summary>
        /// Withdraws funds from the card
        /// </summary>
        /// <param name="pin">The personal identification number for the card</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <returns>An instance of <see cref="ITransactionResponse"/> containing information about the transaction</returns>
        ITransactionResponse WithdrawFunds(int pin, decimal amount);

        /// <summary>
        /// Tops up the card with the amount specified
        /// </summary>
        /// <param name="amount">The amount to top up</param>
        /// <returns>An instance of <see cref="ITransactionResponse"/> containing information about the transaction</returns>
        ITransactionResponse TopUpCard(decimal amount);
    }
}
