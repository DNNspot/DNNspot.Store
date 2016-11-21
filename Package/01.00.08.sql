/*--------------------------
  New Tables/Views
----------------------------*/
BEGIN TRANSACTION
SET NUMERIC_ROUNDABORT OFF

SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON

SET XACT_ABORT ON

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

PRINT N'Creating [dbo].[DNNspot_Store_ShippingService]'

CREATE TABLE [dbo].[DNNspot_Store_ShippingService]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[StoreId] [int] NOT NULL,
[ShippingProviderType] [smallint] NOT NULL
)

PRINT N'Creating primary key [PK_DNNspot_Store_ShippingService] on [dbo].[DNNspot_Store_ShippingService]'

ALTER TABLE [dbo].[DNNspot_Store_ShippingService] ADD CONSTRAINT [PK_DNNspot_Store_ShippingService] PRIMARY KEY CLUSTERED  ([Id])

PRINT N'Creating [dbo].[DNNspot_Store_ShippingServiceSetting]'

CREATE TABLE [dbo].[DNNspot_Store_ShippingServiceSetting]
(
[ShippingServiceId] [int] NOT NULL,
[Name] [varchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Value] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)

PRINT N'Creating primary key [PK_DNNspot_Store_ShippingServiceSetting] on [dbo].[DNNspot_Store_ShippingServiceSetting]'

ALTER TABLE [dbo].[DNNspot_Store_ShippingServiceSetting] ADD CONSTRAINT [PK_DNNspot_Store_ShippingServiceSetting] PRIMARY KEY CLUSTERED  ([ShippingServiceId], [Name])

PRINT N'Creating [dbo].[DNNspot_Store_ShippingServiceRateType]'

CREATE TABLE [dbo].[DNNspot_Store_ShippingServiceRateType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ShippingServiceId] [int] NOT NULL,
[Name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[DisplayName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsEnabled] [bit] NOT NULL CONSTRAINT [DF_DNNspot_Store_ShippingProviderOption_IsEnabled] DEFAULT ((1)),
[SortOrder] [smallint] NOT NULL CONSTRAINT [DF_DNNspot_Store_ShippingProviderOption_SortOrder] DEFAULT ((99))
)

PRINT N'Creating primary key [PK_DNNspot_Store_ShippingProviderRateType] on [dbo].[DNNspot_Store_ShippingServiceRateType]'

ALTER TABLE [dbo].[DNNspot_Store_ShippingServiceRateType] ADD CONSTRAINT [PK_DNNspot_Store_ShippingProviderRateType] PRIMARY KEY CLUSTERED  ([Id])

PRINT N'Creating [dbo].[DNNspot_Store_ShippingServiceRate]'

CREATE TABLE [dbo].[DNNspot_Store_ShippingServiceRate]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[RateTypeId] [int] NOT NULL,
[CountryCode] [nvarchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_DNNspot_Store_ShippingProviderRate_CountryCode] DEFAULT (''),
[Region] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_DNNspot_Store_ShippingProviderRate_Region] DEFAULT (''),
[WeightMin] [decimal] (14, 4) NULL,
[WeightMax] [decimal] (14, 4) NULL,
[Cost] [money] NOT NULL
)

PRINT N'Creating primary key [PK_DNNspot_Store_ShippingProviderRate] on [dbo].[DNNspot_Store_ShippingServiceRate]'

ALTER TABLE [dbo].[DNNspot_Store_ShippingServiceRate] ADD CONSTRAINT [PK_DNNspot_Store_ShippingProviderRate] PRIMARY KEY CLUSTERED  ([Id])

PRINT N'Adding constraints to [dbo].[DNNspot_Store_ShippingService]'

ALTER TABLE [dbo].[DNNspot_Store_ShippingService] ADD CONSTRAINT [IX_DNNspot_Store_ShippingService] UNIQUE NONCLUSTERED  ([StoreId], [ShippingProviderType])

