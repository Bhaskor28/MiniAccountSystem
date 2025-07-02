CREATE PROCEDURE sp_GetVoucherById
    @VoucherId INT
AS
BEGIN
    SELECT 
        VoucherId, 
        VoucherType, 
        VoucherDate, 
        ReferenceNo
    FROM 
        Vouchers
    WHERE 
        VoucherId = @VoucherId
END
