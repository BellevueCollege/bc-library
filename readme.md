# BC Library

## Overview 
The BC Library is intended to be a place for generally useful functionality that can be utilized by BC .NET applications.

## Development requirements

 - .NET Framework 4.5
 - Visual Studio 2013

## How to use
The BC Library can be included in another .NET application using the NuGet package manager. You will want to ensure you have BC's Azure DevOps artifacts feed added as a package source in Visual Studio. You can do so under `Tools > Library Package Manager > Package Manager Settings` for Visual Studio 2013 or `Tools > NuGet Package Manager > Package Manager Settings` in newer versions of Visual Studio.  The name for the package source can be whatever is recognizable to you. The source URL is: https://pkgs.dev.azure.com/bcintegration/_packaging/BCFeed/nuget/v3/index.json

Once BC Azure DevOps is set up as a package source you can include the BC Library in your .NET application via `Manage NuGet Packages`. You will likely need to log in with your Azure DevOps login (should be your BC NetID).

## Functionality
Currently the only functionality available in BC Library is to provide Globals-related dynamic include/path functionality in Master Pages.

### Settings
BC Library functionality related to Globals requires that app settings for `GlobalsURI` and `GlobalsPath` are set in your application config (i.e. `web.config`).

Example:
```
<appSettings>
   <add key="GlobalsURI" value="//s.bellevuecollege.edu/g/3/" />
   <add key="GlobalsPath" value="..\globals\g\3\" />
</appSettings>
```

### Include in a Master Page to provide custom tags

```
<%@ Register TagPrefix="bc" Namespace="BCLibrary.Globals" Assembly="BCLibrary" %>
```

### Usage of custom tags

#### Setting a dynamic path for Globals JavaScript links
The GlobalsJSLink custom control is used to translate Globals' partial paths into complete <script> references to Globals JavaScript resources. This method is necessary both because .NET assumes control of <script> references as controls, but also because .NET doesn't allow dynamic rendering of paths in some contexts (e.g. Master Pages).
 - Example usage: `<bc:GlobalsJSLink runat="server" FilePath="j/ghead.js?ver=3.3" />`
 - Example output: `<script type='text/javascript' src='[globalsURI]j/ghead.js?ver=3.3'></script>`

#### Including a Globals file
The GlobalsFileInclude custom control translates the given file path, reads the file, and includes the content during rendering. This control is necessary to replace server side includes since those do not allow dynamic path creation.

 - Example usage in Master Page: `<bc:GlobalsFileInclude runat="server" FilePath="h/gabranded.html" />`
 - Pulls included file and outputs it to page

> Dev note: The current way this is coded isn't great as the files get held open and can't be updated by automated deploy operations for Globals. I would find a better way, if I were to do this again. -NS

#### parseCssControls() function

This snazzy function updates all href attributes for elements with IDs that begin with the specified identifier. The href is updated to the complete globals path. Typical usage is in a code-behind file for updating CSS controls dynamically, necessary because .NET doesn't allow dynamic generation of paths in some contexts (e.g. Master Pages).

 - Example usage in Master Page: `<link id="globalsscssg" rel="stylesheet" href="c/g.css?ver=3.3">` where `globalsscss` is the identifier
 - Example parsing code in Master Page code-behind:
 ```
  Dim cc As ControlCollection = DirectCast(Page.Controls, ControlCollection)
  Functions.parseCssControls(cc, "globalscss", GlobalsURI)
 ```
 - Example output: `<link id="globalscssg" rel="stylesheet" href="[globalsURI]c/g.css?ver=3.3">`