PRINT N'Adding foreign keys to [dbo].[DNNspot_Store_ShippingServiceRateType]'

ALTER TABLE [dbo].[DNNspot_Store_ShippingServiceRateType] ADD
CONSTRAINT [FK_DNNspot_Store_ShippingServiceRateType_DNNspot_Store_ShippingService] FOREIGN KEY ([ShippingServiceId]) REFERENCES [dbo].[DNNspot_Store_ShippingService] ([Id]) ON DELETE CASCADE

PRINT N'Adding foreign keys to [dbo].[DNNspot_Store_ShippingServiceSetting]'

ALTER TABLE [dbo].[DNNspot_Store_ShippingServiceSetting] ADD
CONSTRAINT [FK_DNNspot_Store_ShippingServiceSetting_DNNspot_Store_ShippingService] FOREIGN KEY ([ShippingServiceId]) REFERENCES [dbo].[DNNspot_Store_ShippingService] ([Id]) ON DELETE CASCADE

PRINT N'Adding foreign keys to [dbo].[DNNspot_Store_ShippingService]'

ALTER TABLE [dbo].[DNNspot_Store_ShippingService] ADD
CONSTRAINT [FK_DNNspot_Store_ShippingService_DNNspot_Store_Store] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[DNNspot_Store_Store] ([Id]) ON DELETE CASCADE

PRINT N'Adding foreign keys to [dbo].[DNNspot_Store_ShippingServiceRate]'

ALTER TABLE [dbo].[DNNspot_Store_ShippingServiceRate] ADD
CONSTRAINT [FK_DNNspot_Store_ShippingServiceRate_DNNspot_Store_ShippingServiceRateType] FOREIGN KEY ([RateTypeId]) REFERENCES [dbo].[DNNspot_Store_ShippingServiceRateType] ([Id]) ON DELETE CASCADE

COMMIT TRANSACTION


PRINT N'Adding view ''vDNNspot_Store_ShippingServiceRates'''
GO
CREATE VIEW [dbo].[vDNNspot_Store_ShippingServiceRates]
AS
SELECT
rate.Id as RateId
,rate.CountryCode as CountryCode
,rate.Region as Region
,rate.WeightMin as WeightMin
,rate.WeightMax as WeightMax
,rate.Cost as Cost
,rateType.Id as RateTypeId
,rateType.Name as RateTypeName
,rateType.DisplayName as RateTypeDisplayName
,rateType.IsEnabled as RateTypeIsEnabled
,rateType.SortOrder as RateTypeSortOrder
FROM DNNspot_Store_ShippingServiceRateType rateType
INNER JOIN DNNspot_Store_ShippingServiceRate rate ON rate.RateTypeId = rateType.Id
GO

/*--------------------------
  ALTER Tables / New Columns
----------------------------*/
BEGIN TRANSACTION
/* Add constraint to Store_Catery Table */
SET NUMERIC_ROUNDABORT OFF

SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON

SET XACT_ABORT ON

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

PRINT N'Adding constraints to [dbo].[DNNspot_Store_Category]'

ALTER TABLE [dbo].[DNNspot_Store_Category] ADD CONSTRAINT [IX_DNNspot_Store_Category_1] UNIQUE NONCLUSTERED  ([StoreId], [Name])

PRINT 'Rename ''AppliesToShippingIds'' Coupon column'
SET NUMERIC_ROUNDABORT OFF

SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON

SET XACT_ABORT ON

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

PRINT N'Altering [dbo].[DNNspot_Store_Coupon]'

EXEC sp_rename N'[dbo].[DNNspot_Store_Coupon].[AppliesToShippingIds]', N'AppliesToShippingRateTypes', 'COLUMN'

PRINT 'Rename ''ShippingOption'' column and change type'
EXEC sp_rename N'[dbo].[DNNspot_Store_Order].[ShippingOption]', N'ShippingServiceOption', 'COLUMN'

