update Mst_ItemMaster
set abbreviation = tbl.abbrv
from Mst_ItemMaster inner join 
 (
Select distinct ST2.drug_pk, 
    substring(
        (
            Select '/'+ST1.GenericAbbrevation  AS [text()]
            From (select c.drug_pk,d.GenericAbbrevation from mst_drug c inner join 
(select a.drug_pk,b.genericabbrevation from lnk_druggeneric a inner join mst_generic b on a.GenericID=b.GenericID 
where genericabbrevation is not null and genericabbrevation <>'') d
on c.Drug_pk = d.Drug_pk) ST1
            Where ST1.Drug_pk = ST2.Drug_pk
            ORDER BY ST1.Drug_pk
            For XML PATH ('')
        ), 2, 1000) [abbrv]
From (select c.drug_pk,d.GenericAbbrevation from mst_drug c inner join 
(select a.drug_pk,b.genericabbrevation from lnk_druggeneric a inner join mst_generic b on a.GenericID=b.GenericID 
where genericabbrevation is not null and genericabbrevation <>'') d
on c.Drug_pk = d.Drug_pk) ST2) tbl
on Mst_ItemMaster.item_pk = tbl.Drug_pk