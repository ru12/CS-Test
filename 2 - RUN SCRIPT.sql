-- =============================================================================================================================
-- Script show case the rules engine approach to classify trades based on rules criteria.
-- Rules can further be added, modified, deleted as needed
--
-- Table [TRADE_RISK_RULES] will be initialized with predefined risks rules for LOWRISK, MEDIUMRISK and HIGHRISK
-- Table [TRADES] will be initialized with the portifolio of trades predefined.
-- Table [TRADES_RISK_CATEGORIZED] will be the output table after the sp_TRADES_TradeRiskCategorize storage procedure runs 
--
-- =============================================================================================================================


--Cleanup before categorize
truncate table [TRADES_RISK_CATEGORIZED]

--Execute the stored procedure that classifys all trades according to the rules table
EXECUTE [dbo].[sp_TRADES_TradeRiskCategorize] 
GO

--Select values to show trades classified
SELECT a.TRADE_TYPE, a.VALUE, a.CLIENT_SECTOR, b.TRADE_RISK
FROM TRADES a
INNER JOIN [TRADES_RISK_CATEGORIZED] b
ON a.TRADE_ID = b.TRADE_ID

