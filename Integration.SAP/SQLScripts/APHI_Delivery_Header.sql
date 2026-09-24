SELECT
	ODLN.DocEntry
	,ODLN.DocNum
	,ODLN.DocDate
	,ODLN.DocDueDate
	,ODLN.CardCode
	,ODLN.CardName
	,OCRD.CntctPrsn [ContactPerson]
	,ODLN.NumAtCard
	,ODLN.U_SchlYear [SchoolYear]
	,ODLN.U_DRNo [DRNo]
	,ODLN.U_ActualDelivDate [ActualDeliveryDate]
	,ODLN.U_DelivMeans [DeliveryMeans]
	,ODLN.U_Courier [Courier]
	,ODLN.U_CourName [CourierName]
	,ODLN.U_Desig [Designation]
	,ODLN.U_WBNo [WaybillNo]
	,ODLN.U_PlateNo [PlateNo]
	,ODLN.U_Driver [Driver]
	,ODLN.U_Remarks [DocRemarks]
	,ODLN.U_RecBy [ReceivedBy]
	,ODLN.U_PrepBy [PreparedBy]
	,ODLN.U_AppBy [ApprovedBy]
	,ODLN.U_Area [Area]
	,ODLN.U_NotedBy [NotedBy]
	,CPN1.U_MaxInv [DiscPercent]
FROM ODLN
INNER JOIN OCRD ON ODLN.CardCode = OCRD.CardCode
OUTER APPLY(SELECT TOP 1 CPN1.U_MaxInv FROM CPN1 INNER JOIN OCPN ON CPN1.CpnNo = OCPN.CpnNo WHERE OCRD.CardCode = CPN1.BpCode AND OCPN.Status = 'O') CPN1
WHERE ODLN.DocEntry = @DocEntry