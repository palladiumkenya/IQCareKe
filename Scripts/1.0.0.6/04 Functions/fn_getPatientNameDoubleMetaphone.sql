IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_getPatientNameDoubleMetaphone]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_getPatientNameDoubleMetaphone]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_getPatientNameDoubleMetaphone]    Script Date: 25-May-2018 16:03:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*------------------------------------------------------------------------------------------------
-- This SQL implements the Double Metaphone algorythm (c) 1998, 1999 by Lawrence Philips
-- it was translated to Python, and BEGIN to SQL from the C source written by Kevin Atkinson (http://aspell.net/metaphone/)
-- By Andrew Collins (atomodo.com) - Feb, 2007 who claims no rights to this work
-- github.com/AtomBoy/double-metaphone
-- Tested with MySQL 5.1 on Ubuntu 6.01 and Ubuntu 10.4
-- Updated Nov 27, 2007 to fix a bug in the 'CC' @section
-- Updated Jun 01, 2010 to fix a bug in the 'Z' @section - thanks Nils Johnsson!
-- Updated Jun 25, 2010 to fix 16 signifigant bugs - thanks again Nils Johnsson for a spectacular
--   bug squashing effort. There were many cases where this function wouldn't give the same output
--   as the original C source that were fixed by his careful attention and excellent communication.

Adopted for T-SQL by MWENDA GITONGA - May 2018 who claims no rights to this work 

-------------------------------------------------------------------------------------------------*/



CREATE FUNCTION [dbo].[fn_getPatientNameDoubleMetaphone](@st VARCHAR(55)) 
RETURNS varchar(128) 

AS
BEGIN
	DECLARE @Length SMALLINT, @first SMALLINT, @Last SMALLINT, @pos SMALLINT, @prevpos SMALLINT, @is_slavo_germanic SMALLINT;
	DECLARE @pri VARCHAR(45) = '', @sec VARCHAR(45) = ''
	DECLARE @ch CHAR(1);
	-- returns the double metaphone code OR codes for given string
	-- if there is a @secondary dm it is separated with a semicolon
	-- there are no checks done on the input string, but it should be a single word OR name.
	--  st is short for string. I usually prefer descriptive over short, but this var is used a lot!

	SET @st = LTRIM(RTRIM(REPLACE(REPLACE(@st,'-',''),'''','')))
	SET @first = 3;
	SET @Length = LEN(@st);
	SET @Last = @first + @Length -1;

	SET @st = CONCAT(REPLICATE('-', @first -1), UPPER(@st), REPLICATE(' ', 5)); --  pad st so we can index beyond the begining AND end of the input string
	SET @is_slavo_germanic = CASE WHEN @st LIKE '%W%' OR @st LIKE '%K%' OR @st LIKE '%CZ%' THEN 1 ELSE 0 END;  -- the check for '%W%' will cat@ch WITZ
	SET @pos = @first; --  @pos is short for @position
	-- skip these silent letters IF at start of word
	IF SUBSTRING(@st, @first, 2) IN ('GN', 'KN', 'PN', 'WR', 'PS') 
    BEGIN
		SET @pos = @pos + 1;
	END
	--  Initial 'X' is pronounced 'Z' e.g. 'Xavier'
	IF SUBSTRING(@st, @first, 1) = 'X' 
	BEGIN
		SET @pri = 'S' SET @sec = 'S' SET @pos = @pos  + 1; -- 'Z' maps to 'S'
	END
	--  main loop through chars IN st
	WHILE @pos <= @Last 
	BEGIN --
		-- @print str(@pos) + '\t' + SUBSTRING(@st, @pos)
    SET @prevpos = @pos;
		SET @ch = SUBSTRING(@st, @pos, 1); --  @ch is short for character
		--CASE
		IF @ch IN ('A', 'E', 'I', 'O', 'U', 'Y') 
		BEGIN
			IF @pos = @first 
			BEGIN --  all init vowels now map to 'A'
				SET @pri = CONCAT(@pri, 'A') SET @sec = CONCAT(@sec, 'A') SET @pos = @pos  + 1; -- nxt = ('A', 1)
			END
			ELSE
				SET @pos = @pos + 1;
		END --end aeiouy
		--****
		IF @ch = 'B' 
		BEGIN
			-- '-mb', e.g', 'dumb', already skipped over... see 'M' below
			IF SUBSTRING(@st, @pos+1, 1) = 'B' 
			BEGIN
				SET @pri = CONCAT(@pri, 'P')SET @sec = CONCAT(@sec, 'P')SET @pos = @pos  + 2; -- nxt = ('P', 2)
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'P')SET @sec = CONCAT(@sec, 'P')SET @pos = @pos  + 1; -- nxt = ('P', 1)
			END
		END			
		IF @ch = 'C' 
		BEGIN
			--  various germanic
			IF (@pos > (@first + 1) AND SUBSTRING(@st, @pos-2, 1) NOT IN ('A', 'E', 'I', 'O', 'U', 'Y') AND SUBSTRING(@st, @pos-1, 3) = 'ACH' AND
			   (SUBSTRING(@st, @pos+2, 1) NOT IN ('I', 'E') OR SUBSTRING(@st, @pos-2, 6) IN ('BACHER', 'MACHER'))) 
			BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
			END --  special case 'CAESAR'
			ELSE IF @pos = @first AND SUBSTRING(@st, @first, 6) = 'CAESAR' 
			BEGIN
				SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'S')SET @pos = @pos  + 2; -- nxt = ('S', 2)
			END
			ELSE IF SUBSTRING(@st, @pos, 4) = 'CHIA' 
			BEGIN -- italian 'chianti'
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
			END
			ELSE IF SUBSTRING(@st, @pos, 2) = 'CH' 
			BEGIN
				--  find 'michael'
				IF @pos > @first AND SUBSTRING(@st, @pos, 4) = 'CHAE' 
				BEGIN
					SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'X')SET @pos = @pos  + 2; -- nxt = ('K', 'X', 2)
				END
				ELSE IF @pos = @first AND (SUBSTRING(@st, @pos+1, 5) IN ('HARAC', 'HARIS') OR  SUBSTRING(@st, @pos+1, 3) IN ('HOR', 'HYM', 'HIA', 'HEM')) AND SUBSTRING(@st, @first, 5) != 'CHORE' 
				BEGIN
					SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
				END -- germanic, greek, OR otherwise 'ch' for 'kh' sound
				ELSE IF SUBSTRING(@st, @first, 4) IN ('VAN ', 'VON ') OR SUBSTRING(@st, @first, 3) = 'SCH' OR SUBSTRING(@st, @pos-2, 6) IN ('ORCHES', 'ARCHIT', 'ORCHID')
				   OR SUBSTRING(@st, @pos+2, 1) IN ('T', 'S') OR ((SUBSTRING(@st, @pos-1, 1) IN ('A', 'O', 'U', 'E') OR @pos = @first)
				   AND SUBSTRING(@st, @pos+2, 1) IN ('L', 'R', 'N', 'M', 'B', 'H', 'F', 'V', 'W', ' ')) 
				BEGIN
					SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
				END
				ELSE IF @pos > @first 
				BEGIN
						IF SUBSTRING(@st, @first, 2) = 'MC' 
						BEGIN
							SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
						END
						ELSE
							SET @pri = CONCAT(@pri, 'X')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('X', 'K', 2)

				END
				ELSE
					SET @pri = CONCAT(@pri, 'X')SET @sec = CONCAT(@sec, 'X')SET @pos = @pos  + 2; -- nxt = ('X', 2)
			END
		    	-- e.g, 'czerny'
			ELSE IF SUBSTRING(@st, @pos, 2) = 'CZ' AND SUBSTRING(@st, @pos-2, 4) != 'WICZ' 
			BEGIN
				SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'X')SET @pos = @pos  + 2; -- nxt = ('S', 'X', 2)
			END-- e.g., 'focaccia'
			ELSE IF SUBSTRING(@st, @pos+1, 3) = 'CIA' 
			BEGIN
				SET @pri = CONCAT(@pri, 'X')SET @sec = CONCAT(@sec, 'X')SET @pos = @pos  + 3; -- nxt = ('X', 3)
			END-- double 'C', but not IF e.g. 'McClellan'
			ELSE IF SUBSTRING(@st, @pos, 2) = 'CC' AND NOT (@pos = (@first +1) AND SUBSTRING(@st, @first, 1) = 'M')
			BEGIN
				-- 'bellocchio' but not 'bacchus'
				IF SUBSTRING(@st, @pos+2, 1) IN ('I', 'E', 'H') AND SUBSTRING(@st, @pos+2, 2) != 'HU' 
				BEGIN
					-- 'accident', 'accede' 'succeed'
					IF (@pos = @first +1 AND SUBSTRING(@st, @first,1) = 'A') OR SUBSTRING(@st, @pos-1, 5) IN ('UCCEE', 'UCCES') 
					BEGIN
						SET @pri = CONCAT(@pri, 'KS')SET @sec = CONCAT(@sec, 'KS')SET @pos = @pos  + 3; -- nxt = ('KS', 3)
					END -- 'bacci', 'bertucci', other italian
					ELSE
					BEGIN
						SET @pri = CONCAT(@pri, 'X')SET @sec = CONCAT(@sec, 'X')SET @pos = @pos  + 3; -- nxt = ('X', 3)
					END
				END
				ELSE
					SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
			END
			ELSE IF SUBSTRING(@st, @pos, 2) IN ('CK', 'CG', 'CQ') 
			BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 'K', 2)
			END
			ELSE IF SUBSTRING(@st, @pos, 2) IN ('CI', 'CE', 'CY') 
			BEGIN
				-- italian vs. english
				IF SUBSTRING(@st, @pos, 3) IN ('CIO', 'CIE', 'CIA') 
				BEGIN
					SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'X')SET @pos = @pos  + 2; -- nxt = ('S', 'X', 2)
				END
				ELSE
					SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'S')SET @pos = @pos  + 2; -- nxt = ('S', 2)
			END
			ELSE IF SUBSTRING(@st, @pos+1, 2) IN (' C', ' Q', ' G') -- name sent IN 'mac caffrey', 'mac gregor
			BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 3; -- nxt = ('K', 3)
			END
			ELSE IF SUBSTRING(@st, @pos+1, 1) IN ('C', 'K', 'Q') AND SUBSTRING(@st, @pos+1, 2) NOT IN ('CE', 'CI') 
			BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
			END
			ELSE --  default for 'C'
			BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 1; -- nxt = ('K', 1)
			END
		END
		-- ELSE IF @ch = 'Ç' BEGIN --  will never get here with st.encode('ascii', 'replace') above
			-- SET @pri = CONCAT(@pri, '5')SET @sec = CONCAT(@sec, '5')SET @pos = @pos  + 1; -- nxt = ('S', 1)
		IF @ch = 'D' 
		BEGIN
			IF SUBSTRING(@st, @pos, 2) = 'DG' 
			BEGIN
				IF SUBSTRING(@st, @pos+2, 1) IN ('I', 'E', 'Y') 
				BEGIN -- e.g. 'edge'
					SET @pri = CONCAT(@pri, 'J')SET @sec = CONCAT(@sec, 'J')SET @pos = @pos  + 3; -- nxt = ('J', 3)
				END 
				ELSE
				BEGIN
					SET @pri = CONCAT(@pri, 'TK')SET @sec = CONCAT(@sec, 'TK')SET @pos = @pos  + 2; -- nxt = ('TK', 2)
				END
			END
			ELSE IF SUBSTRING(@st, @pos, 2) IN ('DT', 'DD') 
			BEGIN
				SET @pri = CONCAT(@pri, 'T')SET @sec = CONCAT(@sec, 'T')SET @pos = @pos  + 2; -- nxt = ('T', 2)
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'T')SET @sec = CONCAT(@sec, 'T')SET @pos = @pos  + 1; -- nxt = ('T', 1)
			END
		END
		IF @ch = 'F' 
		BEGIN
			IF SUBSTRING(@st, @pos+1, 1) = 'F' 
			BEGIN
				SET @pri = CONCAT(@pri, 'F')SET @sec = CONCAT(@sec, 'F')SET @pos = @pos  + 2; -- nxt = ('F', 2)
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'F')SET @sec = CONCAT(@sec, 'F')SET @pos = @pos  + 1; -- nxt = ('F', 1)
			END
		END
		IF @ch = 'G' 
		BEGIN
			IF SUBSTRING(@st, @pos+1, 1) = 'H' 
			BEGIN
				IF (@pos > @first AND SUBSTRING(@st, @pos-1, 1) NOT IN ('A', 'E', 'I', 'O', 'U', 'Y')) OR ( @pos = @first AND SUBSTRING(@st, @pos+2, 1) != 'I') 
				BEGIN
					SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
				END
				ELSE IF @pos = @first AND SUBSTRING(@st, @pos+2, 1) = 'I' 
				BEGIN
					SET @pri = CONCAT(@pri, 'J')SET @sec = CONCAT(@sec, 'J')SET @pos = @pos  + 2; -- nxt = ('J', 2)
				END-- Parker's rule (with some further refinements) - e.g., 'hugh'
				ELSE IF (@pos > (@first + 1) AND SUBSTRING(@st, @pos-2, 1) IN ('B', 'H', 'D') )
				   OR (@pos > (@first + 2) AND SUBSTRING(@st, @pos-3, 1) IN ('B', 'H', 'D') )
				   OR (@pos > (@first + 3) AND SUBSTRING(@st, @pos-4, 1) IN ('B', 'H') ) 
				BEGIN
					SET @pos = @pos + 2; -- nxt = (None, 2)
				END
				ELSE IF @pos > (@first + 2) AND SUBSTRING(@st, @pos-1, 1) = 'U' AND SUBSTRING(@st, @pos-3, 1) IN ('C', 'G', 'L', 'R', 'T') --  e.g., 'laugh', 'McLaughlin', 'cough', 'gough', 'rough', 'tough'
				BEGIN
					SET @pri = CONCAT(@pri, 'F')SET @sec = CONCAT(@sec, 'F')SET @pos = @pos  + 2; -- nxt = ('F', 2)
				END
				ELSE IF @pos > @first AND SUBSTRING(@st, @pos-1, 1) != 'I' 
				BEGIN
					SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
				END
				ELSE
					SET @pos = @pos + 1;
			END
			--END **mg removed
			ELSE IF SUBSTRING(@st, @pos+1, 1) = 'N' 
			BEGIN
				IF @pos = (@first +1) AND SUBSTRING(@st, @first, 1) IN ('A', 'E', 'I', 'O', 'U', 'Y') AND  @is_slavo_germanic=0 BEGIN
					SET @pri = CONCAT(@pri, 'KN')SET @sec = CONCAT(@sec, 'N')SET @pos = @pos  + 2; -- nxt = ('KN', 'N', 2)
				END
				ELSE IF SUBSTRING(@st, @pos+2, 2) != 'EY' AND SUBSTRING(@st, @pos+1, 1) != 'Y' AND @is_slavo_germanic=0 --  not e.g. 'cagney'
				BEGIN
					SET @pri = CONCAT(@pri, 'N')SET @sec = CONCAT(@sec, 'KN')SET @pos = @pos  + 2; -- nxt = ('N', 'KN', 2)
				END
				ELSE
				BEGIN
					SET @pri = CONCAT(@pri, 'KN')SET @sec = CONCAT(@sec, 'KN')SET @pos = @pos  + 2; -- nxt = ('KN', 2)
				END
			END
			--  'tagliaro'
			ELSE IF SUBSTRING(@st, @pos+1, 2) = 'LI' AND  @is_slavo_germanic=0 
			BEGIN
				SET @pri = CONCAT(@pri, 'KL')SET @sec = CONCAT(@sec, 'L')SET @pos = @pos  + 2; -- nxt = ('KL', 'L', 2)
			END
			--  -ges-,-gep-,-gel-, -gie- at beginning
			ELSE IF @pos = @first AND (SUBSTRING(@st, @pos+1, 1) = 'Y'
			   OR SUBSTRING(@st, @pos+1, 2) IN ('ES', 'EP', 'EB', 'EL', 'EY', 'IB', 'IL', 'IN', 'IE', 'EI', 'ER')) 
			   BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'J')SET @pos = @pos  + 2; -- nxt = ('K', 'J', 2)
			   END
			--  -ger-,  -gy-
			ELSE IF (SUBSTRING(@st, @pos+1, 2) = 'ER' OR SUBSTRING(@st, @pos+1, 1) = 'Y')
			   AND SUBSTRING(@st, @first, 6) NOT IN ('DANGER', 'RANGER', 'MANGER')
			   AND SUBSTRING(@st, @pos-1, 1) not IN ('E', 'I') AND SUBSTRING(@st, @pos-1, 3) NOT IN ('RGY', 'OGY') 
			   BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'J')SET @pos = @pos  + 2; -- nxt = ('K', 'J', 2)
			   END
			--  italian e.g, 'biaggi'
			ELSE IF SUBSTRING(@st, @pos+1, 1) IN ('E', 'I', 'Y') OR SUBSTRING(@st, @pos-1, 4) IN ('AGGI', 'OGGI') 
			BEGIN
				--  obvious germanic
				IF SUBSTRING(@st, @first, 4) IN ('VON ', 'VAN ') OR SUBSTRING(@st, @first, 3) = 'SCH'
				   OR SUBSTRING(@st, @pos+1, 2) = 'ET' 
				BEGIN
					SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
				END
				ELSE IF SUBSTRING(@st, @pos+1, 4) = 'IER ' 
					BEGIN
						SET @pri = CONCAT(@pri, 'J')SET @sec = CONCAT(@sec, 'J')SET @pos = @pos  + 2; -- nxt = ('J', 2)
					END
					ELSE
					BEGIN
						SET @pri = CONCAT(@pri, 'J')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('J', 'K', 2)
					END
			END
			ELSE IF SUBSTRING(@st, @pos+1, 1) = 'G' 
			BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 1; -- nxt = ('K', 1)
			END
		END
		IF @ch = 'H' 
		BEGIN
			--  only keep IF @first & before vowel OR btw. 2 ('A', 'E', 'I', 'O', 'U', 'Y')
			IF (@pos = @first OR SUBSTRING(@st, @pos-1, 1) IN ('A', 'E', 'I', 'O', 'U', 'Y'))
				AND SUBSTRING(@st, @pos+1, 1) IN ('A', 'E', 'I', 'O', 'U', 'Y') 
			BEGIN
				SET @pri = CONCAT(@pri, 'H')SET @sec = CONCAT(@sec, 'H')SET @pos = @pos  + 2; -- nxt = ('H', 2)
			END
			ELSE --  (also takes care of 'HH')\
			BEGIN
				SET @pos = @pos + 1; -- nxt = (None, 1)
			END
		END
		IF @ch = 'J' 
		BEGIN
			--  obvious spanish, 'jose', 'san jacinto'
			IF SUBSTRING(@st, @pos, 4) = 'JOSE' OR SUBSTRING(@st, @first, 4) = 'SAN ' 
			BEGIN
				IF (@pos = @first AND SUBSTRING(@st, @pos+4, 1) = ' ') OR SUBSTRING(@st, @first, 4) = 'SAN ' 
				BEGIN
					SET @pri = CONCAT(@pri, 'H')SET @sec = CONCAT(@sec, 'H'); -- nxt = ('H',)
				END
				ELSE
				BEGIN
					SET @pri = CONCAT(@pri, 'J')SET @sec = CONCAT(@sec, 'H'); -- nxt = ('J', 'H')
				END
			END
			ELSE IF @pos = @first AND SUBSTRING(@st, @pos, 4) != 'JOSE'
			BEGIN
				SET @pri = CONCAT(@pri, 'J')SET @sec = CONCAT(@sec, 'A'); -- nxt = ('J', 'A') --  Yankelovich/Jankelowicz
			END
			ELSE IF SUBSTRING(@st, @pos-1, 1) IN ('A', 'E', 'I', 'O', 'U', 'Y') AND @is_slavo_germanic=0  --  spanish pron. of e.g. 'bajador'
				   AND SUBSTRING(@st, @pos+1, 1) IN ('A', 'O') 
			BEGIN
				SET @pri = CONCAT(@pri, 'J')SET @sec = CONCAT(@sec, 'H'); -- nxt = ('J', 'H')
			END
			ELSE IF @pos = @Last 
			BEGIN
				SET @pri = CONCAT(@pri, 'J'); -- nxt = ('J', ' ')
			END
			ELSE IF SUBSTRING(@st, @pos+1, 1) not IN ('L', 'T', 'K', 'S', 'N', 'M', 'B', 'Z') AND SUBSTRING(@st, @pos-1, 1) not IN ('S', 'K', 'L') 
			BEGIN
				SET @pri = CONCAT(@pri, 'J')SET @sec = CONCAT(@sec, 'J'); -- nxt = ('J',)
			END
		
			IF SUBSTRING(@st, @pos+1, 1) = 'J' 
			BEGIN
				SET @pos = @pos + 2;
			END
			ELSE
				SET @pos = @pos + 1;
		END
		IF @ch = 'K' 
		BEGIN
			IF SUBSTRING(@st, @pos+1, 1) = 'K' 
			BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 1; -- nxt = ('K', 1)
			END
		END
		IF @ch = 'L'
		 BEGIN
			IF SUBSTRING(@st, @pos+1, 1) = 'L'
		    BEGIN
				--  spanish e.g. 'cabrillo', 'gallegos'
				IF (@pos = (@Last - 2) AND SUBSTRING(@st, @pos-1, 4) IN ('ILLO', 'ILLA', 'ALLE'))
				   OR ((SUBSTRING(@st, @Last-1, 2) IN ('AS', 'OS') OR SUBSTRING(@st, @Last,1) IN ('A', 'O')) --*** cHECK
				   AND SUBSTRING(@st, @pos-1, 4) = 'ALLE') 
				BEGIN
					SET @pri = CONCAT(@pri, 'L')SET @pos = @pos  + 2; -- nxt = ('L', ' ', 2)
				END
				ELSE
				BEGIN
					SET @pri = CONCAT(@pri, 'L')SET @sec = CONCAT(@sec, 'L')SET @pos = @pos  + 2; -- nxt = ('L', 2)
				END
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'L')SET @sec = CONCAT(@sec, 'L')SET @pos = @pos  + 1; -- nxt = ('L', 1)
			END
		END
		IF @ch = 'M' 
		BEGIN
			IF SUBSTRING(@st, @pos-1, 3) = 'UMB'
			   AND (@pos + 1 = @Last OR SUBSTRING(@st, @pos+2, 2) = 'ER')
			   OR SUBSTRING(@st, @pos+1, 1) = 'M' 
			BEGIN
				SET @pri = CONCAT(@pri, 'M')SET @sec = CONCAT(@sec, 'M')SET @pos = @pos  + 2; -- nxt = ('M', 2)
			END 
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'M')SET @sec = CONCAT(@sec, 'M')SET @pos = @pos  + 1; -- nxt = ('M', 1)
			END
		END
		IF @ch = 'N' 
		BEGIN
			IF SUBSTRING(@st, @pos+1, 1) = 'N' 
			BEGIN
				SET @pri = CONCAT(@pri, 'N')SET @sec = CONCAT(@sec, 'N')SET @pos = @pos  + 2; -- nxt = ('N', 2)
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'N')SET @sec = CONCAT(@sec, 'N')SET @pos = @pos  + 1; -- nxt = ('N', 1)
			END
		END
		-- ELSE IF @ch = u'Ñ' BEGIN
			-- SET @pri = CONCAT(@pri, '5')SET @sec = CONCAT(@sec, '5')SET @pos = @pos  + 1; -- nxt = ('N', 1)
		IF @ch = 'P' 
		BEGIN
			IF SUBSTRING(@st, @pos+1, 1) = 'H' 
			BEGIN
				SET @pri = CONCAT(@pri, 'F')SET @sec = CONCAT(@sec, 'F')SET @pos = @pos  + 2; -- nxt = ('F', 2)
			END 
			ELSE IF SUBSTRING(@st, @pos+1, 1) IN ('P', 'B') 
			BEGIN --  also account for 'campbell', 'raspberry'
				SET @pri = CONCAT(@pri, 'P')SET @sec = CONCAT(@sec, 'P')SET @pos = @pos  + 2; -- nxt = ('P', 2)
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'P')SET @sec = CONCAT(@sec, 'P')SET @pos = @pos  + 1; -- nxt = ('P', 1)
			END
		END
		IF @ch = 'Q' 
		BEGIN
			IF SUBSTRING(@st, @pos+1, 1) = 'Q' 
			BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 2; -- nxt = ('K', 2)
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'K')SET @sec = CONCAT(@sec, 'K')SET @pos = @pos  + 1; -- nxt = ('K', 1)
			END
		END
		IF @ch = 'R' 
		BEGIN
			--  fren@ch e.g. 'rogier', but exclude 'hochmeier'
			IF @pos = @Last AND @is_slavo_germanic=0
			   AND SUBSTRING(@st, @pos-2, 2) = 'IE' AND SUBSTRING(@st, @pos-4, 2) NOT IN ('ME', 'MA')
			BEGIN
				SET @sec = CONCAT(@sec, 'R'); -- nxt = ('', 'R')
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'R')SET @sec = CONCAT(@sec, 'R'); -- nxt = ('R',)
			END
			IF SUBSTRING(@st, @pos+1, 1) = 'R' 			
				SET @pos = @pos + 2;
			ELSE
				SET @pos = @pos + 1;
		END
		IF @ch = 'S' 
		BEGIN
			--  special cases 'island', 'isle', 'carlisle', 'carlysle'
			IF SUBSTRING(@st, @pos-1, 3) IN ('ISL', 'YSL') 
			    SET @pos = @pos + 1;
			--  special case 'sugar-'
			ELSE IF @pos = @first AND SUBSTRING(@st, @first, 5) = 'SUGAR' 
			BEGIN
				SET @pri = CONCAT(@pri, 'X')SET @sec = CONCAT(@sec, 'S')SET @pos = @pos  + 1; --  nxt =('X', 'S', 1)
			END
			ELSE IF SUBSTRING(@st, @pos, 2) = 'SH' 
			BEGIN
				--  germanic
				IF SUBSTRING(@st, @pos+1, 4) IN ('HEIM', 'HOEK', 'HOLM', 'HOLZ') 
				BEGIN
					SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'S')SET @pos = @pos  + 2; -- nxt = ('S', 2)
				END
				ELSE
				BEGIN
					SET @pri = CONCAT(@pri, 'X')SET @sec = CONCAT(@sec, 'X')SET @pos = @pos  + 2; -- nxt = ('X', 2)
				END
			END
			--  italian & armenian
			ELSE IF SUBSTRING(@st, @pos, 3) IN ('SIO', 'SIA') OR SUBSTRING(@st, @pos, 4) = 'SIAN' 
			BEGIN
				IF @is_slavo_germanic=0
				BEGIN
					SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'X')SET @pos = @pos  + 3; -- nxt = ('S', 'X', 3)
				END
				ELSE
				BEGIN
					SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'S')SET @pos = @pos  + 3; -- nxt = ('S', 3)
				END
			END
			--  german & anglicisations, e.g. 'smith' mat@ch 'schmidt', 'snider' mat@ch 'schneider'
			--  also, -sz- IN slavic language altho IN hungarian it is pronounced 's'
			ELSE IF (@pos = @first AND SUBSTRING(@st, @pos+1, 1) IN ('M', 'N', 'L', 'W')) OR SUBSTRING(@st, @pos+1, 1) = 'Z'
			BEGIN
				SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'X'); -- nxt = ('S', 'X')
				IF SUBSTRING(@st, @pos+1, 1) = 'Z' 
					SET @pos = @pos + 2;
				ELSE
					SET @pos = @pos + 1;
			END
			ELSE IF SUBSTRING(@st, @pos, 2) = 'SC' 
			BEGIN
				--  Schlesinger's rule
				IF SUBSTRING(@st, @pos+2, 1) = 'H' 
				BEGIN
					--  dut@ch origin, e.g. 'school', 'schooner'
					IF SUBSTRING(@st, @pos+3, 2) IN ('OO', 'ER', 'EN', 'UY', 'ED', 'EM') 
					BEGIN
						--  'schermerhorn', 'schenker'
						IF SUBSTRING(@st, @pos+3, 2) IN ('ER', 'EN') 
						BEGIN
							SET @pri = CONCAT(@pri, 'X')SET @sec = CONCAT(@sec, 'SK')SET @pos = @pos  + 3; -- nxt = ('X', 'SK', 3)
						END
						ELSE
						BEGIN
							SET @pri = CONCAT(@pri, 'SK')SET @sec = CONCAT(@sec, 'SK')SET @pos = @pos  + 3; -- nxt = ('SK', 3)
						END
					END
					ELSE IF @pos = @first AND SUBSTRING(@st, @first+3, 1) not IN ('A', 'E', 'I', 'O', 'U', 'Y') AND SUBSTRING(@st, @first+3, 1) != 'W' 
					BEGIN
							SET @pri = CONCAT(@pri, 'X')SET @sec = CONCAT(@sec, 'S')SET @pos = @pos  + 3; -- nxt = ('X', 'S', 3)
					END
					ELSE
					BEGIN
						SET @pri = CONCAT(@pri, 'X')SET @sec = CONCAT(@sec, 'X')SET @pos = @pos  + 3; -- nxt = ('X', 3)
					END
				END
				ELSE IF SUBSTRING(@st, @pos+2, 1) IN ('I', 'E', 'Y') 
				BEGIN
					SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'S')SET @pos = @pos  + 3; -- nxt = ('S', 3)
				END
				ELSE
				BEGIN
					SET @pri = CONCAT(@pri, 'SK')SET @sec = CONCAT(@sec, 'SK')SET @pos = @pos  + 3; -- nxt = ('SK', 3)
				END
			END
			ELSE IF @pos = @Last AND SUBSTRING(@st, @pos-2, 2) IN ('AI', 'OI') --  fren@ch e.g. 'resnais', 'artois'
			BEGIN 
				SET @sec = CONCAT(@sec, 'S')SET @pos = @pos  + 1; -- nxt = ('', 'S')
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'S'); -- nxt = ('S',)
				IF SUBSTRING(@st, @pos+1, 1) IN ('S', 'Z')
					SET @pos = @pos + 2;
				ELSE
					SET @pos = @pos + 1;				
			END
		END
		IF @ch = 'T' 
		BEGIN
			IF SUBSTRING(@st, @pos, 4) = 'TION' 
			BEGIN
				SET @pri = CONCAT(@pri, 'X')SET @sec = CONCAT(@sec, 'X')SET @pos = @pos  + 3; -- nxt = ('X', 3)
			END
			ELSE IF SUBSTRING(@st, @pos, 3) IN ('TIA', 'TCH') 
			BEGIN
				SET @pri = CONCAT(@pri, 'X')SET @sec = CONCAT(@sec, 'X')SET @pos = @pos  + 3; -- nxt = ('X', 3)
			END
			ELSE IF SUBSTRING(@st, @pos, 2) = 'TH' OR SUBSTRING(@st, @pos, 3) = 'TTH' 
			BEGIN
				IF SUBSTRING(@st, @pos+2, 2) IN ('OM', 'AM') OR SUBSTRING(@st, @first, 4) IN ('VON ', 'VAN ')
				   OR SUBSTRING(@st, @first, 3) = 'SCH' --  special case 'thomas', 'thames' OR germanic
				BEGIN
					SET @pri = CONCAT(@pri, 'T')SET @sec = CONCAT(@sec, 'T')SET @pos = @pos  + 2; -- nxt = ('T', 2)
				END
				ELSE
				BEGIN
					SET @pri = CONCAT(@pri, '0')SET @sec = CONCAT(@sec, 'T')SET @pos = @pos  + 2; -- nxt = ('0', 'T', 2)
				END
			END
			ELSE IF SUBSTRING(@st, @pos+1, 1) IN ('T', 'D') 
			BEGIN
				SET @pri = CONCAT(@pri, 'T')SET @sec = CONCAT(@sec, 'T')SET @pos = @pos  + 2; -- nxt = ('T', 2)
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'T')SET @sec = CONCAT(@sec, 'T')SET @pos = @pos  + 1; -- nxt = ('T', 1)
			END
		END
		IF @ch = 'V' 
		BEGIN
			IF SUBSTRING(@st, @pos+1, 1) = 'V' 
			BEGIN
				SET @pri = CONCAT(@pri, 'F')SET @sec = CONCAT(@sec, 'F')SET @pos = @pos  + 2; -- nxt = ('F', 2)
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'F')SET @sec = CONCAT(@sec, 'F')SET @pos = @pos  + 1; -- nxt = ('F', 1)
			END
		END
		IF @ch = 'W' 
		BEGIN
			--  can also be IN middle of word
			IF SUBSTRING(@st, @pos, 2) = 'WR' 
			BEGIN
				SET @pri = CONCAT(@pri, 'R')SET @sec = CONCAT(@sec, 'R')SET @pos = @pos  + 2; -- nxt = ('R', 2)
			END
			ELSE IF @pos = @first AND (SUBSTRING(@st, @pos+1, 1) IN ('A', 'E', 'I', 'O', 'U', 'Y')
				OR SUBSTRING(@st, @pos, 2) = 'WH') 
			BEGIN
				--  Wasserman should mat@ch Vasserman
				IF SUBSTRING(@st, @pos+1, 1) IN ('A', 'E', 'I', 'O', 'U', 'Y') 
				BEGIN
					SET @pri = CONCAT(@pri, 'A')SET @sec = CONCAT(@sec, 'F')SET @pos = @pos  + 1; -- nxt = ('A', 'F', 1)
				END
				ELSE
				BEGIN
					SET @pri = CONCAT(@pri, 'A')SET @sec = CONCAT(@sec, 'A')SET @pos = @pos  + 1; -- nxt = ('A', 1)
				END
			END
			--  Arnow should mat@ch Arnoff
			ELSE IF (@pos = @Last AND SUBSTRING(@st, @pos-1, 1) IN ('A', 'E', 'I', 'O', 'U', 'Y'))
			   OR SUBSTRING(@st, @pos-1, 5) IN ('EWSKI', 'EWSKY', 'OWSKI', 'OWSKY')
			   OR SUBSTRING(@st, @first, 3) = 'SCH' 
			 BEGIN
				SET @sec = CONCAT(@sec, 'F')SET @pos = @pos  + 1; -- nxt = ('', 'F', 1)
			 END
			--  polish e.g. 'filipowicz'
			ELSE IF SUBSTRING(@st, @pos, 4) IN ('WICZ', 'WITZ')
			BEGIN
				SET @pri = CONCAT(@pri, 'TS')SET @sec = CONCAT(@sec, 'FX')SET @pos = @pos  + 4; -- nxt = ('TS', 'FX', 4)
			END
			ELSE --  default is to skip it
				SET @pos = @pos + 1;
		END
		IF @ch = 'X' 
		BEGIN
			--  fren@ch e.g. breaux
			IF not(@pos = @Last AND (SUBSTRING(@st, @pos-3, 3) IN ('IAU', 'EAU')
			   OR SUBSTRING(@st, @pos-2, 2) IN ('AU', 'OU')))
			BEGIN
				SET @pri = CONCAT(@pri, 'KS')SET @sec = CONCAT(@sec, 'KS'); -- nxt = ('KS',)
			END
			IF SUBSTRING(@st, @pos+1, 1) IN ('C', 'X') 
				SET @pos = @pos + 2;
			ELSE
				SET @pos = @pos + 1;
		END
		IF @ch = 'Z' 
		BEGIN
			--  chinese pinyin e.g. 'zhao'
			IF SUBSTRING(@st, @pos+1, 1) = 'H' 
			BEGIN
				SET @pri = CONCAT(@pri, 'J')SET @sec = CONCAT(@sec, 'J')SET @pos = @pos  + 1; -- nxt = ('J', 2)
			END
			ELSE IF SUBSTRING(@st, @pos+1, 3) IN ('ZO', 'ZI', 'ZA')
			   OR (@is_slavo_germanic=1 AND @pos > @first AND SUBSTRING(@st, @pos-1, 1) != 'T')
			BEGIN
				SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'TS'); -- nxt = ('S', 'TS')\
			END
			ELSE
			BEGIN
				SET @pri = CONCAT(@pri, 'S')SET @sec = CONCAT(@sec, 'S'); -- nxt = ('S',)
			END
			IF SUBSTRING(@st, @pos+1, 1) = 'Z' 
				SET @pos = @pos + 2;
			ELSE
				SET @pos = @pos + 1;
		END
		
		--SET @pos = @pos + 1; -- DEFAULT is to move to next char	
	
	END --CASE;
    IF @pos = @prevpos 
	BEGIN
       SET @pos = @pos +1;
       SET @pri = CONCAT(@pri,'<didnt incr>'); -- it might be better to throw an error here if you really must be accurate
    END
	 ---WHILE;
	IF @pri != @sec 
	   SET @pri = CONCAT(@pri, ';', @sec);
	RETURN (@pri);
  END
	


GO


