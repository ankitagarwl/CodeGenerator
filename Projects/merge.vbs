' dir C:\projects\pacses_system\DeriveParameters\CodeOutput\*.config /on /b /s > c:\temp\files.txt

' dir C:\projects\pacses_system\DeriveParameters\CodeOutput\I*DataService.vb /on /b /s > c:\temp\files.txt

' dir C:\projects\pacses_system\DeriveParameters\CodeOutput\*DataService.vb /on /b /s > c:\temp\files.txt

' cscript C:\projects\pacses_system\DeriveParameters\merge.vbs

Sub MergeFiles(byVal fn) 
	Dim f
	writeLines = False
	set f = fs.OpenTextFile(fn)
	While not f.AtEndOfStream 
	    l = f.ReadLine
	    If Instr(l, "Region") <> 0 Then ' toogle on/off each Region/End Region
	        Wscript.Echo l
	        If writeLines Then 
	            fsO.WriteLine(l)
	            fsO.WriteLine(Empty)
                writelines = False
	        Else
	            writelines = True 
	        End If
	    ElseIf Instr(fn, ".config") Then ' always write the configs (no regions)
	        writelines = True
	    End If 
	    
	    If writeLines Then
	        fsO.WriteLine(l) 
	    End If 
	WEnd 
	f.Close
	set f = Nothing 
End Sub 

Dim fs, fsI, fsO

Set fs = CreateObject("Scripting.FileSystemObject")
set fsI = fs.OpenTextFile("c:\temp\files.txt")
set fsO = fs.CreateTextFile("c:\temp\mergedFiles.txt")

While not fsI.AtEndOfStream 
	fn = fsI.ReadLine 
	MergeFiles(fn)
WEnd 
fsI.Close
fsO.Close 
set fsI = Nothing 
set fs = Nothing 
