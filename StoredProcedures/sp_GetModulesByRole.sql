
CREATE PROCEDURE sp_GetModulesByRole
    @RoleName NVARCHAR(50)
AS
BEGIN
    SELECT ModuleName
    FROM RoleModules
    WHERE RoleName = @RoleName
END
