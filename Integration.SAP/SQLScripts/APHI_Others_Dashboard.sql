SELECT COUNT(*) [DeliveryCount] FROM ODLN WHERE DocStatus = 'O'

UNION ALL

SELECT COUNT(*) [ReturnCount] FROM ORDN WHERE DocStatus = '0'

UNION ALL 

SELECT COUNT(*) [ReceivingCount] FROM OPOR WHERE DocStatus = '0';