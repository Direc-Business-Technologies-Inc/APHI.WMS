SELECT 
	 T0.DocEntry
	,T0.DocNum
	,T1.LineNum
	,T1.ItemCode
	,T2.ItemName
	,T1.Quantity
	,ISNULL(T1.unitMsr, 'Manual') [UoMCode]
	,T1.NumPerMsr [UoMValue]
	,T4.UomName [UoMName]
	,T1.U_InputType [InputType]
FROM OPDN AS T0
INNER JOIN PDN1 AS T1 ON T0.DocEntry = T1.DocEntry
INNER JOIN OITM AS T2 ON T1.ItemCode = T2.ItemCode
LEFT JOIN OUOM AS T4 ON T1.UomEntry = T4.UomEntry
WHERE
	T0.DocEntry = @DocEntry
ORDER BY
	T1.LineNum