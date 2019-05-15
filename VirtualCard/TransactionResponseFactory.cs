using VirtualCard.Interfaces;

namespace VirtualCard
{
    /// <summary>
    /// Provides methods for creating instances of <see cref="ITransactionResponse#"/>
    /// </summary>
    class TransactionResponseFactory : ITransactionResponseFactory
    {
        /// <summary>
        /// Instantiates a new instance of the <see cref="ITransactionResponse"/> class
        /// </summary>
        /// <param name="success">A flag indicating the success of the transaction</param>
        /// <param name="newBalance">The new card balance</param>
        /// <returns>A new instance of <see cref="ITransactionResponse"/></returns>
        public ITransactionResponse Create(bool success, decimal newBalance)
        {
            return new TransactionResponse(success, newBalance);
        }
    }
}
