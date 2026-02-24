SELECT 
	  T0.Name [Code] 
	, T0.U_TransactionType [Name]
	, T1.AcctCode
	, T1.AcctName
FROM [@TRANSACTION_TYPE] T0
LEFT JOIN OACT T1 ON T1.FormatCode = T0.U_AcctCode
ORDER BY Code
