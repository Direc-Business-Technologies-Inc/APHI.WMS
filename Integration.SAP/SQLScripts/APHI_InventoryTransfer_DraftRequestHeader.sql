SELECT
	  T0.DocEntry
	, T0.DocNum
    , T0.DocDate
    , T0.U_PrepBy [PreparedBy]
    , T0.U_AppBy [ApprovedBy]
    , T0.U_Remarks [Remarks]
    , T0.U_NotedBy [NotedBy]
    , T0.Filler [FrmWhsCode]
    , T0.ToWhsCode [ToWhsCode]
    , T1.WhsName [FrmWhsName]
    , T2.WhsName [ToWhsName]
    , T0.U_SchlYear [SchoolYear]
    , T0.U_TransferType [TransferType]
FROM ODRF T0
INNER JOIN OWDD T3 ON T0.DocEntry = T3.DraftEntry AND T0.ObjType = T3.ObjType
LEFT JOIN OWHS T1 ON T1.WhsCode = T0.Filler
LEFT JOIN OWHS T2 ON T2.WhsCode = T0.ToWhsCode
WHERE 
	T0.ObjType = 67 AND T0.DocEntry = @DocEntry
    AND T3.Status = @Status