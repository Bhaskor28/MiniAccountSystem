CREATE PROCEDURE sp_HasAccess
    @RoleName NVARCHAR(50),
    @ModuleName NVARCHAR(50)
AS
BEGIN
    SELECT CASE 
        WHEN EXISTS (
            SELECT 1 
            FROM RoleModuleAccess 
            WHERE RoleName = @RoleName AND ModuleName = @ModuleName
        )
        THEN 1 ELSE 0 
    END AS HasAccess
END
