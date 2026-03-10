SELECT
	 ODLN.DocEntry
	,ODLN.DocNum
	,ODLN.CardCode
	,OCRD.CardName
	,OCRD.CntctPrsn
	,ODLN.U_Remarks [Remarks]
FROM ODLN
INNER JOIN OCRD ON OCRD.CardCode = ODLN.CardCode
WHERE ODLN.Comments LIKE '%WMS%'