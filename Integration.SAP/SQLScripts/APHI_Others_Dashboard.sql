SELECT 
(SELECT COUNT(*) FROM ODLN WHERE DocStatus = 'O') [DeliveryCount] ,

(SELECT COUNT(*) FROM ODLN
WHERE CANCELED = 'N'  AND CAST(CreateDate as Date) = CAST(GETDATE() as Date)) [DeliveryTodayCount],

(SELECT COUNT(*) FROM ORDN WHERE DocStatus = 'O') [ReturnCount] ,

(SELECT COUNT(*) FROM ODLN 
WHERE CANCELED = 'N'  AND CAST(CreateDate as Date) = CAST(GETDATE() as Date)) [ReturnTodayCount],

(SELECT COUNT(*) FROM OPOR WHERE DocStatus = 'O') [ReceivingCount],

(SELECT COUNT(*) FROM OPDN
WHERE CAST(CreateDate as Date) = CAST(GETDATE() as Date)) [ReceivingTodayCount];