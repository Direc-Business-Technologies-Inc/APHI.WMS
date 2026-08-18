SELECT
	 ODLN.DocEntry
	,ODLN.DocNum
	,DLN1.LineNum
	,DLN1.ItemCode
	,OITM.ItemName
	,DLN1.WhsCode
	,OWHS.WhsName
	,DLN1.Quantity
	,CASE 
        WHEN ISNULL(DLN1.UomCode, '') = '' THEN 'Manual'
        ELSE DLN1.UomCode
     END AS [UoMCode]
	,DLN1.NumPerMsr [UoMValue]
	,OUOM.UomName [UoMName]
	,REPLACE(ISNULL(OITM.U_ISBN, ''), '-', '') [ISBN]
FROM ODLN
INNER JOIN DLN1 ON DLN1.DocEntry = ODLN.DocEntry
INNER JOIN OITM ON DLN1.ItemCode = OITM.ItemCode
INNER JOIN OUOM ON DLN1.UomEntry = OUOM.UomEntry
INNER JOIN OWHS ON DLN1.WhsCode = OWHS.WhsCode
WHERE ODLN.DocEntry = @DocEntry