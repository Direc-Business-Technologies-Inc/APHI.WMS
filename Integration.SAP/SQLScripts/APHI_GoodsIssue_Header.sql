SELECT 
       T0.DocEntry
     , T0.DocNum
     , T0.DocDate
     , T0.U_PrepBy [PreparedBy]
     , T1.Code [TransTypeCode]
     , T1.Name [TransTypeName]
     , T2.AcctCode
     , T2.AcctName
     , T0.U_SchlYear [SchoolYear]
     , T0.U_SRFNo [SrfNo]
     , T0.U_Desig [Designation]
     , T0.U_Remarks [DocRemarks]
     , T0.U_AppBy [ApprovedBy]
FROM OIGE AS T0
LEFT JOIN [@TRANSACTION_TYPE] T1 ON T1.Code = T0.U_TransType
LEFT JOIN OACT T2 ON T2.FormatCode = T1.U_AcctCode
WHERE T0.DocEntry = @DocEntry