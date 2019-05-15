using VirtualCard.Interfaces;

namespace VirtualCard
{
    /// <summary>
    /// Defines the properties of a response for a single card transaction
    /// </summary>
    public class TransactionResponse : ITransactionResponse
    {
        /// <summary>
        /// Instantiates a new instance of the <see cref="TransactionResponse"/> class.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="newBalance"></param>
        public TransactionResponse(bool success, decimal newBalance)
        {
            Success = success;
            NewBalance = newBalance;
        }

        /// <summary>
        /// Gets a flag indicating the success of the transaction
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Gets the new card balance
        /// </summary>
        public decimal NewBalance { get; }
    }
}