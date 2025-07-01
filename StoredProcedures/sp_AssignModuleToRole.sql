CREATE PROCEDURE sp_AssignModuleToRole
    @RoleName NVARCHAR(50),
    @ModuleName NVARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM RoleModules WHERE RoleName = @RoleName AND ModuleName = @ModuleName
    )
    BEGIN
        INSERT INTO RoleModules (RoleName, ModuleName)
        VALUES (@RoleName, @ModuleName)
    END
END
