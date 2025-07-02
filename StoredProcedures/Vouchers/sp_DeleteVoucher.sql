CREATE PROCEDURE sp_DeleteVoucher
    @VoucherId INT
AS
BEGIN
    DELETE FROM VoucherEntries WHERE VoucherId = @VoucherId
    DELETE FROM Vouchers WHERE VoucherId = @VoucherId
END
