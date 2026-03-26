SELECT T0.DocNum
    ,T0.DocDate
    ,T0.Filler [FrmWhsCode]
    ,T1.WhsName [FrmWhsName]
    ,T0.ToWhsCode [ToWhsCode]
    ,T2.WhsName [ToWhsName]
    ,T0.U_TransferType [TransferType]
    ,T0.U_SchlYear [SchoolYear]
    ,ISNULL(T0.U_Remarks, '') [Remarks]
    ,T0.U_PrepBy [PreparedBy]
    ,T0.U_AppBy [ApprovedBy]
    ,T0.U_NotedBy [NotedBy]
FROM OWTR T0
LEFT JOIN OWHS T1 ON T0.Filler = T1.WhsCode
LEFT JOIN OWHS T2 ON T0.ToWhsCode = T2.WhsCode
WHERE T0.DocEntry = @DocEntry