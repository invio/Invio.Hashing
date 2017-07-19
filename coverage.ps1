nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install coveralls.net -Version 0.7.0 -OutputDirectory tools

.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:"C:\Program Files\dotnet\dotnet.exe" -targetargs:" test ""test\Invio.Hashing.Tests\Invio.Hashing.Tests.csproj"" -f netcoreapp1.0" -register:user -filter:"+[Invio.Hashing*]* -[xunit*]*" -oldStyle -returntargetcode -output:opencover_results.xml

.\tools\coveralls.net.0.7.0\tools\csmacnz.Coveralls.exe --opencover -i .\opencover_results.xml
