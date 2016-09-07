copy 'src\Invio.Hashing\project.json' 'src\Invio.Hashing\project.json.bak'
$project = Get-Content 'src\Invio.Hashing\project.json.bak' -raw | ConvertFrom-Json
$project.buildOptions.debugType = "full"
$project | ConvertTo-Json  | set-content 'src\Invio.Hashing\project.json'