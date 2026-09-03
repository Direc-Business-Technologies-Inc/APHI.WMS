SELECT
	 ORDN.DocEntry
	,ORDN.DocNum
	,RDN1.LineNum
	,RDN1.ItemCode
	,OITM.ItemName
	,RDN1.WhsCode
	,OWHS.WhsName
	,RDN1.Quantity [Quantity]
	,CASE 
        WHEN ISNULL(RDN1.UomCode, '') = '' THEN 'Manual'
        ELSE RDN1.UomCode
     END AS [UoMCode]
	,RDN1.NumPerMsr [UoMValue]
	,OUOM.UomName [UoMName]

	,ISNULL(F.Freight1Code, 0) [Freight1Code]
	,ISNULL(F.Freight1, 0) [Freight1]

	,ISNULL(F.Freight2Code, 0) [Freight2Code]
	,ISNULL(F.Freight2, 0) [Freight2]
FROM ORDN
INNER JOIN RDN1 ON RDN1.DocEntry = ORDN.DocEntry
INNER JOIN OITM ON RDN1.ItemCode = OITM.ItemCode
INNER JOIN OUOM ON RDN1.UomEntry = OUOM.UomEntry
INNER JOIN OWHS ON RDN1.WhsCode = OWHS.WhsCode

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

    FROM RDN2
    GROUP BY
         DocEntry
        ,LineNum
) F
    ON F.DocEntry = RDN1.DocEntry
    AND F.LineNum = RDN1.LineNum

WHERE ORDN.DocEntry = @DocEntry