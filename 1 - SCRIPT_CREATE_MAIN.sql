-- =============================================================================================================================
-- Script to create tables and stored procedure to classify trades based on a rule criteria
--
-- Table [TRADE_RISK_RULES] will be initialized with predefined risks rules for LOWRISK, MEDIUMRISK and HIGHRISK
-- Table [TRADES] will be initialized with the portifolio of trades predefined.
-- Table [TRADES_RISK_CATEGORIZED] will be the output table after the sp_TRADES_TradeRiskCategorize storage procedure runs 
--
-- This concept aims to implement a rules engine to classify trades based on rules. 
-- Rules can be added, modified, deleted as needed
-- =============================================================================================================================


-- USE [taskriskdb]
GO
/****** Object:  StoredProcedure [dbo].[sp_TRADES_TradeRiskCategorize]    Script Date: 27/05/2022 11:18:45 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_TRADES_TradeRiskCategorize]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TRADES]') AND type in (N'U'))
ALTER TABLE [dbo].[TRADES] DROP CONSTRAINT IF EXISTS [DF_TRADES_TRADE_ID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TRADE_RISK_RULES]') AND type in (N'U'))
ALTER TABLE [dbo].[TRADE_RISK_RULES] DROP CONSTRAINT IF EXISTS [DF_TRADE_RISK_RULES_RULE_ID]
GO
/****** Object:  Table [dbo].[TRADES_RISK_CATEGORIZED]    Script Date: 27/05/2022 11:18:45 ******/
DROP TABLE IF EXISTS [dbo].[TRADES_RISK_CATEGORIZED]
GO
/****** Object:  Table [dbo].[TRADES]    Script Date: 27/05/2022 11:18:45 ******/
DROP TABLE IF EXISTS [dbo].[TRADES]
GO
/****** Object:  Table [dbo].[TRADE_RISK_RULES]    Script Date: 27/05/2022 11:18:45 ******/
DROP TABLE IF EXISTS [dbo].[TRADE_RISK_RULES]
GO
/****** Object:  Table [dbo].[TRADE_RISK_RULES]    Script Date: 27/05/2022 11:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRADE_RISK_RULES](
	[RULE_ID] [uniqueidentifier] NOT NULL,
	[RULE_DESCRIPTION] [varchar](max) NULL,
	[RULE_RISK] [varchar](max) NULL,
	[RULE_CONDITION] [varchar](max) NULL,
 CONSTRAINT [PK_TRADE_RISK_RULES] PRIMARY KEY CLUSTERED 
(
	[RULE_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TRADES]    Script Date: 27/05/2022 11:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRADES](
	[TRADE_ID] [uniqueidentifier] NOT NULL,
	[VALUE] [money] NOT NULL,
	[CLIENT_SECTOR] [varchar](max) NOT NULL,
	[TRADE_TYPE] [varchar](max) NULL,
 CONSTRAINT [TRADES_PK] PRIMARY KEY CLUSTERED 
(
	[TRADE_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TRADES] ADD  CONSTRAINT [DF_TRADES_TRADE_ID]  DEFAULT (newid()) FOR [TRADE_ID]
GO
/****** Object:  Table [dbo].[TRADES_RISK_CATEGORIZED]    Script Date: 27/05/2022 11:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRADES_RISK_CATEGORIZED](
	[TRADE_ID] [uniqueidentifier] NOT NULL,
	[RULE_ID] [uniqueidentifier] NULL,
	[TRADE_RISK] [varchar](max) NOT NULL,
 CONSTRAINT [TRADES_RISK_CATEGORIZED_PK] PRIMARY KEY CLUSTERED 
(
	[TRADE_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TRADE_RISK_RULES] ADD  CONSTRAINT [DF_TRADE_RISK_RULES_RULE_ID]  DEFAULT (newid()) FOR [RULE_ID]
GO

-- Initializing predefined rules and trades
INSERT INTO [dbo].[TRADE_RISK_RULES] ([RULE_DESCRIPTION], [RULE_RISK], [RULE_CONDITION]) VALUES (N'Medium risk rule', N'MEDIUMRISK', N'VALUE > 1000000 AND CLIENT_SECTOR = ''Public''')
INSERT INTO [dbo].[TRADE_RISK_RULES] ([RULE_DESCRIPTION], [RULE_RISK], [RULE_CONDITION]) VALUES (N'Low risk rule', N'LOWRISK', N'VALUE < 1000000 AND CLIENT_SECTOR = ''Public''')
INSERT INTO [dbo].[TRADE_RISK_RULES] ([RULE_DESCRIPTION], [RULE_RISK], [RULE_CONDITION]) VALUES (N'High risk rule', N'HIGHRISK', N'VALUE > 1000000 AND CLIENT_SECTOR = ''Private''')
INSERT INTO [dbo].[TRADES] ([VALUE], [CLIENT_SECTOR], [TRADE_TYPE]) VALUES (3000000.0000, N'Public', N'Trade4')
INSERT INTO [dbo].[TRADES] ([VALUE], [CLIENT_SECTOR], [TRADE_TYPE]) VALUES (500000.0000, N'Public', N'Trade3')
INSERT INTO [dbo].[TRADES] ([VALUE], [CLIENT_SECTOR], [TRADE_TYPE]) VALUES (400000.0000, N'Public', N'Trade2')
INSERT INTO  [dbo].[TRADES] ([VALUE], [CLIENT_SECTOR], [TRADE_TYPE]) VALUES (2000000.0000, N'Private', N'Trade1')

GO

/****** Object:  StoredProcedure [dbo].[sp_TRADES_TradeRiskCategorize]    Script Date: 27/05/2022 11:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_TRADES_TradeRiskCategorize]



AS
BEGIN

SET NOCOUNT ON

DECLARE @tradeIds AS TABLE
  (
     trade_id UNIQUEIDENTIFIER,
     position INT
  )

INSERT INTO @tradeIds
SELECT trade_id,
       Row_number()
         OVER (
           ORDER BY [trade_id]) AS POSITION
FROM   [trades] AS TRADE_ID


DECLARE @rules AS TABLE
  (
     rule_id        UNIQUEIDENTIFIER,
     rule_risk      VARCHAR(max),
     rule_condition VARCHAR(max),
     position       INT
  )

INSERT INTO @rules
SELECT rule_id,
       rule_risk,
       rule_condition,
       Row_number()
         OVER (
           ORDER BY rule_risk) AS POSITION
FROM   [trade_risk_rules]

DECLARE @TradeRowCnt INT;
DECLARE @TradePosition INT = 1;

SELECT @TradeRowCnt = ( Count(*) + 1 )
FROM   @tradeIds


WHILE @TradePosition < @TradeRowCnt
  BEGIN
      DECLARE @RuleRowCnt INT;
      DECLARE @RulePosition INT = 1;
      DECLARE @CurrentTradeId UNIQUEIDENTIFIER

      SELECT @CurrentTradeId = trade_id
      FROM   @tradeIds
      WHERE  position = @TradePosition

      SELECT @RuleRowCnt = ( Count(*) + 1 )
      FROM   @rules

      WHILE @RulePosition < @RuleRowCnt
        BEGIN
            DECLARE @ruleAppliedCount INT;
            DECLARE @CurrentRule VARCHAR(max)
            DECLARE @CurrentRuleId UNIQUEIDENTIFIER
            DECLARE @CurrentRuleRisk VARCHAR(max)
            DECLARE @ParmDefinition NVARCHAR(500);

            SELECT @CurrentRule = rule_condition,
                   @CurrentRuleId = rule_id,
                   @CurrentRuleRisk = rule_risk
            FROM   @rules
            WHERE  position = @RulePosition;

            DECLARE @query NVARCHAR(3000) =
				'SELECT @ruleAppliedCountOUT = COUNT(*) FROM [TRADES] WHERE TRADE_ID = '''
				+ Cast(@CurrentTradeId AS VARCHAR(max))
				+ ''' AND ' + @CurrentRule;

			SET @ParmDefinition = N'@ruleAppliedCountOUT int OUTPUT';

			EXEC Sp_executesql @query, @ParmDefinition, @ruleAppliedCountOUT=@ruleAppliedCount output

			IF @ruleAppliedCount > 0
				INSERT INTO [trades_risk_categorized]
                  ([trade_id],
                   [rule_id],
                   [trade_risk])
				VALUES (@CurrentTradeId,
                   @CurrentRuleId,
                   @CurrentRuleRisk)

			SET @RulePosition +=1
		END

	SET @TradePosition += 1
END 
END
GO
