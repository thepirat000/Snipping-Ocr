$packageName = 'gols'
$fileType = 'exe'
$url = 'https://github.com/thepirat000/gols/releases/download/v1.0.0.1/setup.exe'
$silentArgs = '/s /v"/qn"'

Install-ChocolateyPackage $packageName $fileType $silentArgs $url