public record CreateTransactionResponse(
    Guid Id,
    Guid Userid,
    Guid PortfolioId,
    string AssetSymbol ,
    double Quantity,
    double Price,
    double Total,
    DateTime Date);