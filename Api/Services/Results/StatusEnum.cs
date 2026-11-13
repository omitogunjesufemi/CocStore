namespace Api.Services.Results
{
    public enum StatusEnum
    {
        Updated,
        Deleted,
        ProductNotFound,
        CategoryNotFound,
        OrderNotFound,
        CartNotFound,
        InvalidProduct,
        InvalidCategory,
        FailedDBTransaction,
        CheckedOut,
    }
}
