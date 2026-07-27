SELECT
	 ODRF.DocEntry
	,ODRF.DocNum
	,ODRF.DocDate
	,ODRF.DocStatus
	,ODRF.CardCode
	,ODRF.U_Area [Area]
	,OCRD.CardName
	,OCRD.CntctPrsn [ContactPerson]
	,ODRF.U_PrepBy [PreparedBy]
	,ODRF.U_Remarks [Remarks]
	,SY.Name [SchoolYear]
	,DRF1.BaseEntry [CreatedFrom]
FROM ODRF
INNER JOIN OCRD ON OCRD.CardCode = ODRF.CardCode
LEFT JOIN [@SCHL_YR] SY ON ODRF.U_SchlYear = SY.Code
LEFT JOIN DRF1 ON DRF1.DocEntry = ODRF.DocEntry
	AND DRF1.LineNum = (SELECT MIN(LineNum) FROM DRF1 sub WHERE sub.DocEntry = ODRF.DocEntry)
WHERE 
	ODRF.ObjType = 15 AND
	ODRF.Comments LIKE '%WMS%'