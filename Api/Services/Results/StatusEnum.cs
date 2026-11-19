namespace Api.Services.Results
{
    public enum StatusEnum
    {
        Updated,
        Deleted,
        ProductNotFound,
        CategoryNotFound,
        OrderNotFound,
        UserNotFound,
        CustomerNotFound,
        UserAlreadyExists,
        CartNotFound,
        InvalidProduct,
        InvalidCategory,
        FailedDBTransaction,
        CheckedOut,
        RegistrationComplete
    }
}
