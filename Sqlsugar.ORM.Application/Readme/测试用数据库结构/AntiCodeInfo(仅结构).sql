/*
 Navicat Premium Data Transfer

 Source Server         : AWS_DB
 Source Server Type    : SQL Server
 Source Server Version : 13005492
 Source Host           : 127.0.0.1:1433
 Source Catalog        : HoriProductQuery
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 13005492
 File Encoding         : 65001

 Date: 20/01/2020 21:29:47
*/


-- ----------------------------
-- Table structure for AntiCodeInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AntiCodeInfo]') AND type IN ('U'))
	DROP TABLE [dbo].[AntiCodeInfo]
GO

CREATE TABLE [dbo].[AntiCodeInfo] (
  [AntiCode] varchar(10) COLLATE Chinese_PRC_CI_AS DEFAULT ((0)) NOT NULL,
  [CheckCode] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ProductType] varchar(3) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Status] varchar(2) COLLATE Chinese_PRC_CI_AS  NULL,
  [QueryTimes] int  NULL,
  [LastQueryDate] datetime  NULL,
  [PrintingState] varchar(2) COLLATE Chinese_PRC_CI_AS DEFAULT ((0)) NULL
)
GO

ALTER TABLE [dbo].[AntiCodeInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'0-为查询;1-已查询',
'SCHEMA', N'dbo',
'TABLE', N'AntiCodeInfo',
'COLUMN', N'Status'
GO

EXEC sp_addextendedproperty
'MS_Description', N'印刷状态(0-未印刷，1-已印刷)',
'SCHEMA', N'dbo',
'TABLE', N'AntiCodeInfo',
'COLUMN', N'PrintingState'
GO


-- ----------------------------
-- Primary Key structure for table AntiCodeInfo
-- ----------------------------
ALTER TABLE [dbo].[AntiCodeInfo] ADD CONSTRAINT [PK_AntiCodeInfo] PRIMARY KEY CLUSTERED ([AntiCode])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

