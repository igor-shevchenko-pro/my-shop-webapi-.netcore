namespace MyShop.ApiModels
{
    #region Sorting
    public enum EntitySortingEnum
    {
        ByCreateAsc = 0,
        ByCreateDesc = 1,
        ByUpdateAsc = 2,
        ByUpdateDesc = 3,
        ByActivityStatusAsc = 4,
        ByActivityStatusDesc = 5,
    }

    public enum BrandSortingEnum
    {
        ByCreateAsc = 0,
        ByCreateDesc = 1,
        ByUpdateAsc = 2,
        ByUpdateDesc = 3,
        ByActivityStatusAsc = 4,
        ByActivityStatusDesc = 5,
    }

    public enum CategorySortingEnum
    {
        ByCreateAsc = 0,
        ByCreateDesc = 1,
        ByUpdateAsc = 2,
        ByUpdateDesc = 3,
        ByActivityStatusAsc = 4,
        ByActivityStatusDesc = 5,
    }

    public enum FileEntitySortingEnum
    {
        ByCreateAsc = 0,
        ByCreateDesc = 1,
        ByUpdateAsc = 2,
        ByUpdateDesc = 3,
        ByActivityStatusAsc = 4,
        ByActivityStatusDesc = 5,
    }

    public enum ModificationSortingEnum
    {
        ByCreateAsc = 0,
        ByCreateDesc = 1,
        ByUpdateAsc = 2,
        ByUpdateDesc = 3,
        ByActivityStatusAsc = 4,
        ByActivityStatusDesc = 5,
    }

    public enum OrderSortingEnum
    {
        ByCreateAsc = 0,
        ByCreateDesc = 1,
        ByUpdateAsc = 2,
        ByUpdateDesc = 3,
        ByActivityStatusAsc = 4,
        ByActivityStatusDesc = 5,
    }

    public enum ProductSortingEnum
    {
        ByCreateAsc = 0,
        ByCreateDesc = 1,
        ByUpdateAsc = 2,
        ByUpdateDesc = 3,
        ByActivityStatusAsc = 4,
        ByActivityStatusDesc = 5,
    }

    public enum RoleSortingEnum
    {
        ByCreateAsc = 0,
        ByCreateDesc = 1,
        ByUpdateAsc = 2,
        ByUpdateDesc = 3,
        ByActivityStatusAsc = 4,
        ByActivityStatusDesc = 5,
        ByTitleAsc = 6,
        ByTitleDesc = 7,
    }

    public enum UserSortingEnum
    {
        ByCreateAsc = 0,
        ByCreateDesc = 1,
        ByUpdateAsc = 2,
        ByUpdateDesc = 3,
        ByActivityStatusAsc = 4,
        ByActivityStatusDesc = 5,
        ByUserNameAsc = 6,
        ByUserNameDesc = 7,
        ByEmailAsc = 8,
        ByEmailDesc = 9,
        ByPhoneAsc = 10,
        ByPhoneDesc = 11,
        ByFirstNameAsc = 12,
        ByFirstNameDesc = 13,
        BySecondNameAsc = 14,
        BySecondNameDesc = 15,
    }

    public enum SupplierSortingEnum
    {
        ByCreateAsc = 0,
        ByCreateDesc = 1,
        ByUpdateAsc = 2,
        ByUpdateDesc = 3,
        ByActivityStatusAsc = 4,
        ByActivityStatusDesc = 5,
        ByTitleAsc = 6,
        ByTitleDesc = 7,
        ByEmailAsc = 8,
        ByEmailDesc = 9,
        ByExtraEmailAsc = 10,
        ByExtraEmailDesc = 11,
        ByPhoneAsc = 12,
        ByPhoneDesc = 13,
        ByExtraPhoneAsc = 14,
        ByExtraPhoneDesc = 15,
        ByManagerAsc = 16,
        ByManagerDesc = 17,
        ByExtraManagerAsc = 18,
        ByExtraManagerDesc = 19,
        ByAddressAsc = 20,
        ByAddressDesc = 21,
        ByExtraAddressAsc = 22,
        ByExtraAddressDesc = 23,
    }

    #endregion


    public enum TypeModelResponseEnum
    {
        GetFullApiModel = 0,
        GetMinApiModel = 1,
    }

    public enum EntityActivityStatusEnum
    {
        Inactive = 0,
        Active = 1,
        Deleted = 2,
    }

    public enum ContactTypeEnum
    {
        Email = 0,
        Phone = 1,
    }

    public enum FrontClientType
    {
        AdminPanel = 0,
        WebClient = 1,
    }

    public enum FileEntityTypeEnum
    {
        IconImage = 0,
        AvatarImage = 1,
        Image = 2,
        Video = 3,
        Document = 4,
        Unknown = 5,
    }

    public enum VerificationTokenTypeEnum
    {
        Recovery = 0,
        Verification = 1
    }

    public enum LetterTypeEnum
    {
        ContactVerification = 0,
        RecoveryPassword = 1,
        SupportService = 2,
    }

    public enum UserLanguageEnum
    {
        Russian = 1,
        Ukrainian = 2,
        English = 3,
    }

    public enum ProductStatusEnum
    {
        InStock = 0,
        OutOfStock = 1,
        AwaitingDelivery = 2
    }

    public enum OrderStatusEnum
    {
        New = 0,
        InProgress = 1,
        AwaitingChecking = 2,
        AwaitingShipment = 3,
        InDelivery = 4,
        Сompleted = 5,
        Сanceled = 6,
        Return = 7
    }
}
