CREATE PROCEDURE sp_SaveVoucher
    @VoucherType NVARCHAR(50),
    @VoucherDate DATE,
    @ReferenceNo NVARCHAR(100),
    @Entries TVP_VoucherEntry READONLY 
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Vouchers (VoucherType, VoucherDate, ReferenceNo)
    VALUES (@VoucherType, @VoucherDate, @ReferenceNo)

    DECLARE @NewVoucherId INT = SCOPE_IDENTITY();

    INSERT INTO VoucherEntries (VoucherId, AccountId, Amount, EntryType)
    SELECT @NewVoucherId, AccountId, Amount, EntryType
    FROM @Entries
END
