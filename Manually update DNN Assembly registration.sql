declare @adminVersion varchar(50)
declare @storeVersion varchar(50)

SELECT @adminVersion = [Version] FROM [DesktopModules] WHERE ModuleName = 'DNNspot-Store-Admin'
SELECT @storeVersion = [Version] FROM [DesktopModules] WHERE ModuleName = 'DNNspot-Store'

IF @storeVersion <> @adminVersion
BEGIN
	PRINT 'Store version mismatch! Admin: ' + @adminVersion + ', Store: ' + @storeVersion
	
	declare @lastDotIndex int = LEN(@adminVersion) - (CHARINDEX('.',REVERSE(@adminVersion)) - 2)
	declare @adminMinorVersion varchar(50) = SUBSTRING(@adminVersion, @lastDotIndex, 2)
	PRINT @adminMinorVersion
	
	set @lastDotIndex = LEN(@storeVersion) - (CHARINDEX('.',REVERSE(@storeVersion)) - 2)
	declare @storeMinorVersion varchar(50) = SUBSTRING(@storeVersion, @lastDotIndex, 2)
	PRINT @storeMinorVersion
	
	IF CAST(@adminMinorVersion as int) > CAST(@storeMinorVersion as int)
	BEGIN
		--PRINT 'update'	
		--UPDATE [DesktopModules] SET [Version] = @adminVersion WHERE ModuleName = 'DNNspot-Store'
		--UPDATE [Assemblies] SET [Version] = @adminVersion WHERE AssemblyName = 'DNNspot.Store.dll'
		PRINT ''
	END
END