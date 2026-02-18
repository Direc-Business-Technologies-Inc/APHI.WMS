SELECT 
	 T0.DocEntry
	,T0.DocNum
	,T1.BaseEntry
	,T3.DocNum [BaseDocNum]
	,T0.DocDate
	,T0.DocDueDate
	,T0.CardCode
	,T5.CardName
	,T6.Name [SupplierContactPerson]
	,T0.U_RecBy [ReceivedBy]
FROM OPDN AS T0
INNER JOIN PDN1 AS T1 ON T0.DocEntry = T1.DocEntry
INNER JOIN OPOR AS T3 ON T1.BaseEntry = T3.DocEntry
INNER JOIN OCRD AS T5 ON T0.CardCode = T5.CardCode
LEFT JOIN OCPR AS T6 ON T3.CntctCode = T6.CntctCode
WHERE 
	T0.Comments LIKE '%WMS%'
	AND T0.DocEntry = @DocEntry