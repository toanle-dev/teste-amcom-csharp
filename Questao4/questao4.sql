SELECT 
    assunto,
    ano,
    COUNT(*) AS quantidade_ocorrencias
FROM 
    atendimentos
GROUP BY
    assunto, ano
HAVING 
    COUNT(*) > 3
ORDER BY
    ano DESC,
    quantidade_ocorrencias DESC;