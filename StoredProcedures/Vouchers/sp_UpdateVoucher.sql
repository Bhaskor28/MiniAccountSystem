CREATE PROCEDURE sp_UpdateVoucher
    @VoucherId INT,
    @VoucherType NVARCHAR(50),
    @VoucherDate DATE,
    @ReferenceNo NVARCHAR(100)
AS
BEGIN
    UPDATE Vouchers
    SET VoucherType = @VoucherType,
        VoucherDate = @VoucherDate,
        ReferenceNo = @ReferenceNo
    WHERE VoucherId = @VoucherId
END
