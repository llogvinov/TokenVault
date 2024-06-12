public record CreateTransactionResponse(
    Guid Id,
    Guid PortfolioId,
    string AssetSymbol ,
    double Quantity,
    double Price,
    double Total,
    DateTime Date);