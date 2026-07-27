SELECT
	 ODRF.DocEntry
	,ODRF.DocNum
	,ODRF.DocDate
	,ODRF.DocDueDate
	,ODRF.CardCode
	,ODRF.CardName
	,OCRD.CntctPrsn [ContactPerson]
	,ODRF.NumAtCard
	,ODRF.U_SchlYear [SchoolYear]
	,ODRF.U_RetType [ReturnType]
	,ODRF.U_PURNo [PURNo]
	,ODRF.U_DRNo [DRNo]
	,ODRF.U_SONo [SONo]
	,ODRF.U_SINo [SINo]
	,ODRF.U_Desig [Designation]
	,ODRF.U_Remarks [DocRemarksa]
	,ODRF.U_RetBy [ReturnedBy]
	,ODRF.U_PickBy [PickBy]
	,ODRF.U_PrepBy [PreparedBy]
	,ODRF.U_CheckBy [CheckedBy]
	,ODRF.U_NotedBy [NotedBy]
	,ODRF.U_AppBy [ApprovedBy]
FROM ODRF
INNER JOIN OCRD ON ODRF.CardCode = OCRD.CardCode
WHERE ODRF.DocEntry = @DocEntry
	AND ODRF.ObjType = 16