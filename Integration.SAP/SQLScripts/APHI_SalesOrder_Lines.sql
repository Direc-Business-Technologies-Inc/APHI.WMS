SELECT
     ORDR.DocEntry
    ,ORDR.DocNum
    ,RDR1.LineNum
    ,RDR1.ItemCode
    ,OITM.ItemName
    ,RDR1.WhsCode
    ,OWHS.WhsName
    ,RDR1.Quantity [TargetQty]
    ,OITW.OnHand [OnHand]
    ,0 [Quantity]
    ,RDR1.OpenQty [OpenQty]
    ,CASE 
        WHEN ISNULL(RDR1.UomCode, '') = '' THEN 'Manual'
        ELSE RDR1.UomCode
     END AS [UoMCode]
    ,RDR1.NumPerMsr [UoMValue]
    ,OUOM.UomName [UoMName]
    ,REPLACE(ISNULL(OITM.U_ISBN, ''), '-', '') [ISBN]

    ,ISNULL(F.Freight1Code, 0) [Freight1Code]
    ,ISNULL(F.Freight1, 0) [Freight1]

    ,ISNULL(F.Freight2Code, 0) [Freight2Code]
    ,ISNULL(F.Freight2, 0) [Freight2]

FROM ORDR

INNER JOIN RDR1 
    ON RDR1.DocEntry = ORDR.DocEntry

INNER JOIN OITM 
    ON RDR1.ItemCode = OITM.ItemCode

INNER JOIN OUOM 
    ON RDR1.UomEntry = OUOM.UomEntry

INNER JOIN OWHS 
    ON RDR1.WhsCode = OWHS.WhsCode

INNER JOIN OITW 
    ON OWHS.WhsCode = OITW.WhsCode 
    AND OITW.ItemCode = OITM.ItemCode

LEFT JOIN
(
    SELECT
         DocEntry
        ,LineNum

        ,MAX(CASE 
            WHEN ExpnsCode = 3 THEN ExpnsCode
         END) AS Freight1Code

        ,SUM(CASE 
            WHEN ExpnsCode = 3 THEN LineTotal
            ELSE 0
         END) AS Freight1

        ,MAX(CASE 
            WHEN ExpnsCode = 5 THEN ExpnsCode
         END) AS Freight2Code

        ,SUM(CASE 
            WHEN ExpnsCode = 5 THEN LineTotal
            ELSE 0
         END) AS Freight2

    FROM RDR2
    GROUP BY
         DocEntry
        ,LineNum
) F
    ON F.DocEntry = RDR1.DocEntry
    AND F.LineNum = RDR1.LineNum

WHERE ORDR.DocEntry = @DocEntry