CREATE FUNCTION getMaxMinGainForSymbolInDateInterval(@dateFrom datetime, @dateTo datetime, @symbolId int)
RETURNS 
@report TABLE
(
	TradeDate datetime, Price MONEY, Dividends MONEY
)
AS BEGIN 

		DECLARE @tab TABLE(TradeDate datetime, Price MONEY, Dividends MONEY)

		INSERT INTO @tab SELECT DISTINCT tr.TradeDate, tr.TradeIndex AS Price, ISNULL( (
									SELECT SUM(d.DividendAmount)
									FROM SymbolDividends d
	   								WHERE d.SymbolId = tr.SymbolId AND d.SymbolID = @symbolId AND
	   								d.TradeDate = tr.TradeDate ),0) AS Dividends
		FROM TradeSybolInformation tr
		WHERE tr.SymbolID = @symbolId  AND tr.TradeDate >= @dateFrom AND tr.TradeDate <= @dateTo
		ORDER BY tr.TradeDate

		INSERT INTO @report SELECT TOP 1 * FROM @tab
		WHERE Price + Dividends = (SELECT MAX(Price + Dividends) FROM @tab)

		INSERT INTO @report SELECT TOP 1 * FROM @tab
		WHERE Price + Dividends = (SELECT MIN(Price + Dividends) FROM @tab)

	RETURN 
END
