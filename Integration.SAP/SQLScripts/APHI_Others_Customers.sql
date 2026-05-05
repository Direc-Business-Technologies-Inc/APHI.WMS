SELECT DISTINCT
	 T0.CardCode
	,T0.CardName
FROM OCRD T0
WHERE
	CardType = 'C'
	AND ISNULL(T0.CardName, '') <> ''
	AND T0.ValidFor = 'Y'
	AND T0.FrozenFor = 'N'
