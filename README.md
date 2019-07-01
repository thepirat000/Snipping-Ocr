# Snipping-Ocr

#### A simple Snipping tool for Windows with OCR capabilities using [SpaceOCR API](https://ocr.space/ocrapi) **or** [Tesseract](https://github.com/tesseract-ocr/tesseract)

## Installation

There are have multiple ways to install the application:

- Chocolatey: Install via [Chocolatey](https://chocolatey.org/docs/installation) with command `choco install snipping-ocr`
- ClickOnce: Install via ClickOnce from [here](https://snipping-ocr.azurewebsites.net/snipping-ocr/)
- Manual: Download executable from [Releases](https://github.com/thepirat000/Snipping-Ocr/releases/)

> NOTE: This is just an example application for which I do not give support. You are free to copy/paste the code. Do not expect bugs/issues to be fixed.

### ClickOnce installation

When installing from [ClickOnce](https://snipping-ocr.azurewebsites.net/snipping-ocr/) you can get a security warning with the message "Your security settings do not allow this application to be installed on your computer". In order to be able to install, you can update the registry by creating a `.reg` file with the following content and then executing it:

```
Windows Registry Editor Version 5.00

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\Security\TrustManager\PromptingLevel]
"Internet"="Enabled"
```

Check [this SO question](https://superuser.com/questions/1252575/unable-to-install-clickonce-application-due-to-security-settings-windows-10).

## Usage

##### 1 - Start the application that will start as a systray icon.

##### 2 - Double-click the systray icon or press the Hotkey combination (CTRL+WIN+C) to start a new snip. 

> The hotkey combination can be changed and is configured on the `Snipping OCR.exe.config` file

![Start](http://i.imgur.com/3FIfidD.png)

##### 3 - Select the area on your desktop to recognize by OCR 

![Snip](http://i.imgur.com/BmpcXrB.png)

##### 4 - Wait for the results

![Process](http://i.imgur.com/3R1BQHO.png)

![Result](https://i.imgur.com/frqMxYw.png)
