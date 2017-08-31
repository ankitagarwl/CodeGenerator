select name, ltrim(rtrim(text)) as text, line 
from all_source 
where type = 'PACKAGE BODY' 
and substr(ltrim(rtrim(text)), 1,2) = '//' 
and (upper(text) like '%NAME%:%' or upper(text) like '%DESCRIPTION%:%') 
and owner = user order by name, line

select lower(user) || '.' || min(lower(object_name)) || '.' || lower(procedure_name)
from all_Procedures
where procedure_name like 'USP%'
and object_name = ''
group by procedure_name
order by procedure_name
         
         
/* 
set cmd="C:\Documents and Settings\devlinro\My Documents\Visual Studio 2008\Projects\DeriveParameters\DeriveParameters\bin\Debug\DeriveParameters.exe"
*/ 

