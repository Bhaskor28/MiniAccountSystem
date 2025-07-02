CREATE PROCEDURE sp_GetAllVouchers
AS
BEGIN
    SELECT VoucherId, VoucherType, VoucherDate, ReferenceNo
    FROM Vouchers
    ORDER BY VoucherDate DESC
END
