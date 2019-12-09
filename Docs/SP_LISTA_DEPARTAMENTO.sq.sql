CREATE PROCEDURE SP_LISTA_DEPARTAMENTO
AS
SET NOCOUNT ON;
SELECT 
    cod_dept, 
    nome_dept, 
    localizacao
FROM departamento

GO


-- EXEC SP_LISTA_DEPARTAMENTO