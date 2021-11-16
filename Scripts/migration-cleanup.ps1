Write-Host "IMPORTANT: This script must be run from the root of your Sitecore CM (or CD) site!"

#Configs
Remove-Item -Path "App_Config\Include\MediaFramework" -Recurse -ErrorAction SilentlyContinue

#DLLs
Remove-Item -Path "bin\Sitecore.MediaFramework.dll" -ErrorAction SilentlyContinue
Remove-Item -Path "bin\Sitecore.MediaFramework.Mvc.dll" -ErrorAction SilentlyContinue
Remove-Item -Path "bin\Sitecore.MediaFramework.Migration.dll" -ErrorAction SilentlyContinue
Remove-Item -Path "bin\Brightcove.MediaFramework.Brightcove.dll" -ErrorAction SilentlyContinue

#layouts
Remove-Item -Path "layouts\MediaFramework" -Recurse -ErrorAction SilentlyContinue

#sitecore/*
Remove-Item -Path "sitecore\shell\client\Applications\ExperienceAnalytics\Common\Layouts\Renderings\MediaFramework" -Recurse -ErrorAction SilentlyContinue
Remove-Item -Path "sitecore\shell\Controls\Rich Text Editor\EmbedLink" -Recurse -ErrorAction SilentlyContinue
Remove-Item -Path "sitecore\shell\Controls\Rich Text Editor\EmbedMedia" -ErrorAction SilentlyContinue
Remove-Item -Path "sitecore\shell\Controls\Rich Text Editor\MF RichText Commands.js" -ErrorAction SilentlyContinue
Remove-Item -Path "sitecore\shell\Themes\Standard\MediaFramework" -Recurse -ErrorAction SilentlyContinue
Remove-Item -Path "sitecore\shell\Themes\Standard\Images\MediaFramework" -Recurse -ErrorAction SilentlyContinue

#sitecore modules/*
Remove-Item -Path "sitecore modules\Web\MediaFramework" -Recurse -ErrorAction SilentlyContinue

Write-Host "Cleanup complete..."