ALTER TABLE [dbo].[DNNspot_Store_Order] ALTER COLUMN ShippingServiceOption NVARCHAR(100)

PRINT 'Rename ''ShippingCost'' column'
EXEC sp_rename N'[dbo].[DNNspot_Store_Order].[ShippingCost]', N'ShippingAmount', 'COLUMN'

PRINT 'Add new ShippingServiceXXX Columns'
ALTER TABLE [dbo].[DNNspot_Store_Order]
ADD
[ShippingServiceProvider] nvarchar(200) NOT NULL default('')
,[ShippingServicePrice] money NULL
,[ShippingServiceTrackingNumber] nvarchar(200) NOT NULL default('')
,[ShippingServiceLabelFile] nvarchar(300) NOT NULL default('')

PRINT 'Drop PaymentProvider.IsEnabled column'
ALTER TABLE [dbo].[DNNspot_Store_StorePaymentProvider] DROP CONSTRAINT DF_DNNspot_Store_StorePaymentProcessor_IsEnabled
ALTER TABLE [dbo].[DNNspot_Store_StorePaymentProvider] DROP COLUMN [IsEnabled]

COMMIT TRANSACTION

/*----------------------------------------------
  Migrate data from old -> new tables/columns
-----------------------------------------------*/
BEGIN TRANSACTION
PRINT 'Create ''CustomShipping'' service for each store'
INSERT INTO DNNspot_Store_ShippingService(StoreId, ShippingProviderType)
	SELECT Id, 1 FROM DNNspot_Store_Store

PRINT 'Enable the ''CustomShipping'' service for each store'
INSERT INTO DNNspot_Store_ShippingServiceSetting(ShippingServiceId, Name, [Value])
	SELECT Id, 'IsEnabled', 'True' FROM DNNspot_Store_ShippingService	
	
PRINT 'Migrate ''ShippingMethod'' rows to ''ShippingServiceRateType'' table'
INSERT INTO DNNSpot_Store_ShippingServiceRateType(ShippingServiceId, Name, DisplayName, IsEnabled, SortOrder)
	SELECT shipService.Id, Name, Name as DisplayName, 1 as IsEnabled, SortOrder
	FROM DNNspot_Store_ShippingMethod old
	INNER JOIN DNNspot_Store_ShippingService shipService ON old.StoreId = shipService.StoreId

PRINT 'Migrate old ''ShippingRate_Weight'' rows to ''ShippingServiceRate'' table'
INSERT INTO DNNSpot_Store_ShippingServiceRate(RateTypeID,CountryCode,Region,WeightMin,WeightMax,Cost)
	SELECT
		rateTypes.Id		
		,oldRates.CountryCode
		,oldRates.Region
		,oldRates.MinRange as WeightMin
		,oldRates.MaxRange as WeightMax
		,oldRates.ShippingCost as Cost
	FROM DNNspot_Store_ShippingRate_Weight oldRates
	INNER JOIN DNNspot_Store_ShippingMethod oldMethods ON oldMethods.Id = oldRates.ShippingMethodId
	INNER JOIN DNNspot_Store_ShippingService shipService ON oldMethods.StoreId = shipService.StoreId
	INNER JOIN DNNSpot_Store_ShippingServiceRateType rateTypes ON (rateTypes.ShippingServiceId = shipService.Id AND rateTypes.Name = oldMethods.Name)

COMMIT

/*-----------------------------------------------
	New Data
------------------------------------------------*/
PRINT 'Insert ''Deleted'' Order Status if it doesn''t exist'
IF NOT EXISTS(SELECT * FROM [DNNspot_Store_OrderStatus] WHERE Id = 98)
BEGIN
  INSERT INTO [DNNspot_Store_OrderStatus](Id, [Name]) VALUES(98,'Deleted')
END

/*----------------------------------------------
  DROP old Tables/Columns
-----------------------------------------------*/
PRINT 'DROP old tables'
BEGIN TRANSACTION

