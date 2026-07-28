SELECT
	 T0.DocEntry
	,T0.DocNum
	,T0.DocDate
	,T0.DocDueDate
	,T0.CardCode
	,T0.CardName
	,ISNULL(T0.Comments, '') [Remarks]
	,CASE 
		WHEN T0.CANCELED = 'Y' THEN 'Cancelled'	
		WHEN T0.DocStatus = 'C' THEN 'Closed'
	 ELSE 'Open'
	 END as DocStatus
	,T0.U_SchlYear [SchoolYear]
	,T0.U_RetType [ReturnType]
	,T0.U_DRNo [DRNo]
	,T0.U_SINo [SINo]
	,T0.U_DelBy [DeliveredBy]
	,T0.U_RecBy [ReceivedBy]
	,T0.U_Remarks [DocRemarks]
	,T0.U_PrepBy [PreparedBy]
	,T0.U_RevBy [ReviewedBy]
	,T0.U_AppBy [ApprovedBy]
	,T0.U_CheckBy [CheckedBy]
	,AX1.WhsCode
	,T8.WhsName
FROM ODRF T0
OUTER APPLY (
	SELECT
		X1.WhsCode
	FROM DRF1 X1
	WHERE X1.DocEntry = T0.DocEntry
) AX1
INNER JOIN DRF1 T1 ON T1.DocEntry = T0.DocEntry
INNER JOIN OWHS T8 ON AX1.WhsCode = T8.WhsCode
WHERE T0.DocEntry = @DocEntry