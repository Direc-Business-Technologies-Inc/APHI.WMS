SELECT
	 ODRF.DocDate
	,ODRF.DocEntry
	,ODRF.DocNum
	,OCRD.CardCode
	,OCRD.CardName
	,ISNULL(ODRF.U_Remarks, '-') [Remarks]
FROM ODRF
INNER JOIN OCRD ON OCRD.CardCode = ODRF.CardCode
WHERE ODRF.Comments LIKE '%WMS%'
	AND ODRF.ObjType = 16