/****** Object:  ForeignKey [FK_DNNspot_Store_Shippinption_DNNspot_Store_Store]    Script Date: 05/10/2010 23:23:22 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DNNspot_Store_Shippinption_DNNspot_Store_Store]') AND parent_object_id = OBJECT_ID(N'[dbo].[DNNspot_Store_Shipping_ProductWeightOption]'))
ALTER TABLE [dbo].[DNNspot_Store_Shipping_ProductWeightOption] DROP CONSTRAINT [FK_DNNspot_Store_Shippinption_DNNspot_Store_Store]

/****** Object:  ForeignKey [FK_DNNspot_Store_ShippingRate_Weight_DNNspot_Store_ShippingMethod]    Script Date: 05/10/2010 23:23:22 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DNNspot_Store_ShippingRate_Weight_DNNspot_Store_ShippingMethod]') AND parent_object_id = OBJECT_ID(N'[dbo].[DNNspot_Store_Shipping_ProductWeightRate]'))
ALTER TABLE [dbo].[DNNspot_Store_Shipping_ProductWeightRate] DROP CONSTRAINT [FK_DNNspot_Store_ShippingRate_Weight_DNNspot_Store_ShippingMethod]

/****** Object:  Table [dbo].[DNNspot_Store_Shipping_ProductWeightRate]    Script Date: 05/10/2010 23:23:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DNNspot_Store_Shipping_ProductWeightRate]') AND type in (N'U'))
DROP TABLE [dbo].[DNNspot_Store_Shipping_ProductWeightRate]

/****** Object:  Table [dbo].[DNNspot_Store_Shipping_ProductWeightOption]    Script Date: 05/10/2010 23:23:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DNNspot_Store_Shipping_ProductWeightOption]') AND type in (N'U'))
DROP TABLE [dbo].[DNNspot_Store_Shipping_ProductWeightOption]


/****** Object:  ForeignKey [FK_DNNspot_Store_Shippinption_DNNspot_Store_Store]    Script Date: 05/10/2010 23:25:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DNNspot_Store_Shippinption_DNNspot_Store_Store]') AND parent_object_id = OBJECT_ID(N'[dbo].[DNNspot_Store_ShippingMethod]'))
ALTER TABLE [dbo].[DNNspot_Store_ShippingMethod] DROP CONSTRAINT [FK_DNNspot_Store_Shippinption_DNNspot_Store_Store]

/****** Object:  ForeignKey [FK_DNNspot_Store_ShippingRate_Weight_DNNspot_Store_ShippingMethod]    Script Date: 05/10/2010 23:25:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DNNspot_Store_ShippingRate_Weight_DNNspot_Store_ShippingMethod]') AND parent_object_id = OBJECT_ID(N'[dbo].[DNNspot_Store_ShippingRate_Weight]'))
ALTER TABLE [dbo].[DNNspot_Store_ShippingRate_Weight] DROP CONSTRAINT [FK_DNNspot_Store_ShippingRate_Weight_DNNspot_Store_ShippingMethod]

/****** Object:  Table [dbo].[DNNspot_Store_ShippingRate_Weight]    Script Date: 05/10/2010 23:25:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DNNspot_Store_ShippingRate_Weight]') AND type in (N'U'))
DROP TABLE [dbo].[DNNspot_Store_ShippingRate_Weight]

/****** Object:  Table [dbo].[DNNspot_Store_ShippingMethod]    Script Date: 05/10/2010 23:25:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DNNspot_Store_ShippingMethod]') AND type in (N'U'))
DROP TABLE [dbo].[DNNspot_Store_ShippingMethod]

/****** Object:  View [dbo].[vDNNspot_Store_ShippingRateWeight]    Script Date: 05/10/2010 23:25:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vDNNspot_Store_ShippingRateWeight]') AND type in (N'V'))
DROP VIEW [dbo].vDNNspot_Store_ShippingRateWeight
GO

COMMIT


