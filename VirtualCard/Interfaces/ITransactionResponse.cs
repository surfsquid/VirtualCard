namespace VirtualCard.Interfaces
{
    /// <summary>
    /// Defines the properties of a response for a single card transaction
    /// </summary>
    public interface ITransactionResponse
    {
        /// <summary>
        /// Gets a flag indicating the success of the transaction
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the new card balance
        /// </summary>
        decimal NewBalance { get; }
    }
}