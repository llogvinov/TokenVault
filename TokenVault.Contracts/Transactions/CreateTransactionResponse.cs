public record CreateTransactionResponse(
    Guid Id,
    string AssetSymbol ,
    double Quantity,
    double Price,
    double Total,
    DateTime Date);