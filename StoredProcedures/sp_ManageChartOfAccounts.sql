CREATE PROCEDURE sp_ManageChartOfAccounts
    @Action NVARCHAR(10),
    @AccountId INT = NULL,
    @AccountName NVARCHAR(100) = NULL,
    @ParentAccountId INT = NULL,
    @AccountType NVARCHAR(50) = NULL
AS
BEGIN
    IF @Action = 'INSERT'
    BEGIN
        INSERT INTO ChartOfAccounts (AccountName, ParentAccountId, AccountType)
        VALUES (@AccountName, @ParentAccountId, @AccountType)
    END

    ELSE IF @Action = 'UPDATE'
    BEGIN
        UPDATE ChartOfAccounts
        SET AccountName = @AccountName,
            ParentAccountId = @ParentAccountId,
            AccountType = @AccountType
        WHERE AccountId = @AccountId
    END

    ELSE IF @Action = 'DELETE'
    BEGIN
        DELETE FROM ChartOfAccounts WHERE AccountId = @AccountId
    END

    ELSE IF @Action = 'SELECT_ALL'
    BEGIN
        SELECT * FROM ChartOfAccounts
    END

    ELSE IF @Action = 'SELECT_BY_ID'
    BEGIN
        SELECT * FROM ChartOfAccounts WHERE AccountId = @AccountId
    END
END
