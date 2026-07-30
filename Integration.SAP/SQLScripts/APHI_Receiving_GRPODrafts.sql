SELECT TOP(10)
	 ODRF.DocEntry
	,ODRF.DocEntry [DocNum]
	,ODRF.DocDate
	,ODRF.DocDueDate
	,ODRF.CardCode
	,ODRF.U_RecBy [ReceivedBy]
	,AX1.*
	,AX2.*
FROM ODRF
OUTER APPLY (
	SELECT TOP 1  
		 ISNULL(T3.DocEntry, -1) [BaseEntry]
		,ISNULL(T3.DocNum, -1) [BaseDocNum]
		,T3.DocDate [PoDocDate]
		,T5.CardName
		,T6.Name [SupplierContactPerson]
		,T1.WhsCode
		,T4.WhsName
	FROM DRF1 T1
	INNER JOIN OPOR AS T3 ON T1.BaseEntry = T3.DocEntry
	INNER JOIN OWHS AS T4 ON T1.WhsCode = T4.WhsCode
	INNER JOIN OCRD AS T5 ON ODRF.CardCode = T5.CardCode
	LEFT JOIN OCPR AS T6 ON T3.CntctCode = T6.CntctCode
	WHERE ODRF.DocEntry = T1.DocEntry
) AS AX1
OUTER APPLY (
    SELECT
		 STRING_AGG(X1.ItemName, ', ') AS ItemDesc
    FROM DRF1 AS X0
    INNER JOIN OITM AS X1 ON X0.ItemCode = X1.ItemCode
    WHERE X0.DocEntry = ODRF.DocEntry
) AS AX2
WHERE 
	ODRF.Comments LIKE '%WMS%' AND
	ODRF.ObjType = 20