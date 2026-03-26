SELECT T0.ItemCode
	,T0.Dscription [ItemDescription]
	,T0.unitMsr [UoM]
	,T0.Quantity [Quantity]
FROM WTR1 AS T0
WHERE T0.DocEntry = @DocEntry