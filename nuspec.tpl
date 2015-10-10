<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd">
	<metadata>
		<id>JF.AspNet.Identity.DocumentDB</id>
		<version>1.0.8</version>
		<title>JF.AspNet.Identity.DocumentDB</title>
		<authors>Josef Fazekas</authors>
		<owners>Josef Fazekas</owners>
		<licenseUrl>https://github.com/theuntitled/JF.AspNet.Identity.DocumentDB/blob/master/LICENSE</licenseUrl>
		<projectUrl>https://github.com/theuntitled/JF.AspNet.Identity.DocumentDB</projectUrl>
		<iconUrl>https://raw.githubusercontent.com/theuntitled/JF.AspNet.Identity.DocumentDB/master/JF.AspNet.Identity.DocumentDB/theuntitled.ico</iconUrl>
		<requireLicenseAcceptance>false</requireLicenseAcceptance>
		<description>AspNet Identity Azure DocumentDB Implementation.</description>
		<copyright>Copyright © Josef Fazekas 2015</copyright>
		<language>en-GB</language>
		<tags>AspNet, Identity, Azure, DocumentDB</tags>
		<dependencies>
			<group targetFramework=".NETFramework4.5.2">
				<dependency id="JF.Azure.DocumentDB" version="1.0.7" />
				<dependency id="JF.Build.Tasks" version="1.0.0" />
				<dependency id="Microsoft.AspNet.Identity.Core" version="2.2.1" />
				<dependency id="Microsoft.Azure.DocumentDB" version="1.5.0" />
				<dependency id="Newtonsoft.Json" version="7.0.1" />
			</group>
		</dependencies>
		<frameworkAssemblies>
			<frameworkAssembly assemblyName="System" targetFramework=".NETFramework4.5.2" />
			<frameworkAssembly assemblyName="System.Core" targetFramework=".NETFramework4.5.2" />
		</frameworkAssemblies>
	</metadata>
	<files>
		<file src="JF.AspNet.Identity.DocumentDB\bin\Release\JF.AspNet.Identity.DocumentDB.XML" target="lib\JF.AspNet.Identity.DocumentDB.XML" />
		<file src="JF.AspNet.Identity.DocumentDB\bin\Release\JF.AspNet.Identity.DocumentDB.pdb" target="lib\JF.AspNet.Identity.DocumentDB.pdb" />
		<file src="JF.AspNet.Identity.DocumentDB\bin\Release\JF.AspNet.Identity.DocumentDB.dll" target="lib\JF.AspNet.Identity.DocumentDB.dll" />
	</files>
</package>