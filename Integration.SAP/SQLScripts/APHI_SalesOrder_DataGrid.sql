SELECT
	 ORDR.DocEntry
	,ORDR.DocNum
	,ORDR.DocDate
	,ORDR.CardCode
	,OCRD.CardName
	,OCRD.CntctPrsn
	,ORDR.U_Remarks [Remarks]
FROM ORDR
INNER JOIN OCRD ON OCRD.CardCode = ORDR.CardCode
WHERE 
	ORDR.DocStatus = 'O'
	AND ORDR.CANCELED = 'N'