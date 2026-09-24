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
	,ODRF.U_DRNo [DRNo]
	,ODRF.U_ActualDelivDate [ActualDeliveryDate]
	,ODRF.U_DelivMeans [DeliveryMeans]
	,ODRF.U_Courier [Courier]
	,ODRF.U_CourName [CourierName]
	,ODRF.U_Desig [Designation]
	,ODRF.U_WBNo [WaybillNo]
	,ODRF.U_PlateNo [PlateNo]
	,ODRF.U_Driver [Driver]
	,ODRF.U_Remarks [DocRemarks]
	,ODRF.U_RecBy [ReceivedBy]
	,ODRF.U_PrepBy [PreparedBy]
	,ODRF.U_AppBy [ApprovedBy]
	,ODRF.U_Area [Area]
	,ODRF.U_NotedBy [NotedBy]
	,CPN1.U_MaxInv [DiscPercent]
FROM ODRF
INNER JOIN OCRD ON ODRF.CardCode = OCRD.CardCode
OUTER APPLY(SELECT TOP 1 CPN1.U_MaxInv FROM CPN1 INNER JOIN OCPN ON CPN1.CpnNo = OCPN.CpnNo WHERE OCRD.CardCode = CPN1.BpCode AND OCPN.Status = 'O') CPN1
WHERE 
	ODRF.ObjType = 15 -- object type is 'Delivery Notes'
	AND ODRF.DocEntry = @DocEntry