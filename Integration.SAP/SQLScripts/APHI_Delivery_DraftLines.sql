SELECT
	 ODRF.DocEntry
	,ODRF.DocNum
	,DRF1.LineNum
	,DRF1.ItemCode
	,OITM.ItemName
	,DRF1.WhsCode
	,OWHS.WhsName
	,DRF1.Quantity
	,CASE 
        WHEN ISNULL(DRF1.UomCode, '') = '' THEN 'Manual'
        ELSE DRF1.UomCode
     END AS [UoMCode]
	,DRF1.NumPerMsr [UoMValue]
	,OUOM.UomName [UoMName]
	,DRF1.BaseEntry [CreatedFrom]
FROM ODRF
INNER JOIN DRF1 ON DRF1.DocEntry = ODRF.DocEntry
INNER JOIN OITM ON DRF1.ItemCode = OITM.ItemCode
INNER JOIN OUOM ON DRF1.UomEntry = OUOM.UomEntry
INNER JOIN OWHS ON DRF1.WhsCode = OWHS.WhsCode
WHERE ODRF.DocEntry = @DocEntry