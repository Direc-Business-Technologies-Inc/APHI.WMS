SELECT
	 ODRF.DocEntry
	,ODRF.DocNum
	,DRF1.LineNum
	,DRF1.ItemCode
	,OITM.ItemName
	,DRF1.WhsCode
	,OWHS.WhsName
	,DRF1.Quantity [Quantity]
	,CASE 
        WHEN ISNULL(DRF1.UomCode, '') = '' THEN 'Manual'
        ELSE DRF1.UomCode
     END AS [UoMCode]
	,DRF1.NumPerMsr [UoMValue]
	,OUOM.UomName [UoMName]
	,REPLACE(ISNULL(OITM.U_ISBN, ''), '-', '') [ISBN]

	,ISNULL(F.Freight1Code, 0) [Freight1Code]
	,ISNULL(F.Freight1, 0) [Freight1]

	,ISNULL(F.Freight2Code, 0) [Freight2Code]
	,ISNULL(F.Freight2, 0) [Freight2]
FROM ODRF
INNER JOIN DRF1 ON DRF1.DocEntry = ODRF.DocEntry
INNER JOIN OITM ON DRF1.ItemCode = OITM.ItemCode
INNER JOIN OUOM ON DRF1.UomEntry = OUOM.UomEntry
INNER JOIN OWHS ON DRF1.WhsCode = OWHS.WhsCode

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

    FROM DRF2
    GROUP BY
         DocEntry
        ,LineNum
) F
    ON F.DocEntry = DRF1.DocEntry
    AND F.LineNum = DRF1.LineNum

WHERE ODRF.DocEntry = @DocEntry
	AND ODRF.ObjType = 16