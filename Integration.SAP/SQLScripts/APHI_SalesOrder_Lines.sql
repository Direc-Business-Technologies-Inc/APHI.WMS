SELECT
	 ORDR.DocEntry
	,ORDR.DocNum
	,RDR1.LineNum
	,RDR1.ItemCode
	,OITM.ItemName
	,RDR1.WhsCode
	,OWHS.WhsName
	,RDR1.Quantity [TargetQty]
	,0 [Quantity]
	,RDR1.OpenQty [OpenQty]
	,CASE 
        WHEN ISNULL(RDR1.UomCode, '') = '' THEN 'Manual'
        ELSE RDR1.UomCode
     END AS [UoMCode]
	,RDR1.NumPerMsr [UoMValue]
	,OUOM.UomName [UoMName]
FROM ORDR
INNER JOIN RDR1 ON RDR1.DocEntry = ORDR.DocEntry
INNER JOIN OITM ON RDR1.ItemCode = OITM.ItemCode
INNER JOIN OUOM ON RDR1.UomEntry = OUOM.UomEntry
INNER JOIN OWHS ON RDR1.WhsCode = OWHS.WhsCode
INNER JOIN OITW ON OWHS.WhsCode = OITW.WhsCode AND OITW.ItemCode = OITM.ItemCode
WHERE ORDR.DocEntry = @DocEntry