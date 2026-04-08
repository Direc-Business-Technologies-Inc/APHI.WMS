SELECT 
	 T0.DocEntry
	,T0.DocNum
	,T1.LineNum
	,T1.ItemCode
	,T1.Dscription [ItemDescription]
	,T1.Quantity
	,CASE 
        WHEN ISNULL(T1.UomCode, '') = '' THEN 'Manual'
        ELSE T1.UomCode
     END AS [UoMCode]
	,ISNULL(T1.unitMsr, 'Manual') [UoMName]
FROM ODRF T0
INNER JOIN DRF1 T1 ON T1.DocEntry = T0.DocEntry
WHERE 
	T0.ObjType = 67 
	AND T0.DocEntry = @DocEntry
