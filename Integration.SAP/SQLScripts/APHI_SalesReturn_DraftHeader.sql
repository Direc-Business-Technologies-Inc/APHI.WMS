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
	,CPN1.U_MaxInv [DiscPercent]
FROM ODRF
INNER JOIN OCRD ON ODRF.CardCode = OCRD.CardCode
OUTER APPLY(SELECT TOP 1 CPN1.U_MaxInv FROM CPN1 INNER JOIN OCPN ON CPN1.CpnNo = OCPN.CpnNo WHERE OCRD.CardCode = CPN1.BpCode AND OCPN.Status = 'O') CPN1
WHERE ODRF.DocEntry = @DocEntry
	AND ODRF.ObjType = 16