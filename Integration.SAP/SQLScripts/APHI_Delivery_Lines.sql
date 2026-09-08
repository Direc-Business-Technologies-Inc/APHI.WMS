SELECT
	 ODLN.DocEntry
	,ODLN.DocNum
	,DLN1.LineNum
	,DLN1.ItemCode
	,OITM.ItemName
	,DLN1.WhsCode
	,OWHS.WhsName
	,DLN1.Quantity
	,CASE 
        WHEN ISNULL(DLN1.UomCode, '') = '' THEN 'Manual'
        ELSE DLN1.UomCode
     END AS [UoMCode]
	,DLN1.NumPerMsr [UoMValue]
	,OUOM.UomName [UoMName]
	,REPLACE(ISNULL(OITM.U_ISBN, ''), '-', '') [ISBN]

	,ISNULL(F.Freight1Code, 0) [Freight1Code]
	,ISNULL(F.Freight1, 0) [Freight1]

	,ISNULL(F.Freight2Code, 0) [Freight2Code]
	,ISNULL(F.Freight2, 0) [Freight2]
    ,DLN1.U_MarkUp [MarkUp]
FROM ODLN
INNER JOIN DLN1 ON DLN1.DocEntry = ODLN.DocEntry
INNER JOIN OITM ON DLN1.ItemCode = OITM.ItemCode
INNER JOIN OUOM ON DLN1.UomEntry = OUOM.UomEntry
INNER JOIN OWHS ON DLN1.WhsCode = OWHS.WhsCode

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

    FROM DLN2
    GROUP BY
         DocEntry
        ,LineNum
) F
    ON F.DocEntry = DLN1.DocEntry
    AND F.LineNum = DLN1.LineNum

WHERE ODLN.DocEntry = @DocEntry