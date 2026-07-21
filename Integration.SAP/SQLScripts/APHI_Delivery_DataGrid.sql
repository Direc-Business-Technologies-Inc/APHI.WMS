SELECT
	 ODLN.DocEntry
	,ODLN.DocNum
	,ODLN.DocDate
	,ODLN.DocStatus
	,ODLN.CardCode
	,ODLN.U_Area [Area]
	,OCRD.CardName
	,OCRD.CntctPrsn [ContactPerson]
	,ODLN.U_PrepBy [PreparedBy]
	,ODLN.U_Remarks [Remarks]
	,SY.Name [SchoolYear]
FROM ODLN
INNER JOIN OCRD ON OCRD.CardCode = ODLN.CardCode
LEFT JOIN [@SCHL_YR] SY ON ODLN.U_SchlYear = SY.Code
WHERE ODLN.Comments LIKE '%WMS%'