namespace VirtualCard.Interfaces
{
    /// <summary>
    /// Provides methods for creating instances of <see cref="ITransactionResponse#"/>
    /// </summary>
    public interface ITransactionResponseFactory
    {
        /// <summary>
        /// Instantiates a new instance of the <see cref="ITransactionResponse"/> class
        /// </summary>
        /// <param name="success">A flag indicating the success of the transaction</param>
        /// <param name="newBalance">The new card balance</param>
        /// <returns>A new instance of <see cref="ITransactionResponse"/></returns>
        ITransactionResponse Create(bool success, decimal newBalance);
    }
}
