
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/12/2016 00:46:45
-- Generated from EDMX file: C:\Users\Alexandre\Documents\Visual Studio 2015\Projects\POSBourse-.NET\POSBourse\Entity\bourse.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [bourse];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TransactionSoldProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SoldProductSet] DROP CONSTRAINT [FK_TransactionSoldProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionBuyTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BuyTransactionSet] DROP CONSTRAINT [FK_TransactionBuyTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionCashOutput]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashOutputSet] DROP CONSTRAINT [FK_TransactionCashOutput];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionCashInput]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashInputSet] DROP CONSTRAINT [FK_TransactionCashInput];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionEmittedCoupon]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmittedCouponSet] DROP CONSTRAINT [FK_TransactionEmittedCoupon];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionEnteredCoupon]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnteredCouponSet1] DROP CONSTRAINT [FK_TransactionEnteredCoupon];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionEnteredDirectExchange]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnteredDirectExchangeSet1] DROP CONSTRAINT [FK_TransactionEnteredDirectExchange];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionEnteredDiscount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnteredDiscountSet] DROP CONSTRAINT [FK_TransactionEnteredDiscount];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSet];
GO
IF OBJECT_ID(N'[dbo].[SoldProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SoldProductSet];
GO
IF OBJECT_ID(N'[dbo].[TransactionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionSet];
GO
IF OBJECT_ID(N'[dbo].[BuyTransactionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BuyTransactionSet];
GO
IF OBJECT_ID(N'[dbo].[EmittedCouponSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmittedCouponSet];
GO
IF OBJECT_ID(N'[dbo].[EnteredCouponSet1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnteredCouponSet1];
GO
IF OBJECT_ID(N'[dbo].[EnteredDirectExchangeSet1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnteredDirectExchangeSet1];
GO
IF OBJECT_ID(N'[dbo].[EnteredDiscountSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnteredDiscountSet];
GO
IF OBJECT_ID(N'[dbo].[CashInputSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashInputSet];
GO
IF OBJECT_ID(N'[dbo].[CashOutputSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashOutputSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [code] nvarchar(max)  NOT NULL,
    [title] nvarchar(max)  NOT NULL,
    [author] nvarchar(max)  NOT NULL,
    [editor] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SoldProductSet'
CREATE TABLE [dbo].[SoldProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [datetime] datetime  NOT NULL,
    [price] decimal(18,0)  NOT NULL,
    [TransactionId] int  NOT NULL,
    [inStock] bit  NOT NULL,
    [code] nvarchar(max)  NOT NULL,
    [title] nvarchar(max)  NOT NULL,
    [author] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [editor] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TransactionSet'
CREATE TABLE [dbo].[TransactionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [datetime] datetime  NOT NULL,
    [giftCouponValue] decimal(18,0)  NOT NULL,
    [couponValue] decimal(18,0)  NOT NULL,
    [usedCouponExchangeValue] decimal(18,0)  NOT NULL,
    [exchangeValue] decimal(18,0)  NOT NULL,
    [convertedCouponExchangeValue] decimal(18,0)  NOT NULL,
    [discountOfferValue] decimal(18,0)  NOT NULL,
    [directExchangeValue] decimal(18,0)  NOT NULL,
    [paycardValue] decimal(18,0)  NOT NULL,
    [cashValue] decimal(18,0)  NOT NULL,
    [realCashValue] decimal(18,0)  NOT NULL,
    [emittedCouponValue] decimal(18,0)  NOT NULL,
    [emittedCashValue] decimal(18,0)  NOT NULL,
    [realEmittedCashValue] decimal(18,0)  NOT NULL,
    [totalSalesValue] decimal(18,0)  NOT NULL,
    [totalBuyValue] decimal(18,0)  NOT NULL,
    [productCount] bigint  NOT NULL,
    [store] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BuyTransactionSet'
CREATE TABLE [dbo].[BuyTransactionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [datetime] datetime  NOT NULL,
    [productType] nvarchar(max)  NOT NULL,
    [Transaction_Id] int  NOT NULL
);
GO

-- Creating table 'EmittedCouponSet'
CREATE TABLE [dbo].[EmittedCouponSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [couponID] nvarchar(max)  NOT NULL,
    [onlyOn] nvarchar(max)  NOT NULL,
    [transactionSpecificity] nvarchar(max)  NOT NULL,
    [policeBookID] nvarchar(max)  NOT NULL,
    [value] nvarchar(max)  NOT NULL,
    [datetime] datetime  NOT NULL,
    [TransactionId] int  NOT NULL
);
GO

-- Creating table 'EnteredCouponSet1'
CREATE TABLE [dbo].[EnteredCouponSet1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [couponID] nvarchar(max)  NOT NULL,
    [value] nvarchar(max)  NOT NULL,
    [store] nvarchar(max)  NOT NULL,
    [datetime] datetime  NOT NULL,
    [couponType] nvarchar(max)  NOT NULL,
    [TransactionId] int  NOT NULL,
    [exchange] bit  NOT NULL
);
GO

-- Creating table 'EnteredDirectExchangeSet1'
CREATE TABLE [dbo].[EnteredDirectExchangeSet1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [datetime] nvarchar(max)  NOT NULL,
    [clientName] nvarchar(max)  NOT NULL,
    [TransactionId] int  NOT NULL
);
GO

-- Creating table 'EnteredDiscountSet'
CREATE TABLE [dbo].[EnteredDiscountSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [discountType] nvarchar(max)  NOT NULL,
    [discountValue] decimal(18,0)  NOT NULL,
    [clientName] nvarchar(max)  NOT NULL,
    [datetime] datetime  NOT NULL,
    [TransactionId] int  NOT NULL
);
GO

-- Creating table 'CashInputSet'
CREATE TABLE [dbo].[CashInputSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [reason] nvarchar(max)  NOT NULL,
    [value] nvarchar(max)  NOT NULL,
    [datetime] datetime  NOT NULL,
    [Transaction_Id] int  NOT NULL
);
GO

-- Creating table 'CashOutputSet'
CREATE TABLE [dbo].[CashOutputSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [reason] nvarchar(max)  NOT NULL,
    [value] nvarchar(max)  NOT NULL,
    [datetime] datetime  NOT NULL,
    [Transaction_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SoldProductSet'
ALTER TABLE [dbo].[SoldProductSet]
ADD CONSTRAINT [PK_SoldProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TransactionSet'
ALTER TABLE [dbo].[TransactionSet]
ADD CONSTRAINT [PK_TransactionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BuyTransactionSet'
ALTER TABLE [dbo].[BuyTransactionSet]
ADD CONSTRAINT [PK_BuyTransactionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmittedCouponSet'
ALTER TABLE [dbo].[EmittedCouponSet]
ADD CONSTRAINT [PK_EmittedCouponSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EnteredCouponSet1'
ALTER TABLE [dbo].[EnteredCouponSet1]
ADD CONSTRAINT [PK_EnteredCouponSet1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EnteredDirectExchangeSet1'
ALTER TABLE [dbo].[EnteredDirectExchangeSet1]
ADD CONSTRAINT [PK_EnteredDirectExchangeSet1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EnteredDiscountSet'
ALTER TABLE [dbo].[EnteredDiscountSet]
ADD CONSTRAINT [PK_EnteredDiscountSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CashInputSet'
ALTER TABLE [dbo].[CashInputSet]
ADD CONSTRAINT [PK_CashInputSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CashOutputSet'
ALTER TABLE [dbo].[CashOutputSet]
ADD CONSTRAINT [PK_CashOutputSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TransactionId] in table 'SoldProductSet'
ALTER TABLE [dbo].[SoldProductSet]
ADD CONSTRAINT [FK_TransactionSoldProduct]
    FOREIGN KEY ([TransactionId])
    REFERENCES [dbo].[TransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionSoldProduct'
CREATE INDEX [IX_FK_TransactionSoldProduct]
ON [dbo].[SoldProductSet]
    ([TransactionId]);
GO

-- Creating foreign key on [Transaction_Id] in table 'BuyTransactionSet'
ALTER TABLE [dbo].[BuyTransactionSet]
ADD CONSTRAINT [FK_TransactionBuyTransaction]
    FOREIGN KEY ([Transaction_Id])
    REFERENCES [dbo].[TransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionBuyTransaction'
CREATE INDEX [IX_FK_TransactionBuyTransaction]
ON [dbo].[BuyTransactionSet]
    ([Transaction_Id]);
GO

-- Creating foreign key on [Transaction_Id] in table 'CashOutputSet'
ALTER TABLE [dbo].[CashOutputSet]
ADD CONSTRAINT [FK_TransactionCashOutput]
    FOREIGN KEY ([Transaction_Id])
    REFERENCES [dbo].[TransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionCashOutput'
CREATE INDEX [IX_FK_TransactionCashOutput]
ON [dbo].[CashOutputSet]
    ([Transaction_Id]);
GO

-- Creating foreign key on [Transaction_Id] in table 'CashInputSet'
ALTER TABLE [dbo].[CashInputSet]
ADD CONSTRAINT [FK_TransactionCashInput]
    FOREIGN KEY ([Transaction_Id])
    REFERENCES [dbo].[TransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionCashInput'
CREATE INDEX [IX_FK_TransactionCashInput]
ON [dbo].[CashInputSet]
    ([Transaction_Id]);
GO

-- Creating foreign key on [TransactionId] in table 'EmittedCouponSet'
ALTER TABLE [dbo].[EmittedCouponSet]
ADD CONSTRAINT [FK_TransactionEmittedCoupon]
    FOREIGN KEY ([TransactionId])
    REFERENCES [dbo].[TransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionEmittedCoupon'
CREATE INDEX [IX_FK_TransactionEmittedCoupon]
ON [dbo].[EmittedCouponSet]
    ([TransactionId]);
GO

-- Creating foreign key on [TransactionId] in table 'EnteredCouponSet1'
ALTER TABLE [dbo].[EnteredCouponSet1]
ADD CONSTRAINT [FK_TransactionEnteredCoupon]
    FOREIGN KEY ([TransactionId])
    REFERENCES [dbo].[TransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionEnteredCoupon'
CREATE INDEX [IX_FK_TransactionEnteredCoupon]
ON [dbo].[EnteredCouponSet1]
    ([TransactionId]);
GO

-- Creating foreign key on [TransactionId] in table 'EnteredDirectExchangeSet1'
ALTER TABLE [dbo].[EnteredDirectExchangeSet1]
ADD CONSTRAINT [FK_TransactionEnteredDirectExchange]
    FOREIGN KEY ([TransactionId])
    REFERENCES [dbo].[TransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionEnteredDirectExchange'
CREATE INDEX [IX_FK_TransactionEnteredDirectExchange]
ON [dbo].[EnteredDirectExchangeSet1]
    ([TransactionId]);
GO

-- Creating foreign key on [TransactionId] in table 'EnteredDiscountSet'
ALTER TABLE [dbo].[EnteredDiscountSet]
ADD CONSTRAINT [FK_TransactionEnteredDiscount]
    FOREIGN KEY ([TransactionId])
    REFERENCES [dbo].[TransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionEnteredDiscount'
CREATE INDEX [IX_FK_TransactionEnteredDiscount]
ON [dbo].[EnteredDiscountSet]
    ([TransactionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------