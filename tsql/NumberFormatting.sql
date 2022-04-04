SET formatted = FORMAT ( 
  MAX ( 
    TRY_CONVERT(BIGINT, 
      SUBSTRING ( E.StringWithNumbers, PATINDEX('%[0-9]%', E.StringWithNumbers ), 
      LEN( E.StringWithNumbers ) ) 
    ) 
  ) + 1, '00000000' )
