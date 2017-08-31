create or replace package pkg_genr_data_access_layer
as
	procedure usp_get_pkge_list
	( p_cur_ref OUT SYS_REFCURSOR
	)
	;

	procedure usp_get_pkge_sp
	( p_cur_ref OUT SYS_REFCURSOR
	, p_nam_owner IN sys.all_procedures.owner%type
	, p_nam_pkge IN sys.all_procedures.object_name%type 
	)
	;
	
	procedure usp_get_pkge_sp_cmts
	( p_cur_ref OUT SYS_REFCURSOR
	, p_nam_owner IN sys.all_procedures.owner%type
	, p_nam_pkge IN sys.all_procedures.object_name%type 
	)
	;
end pkg_genr_data_access_layer;
/
create or replace package body pkg_genr_data_access_layer
as 
	procedure usp_get_pkge_list
	( p_cur_ref OUT SYS_REFCURSOR
	)
	as 
	begin
		open p_cur_ref for
			select lower(owner || '.' || object_name) as owner_package
			, owner
			, object_name as package 
			from all_procedures
			where object_name like 'PKG%'
			and procedure_name like 'USP%'
			group by owner, object_name
			order by owner, object_name 
			;
	end usp_get_pkge_list; 
	
	procedure usp_get_pkge_sp
	( p_cur_ref OUT SYS_REFCURSOR
	, p_nam_owner IN sys.all_procedures.owner%type
	, p_nam_pkge IN sys.all_procedures.object_name%type 
	)
	as 
	begin 
		open p_cur_ref for
			select p_nam_owner as owner_name
			, p_nam_pkge as package_name
			, procedure_name
			from all_Procedures
			where procedure_name like 'USP%'
			and owner = UPPER(p_nam_owner)
			and object_name = UPPER(p_nam_pkge)
			group by procedure_name
			order by procedure_name

		;
	end usp_get_pkge_sp; 
	
	procedure usp_get_pkge_sp_cmts
	( p_cur_ref OUT SYS_REFCURSOR
	, p_nam_owner IN sys.all_procedures.owner%type
	, p_nam_pkge IN sys.all_procedures.object_name%type 
	)
	as 
	begin 

		open p_cur_ref for
			select name, ltrim(rtrim(text)) as text, line 
			from all_source 
			where type = 'PACKAGE BODY' 
			and text like '%//%' 
			and (upper(text) like '%NAME%:%' or upper(text) like '%DESCRIPTION%:%') 
			and owner = UPPER(p_nam_owner)
			and name = UPPER(p_nam_pkge) 
			order by name, line
		;
	
	end usp_get_pkge_sp_cmts; 
	
end pkg_genr_data_access_layer;
/
/* 



*/