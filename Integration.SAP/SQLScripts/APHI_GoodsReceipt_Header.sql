SELECT 
       T0.DocEntry
     , T0.DocNum
     , T0.DocDate
     , T0.U_PrepBy [PreparedBy]
     , T1.U_TransType [TransTypeCode]
     , T1.Name [TransTypeName]
     , T2.AcctCode
     , T2.AcctName
FROM OIGN AS T0
LEFT JOIN [@TRANSACTION_TYPE] T1 ON T1.U_TransType = T0.U_TransType
LEFT JOIN OACT T2 ON T2.FormatCode = T1.U_AcctCode
WHERE T0.DocEntry = @DocEntry