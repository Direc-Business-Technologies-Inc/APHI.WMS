SELECT
     T0.ItemCode
    ,CASE
        WHEN ISNULL(T0.InvntryUom, '') = '' THEN NULL
        WHEN T2.UomEntry IS NOT NULL THEN T2.UomCode
        ELSE T0.InvntryUom
     END AS [UoMCode]
FROM OITM T0
INNER JOIN OITW T5 ON T0.ItemCode = T5.ItemCode
LEFT JOIN OUOM T2 ON T0.InvntryUom = T2.UomName
WHERE
    T0.ItemType = 'I'
    AND T0.Canceled = 'N'
    AND T5.WhsCode = '{WhsCode}'
