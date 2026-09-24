SELECT
	 ORDN.DocEntry
	,ORDN.DocNum
	,ORDN.DocDate
	,ORDN.DocDueDate
	,ORDN.CardCode
	,ORDN.CardName
	,OCRD.CntctPrsn [ContactPerson]
	,ORDN.NumAtCard
	,ORDN.U_SchlYear [SchoolYear]
	,ORDN.U_RetType [ReturnType]
	,ORDN.U_PURNo [PURNo]
	,ORDN.U_DRNo [DRNo]
	,ORDN.U_SONo [SONo]
	,ORDN.U_SINo [SINo]
	,ORDN.U_Desig [Designation]
	,ORDN.U_Remarks [DocRemarksa]
	,ORDN.U_RetBy [ReturnedBy]
	,ORDN.U_PickBy [PickBy]
	,ORDN.U_PrepBy [PreparedBy]
	,ORDN.U_CheckBy [CheckedBy]
	,ORDN.U_NotedBy [NotedBy]
	,ORDN.U_AppBy [ApprovedBy]
	,CPN1.U_MaxInv [DiscPercent]
FROM ORDN
INNER JOIN OCRD ON ORDN.CardCode = OCRD.CardCode
OUTER APPLY(SELECT TOP 1 CPN1.U_MaxInv FROM CPN1 INNER JOIN OCPN ON CPN1.CpnNo = OCPN.CpnNo WHERE OCRD.CardCode = CPN1.BpCode AND OCPN.Status = 'O') CPN1
WHERE ORDN.DocEntry = @DocEntry