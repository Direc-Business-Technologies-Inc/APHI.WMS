SELECT
	 ORDR.DocEntry
	,ORDR.DocNum
	,ORDR.DocDate
	,ORDR.DocDueDate
	,ORDR.DocStatus
	,OCRD.CardCode
	,OCRD.CardName
	,OCRD.CntctPrsn [ContactPerson]
	,ORDR.NumAtCard
	,ORDR.U_SchlYear [SchoolYear]
	,ORDR.U_PONo [PONo]
	,ORDR.U_Area [Area]
	,ORDR.U_Desig [Designation]
	,ORDR.U_OrdBy [OrderBy]
	,ORDR.U_Remarks [DocRemarks]
	,ORDR.U_PrepBy [PreparedBy]
	,ORDR.U_RevBy [ReviewedBy]
	,ORDR.U_AppBy [ApprovedBy]
	,ORDR.U_NotedBy [NotedBy]
	,CPN1.U_MaxInv [DiscPercent]
FROM ORDR
INNER JOIN OCRD ON OCRD.CardCode = ORDR.CardCode
OUTER APPLY(SELECT TOP 1 CPN1.U_MaxInv FROM CPN1 INNER JOIN OCPN ON CPN1.CpnNo = OCPN.CpnNo WHERE OCRD.CardCode = CPN1.BpCode AND OCPN.Status = 'O') CPN1
WHERE ORDR.DocEntry = @DocEntry
